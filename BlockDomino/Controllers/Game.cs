using System;
using System.Collections.Generic;
using System.Linq;
using BlockDomino.Enums;
using BlockDomino.Interfaces;

namespace BlockDomino.Controllers
{
    public class Game
    {
        private readonly List<IPlayer> _players;
        private readonly IDeck _deck;
        private readonly IBoard _board;
        private byte _currentTurn;
        public int ScoreLimit { get; set; } = 50;
        public IReadOnlyList<IPlayer> Players => _players.AsReadOnly();
        public Action<IPlayer> OnGameOver;
        public Game(List<IPlayer> players, IDeck deck, IBoard board)
        {
            if (players == null || players.Count < 2 || players.Count > 4)
                throw new ArgumentException("Game requires 2 to 4 players");

            _players = players;
            _deck = deck;
            _board = board;
            _currentTurn = 0;
        }

        #region Deck / Deal

        public void SetupDeck()
        {
            _deck.DominoTiles.Clear();
            for (byte i = 0; i <= 6; i++)
            {
                for (byte j = i; j <= 6; j++)
                {
                    // Creating concrete tiles here; deck/list holds IDominoTile
                    _deck.DominoTiles.Add(new Models.DominoTile(i, j));
                }
            }
        }

        public void ShuffleDeck()
        {
            var rng = new Random();
            var shuffled = _deck.DominoTiles.OrderBy(_ => rng.Next()).ToList();

            _deck.DominoTiles.Clear();
            foreach (var tile in shuffled)
            {
                _deck.DominoTiles.Add(tile);
            }
        }

        private int GetHandSize()
        {
            // Common convention: 2 players -> 7 each, 3 players -> 6, 4 players -> 5
            return _players.Count == 2 ? 7 : (_players.Count == 3 ? 6 : 5);
        }

        private void DealHands()
        {
            // Clear existing hands first (for new round)
            foreach (var p in _players)
                p.Hand.Clear();

            int handSize = GetHandSize();
            for (int i = 0; i < handSize; i++)
            {
                foreach (var p in _players)
                {
                    if (_deck.DominoTiles.Count == 0) break;
                    var tile = _deck.DominoTiles[0];
                    _deck.DominoTiles.RemoveAt(0);
                    p.Hand.Add(tile);
                }
            }
        }

        #endregion

        #region Start / Round management

        /// <summary>
        /// Start a new match (sets scores to 0) and deals the first round.
        /// </summary>
        public void StartMatch()
        {
            foreach (var p in _players)
                p.Score = 0;

            StartRound();
        }

        /// <summary>
        /// Prepares and starts a new round: builds deck, shuffles, deals, picks starter by highest double.
        /// </summary>
        public void StartRound()
        {
            // prepare deck and deal
            SetupDeck();
            ShuffleDeck();
            DealHands();

            // clear board
            _board.DominoTiles.Clear();

            // Decide first turn by highest double among all players (6-6, 5-5, ...).
            DecideFirstTurnByHighestDouble();
        }

        private void DecideFirstTurnByHighestDouble()
        {
            IPlayer? starter = null;
            byte bestDouble = 0;
            int starterIndex = 0;

            for (int i = 0; i < _players.Count; i++)
            {
                var player = _players[i];
                foreach (var tile in player.Hand)
                {
                    if (tile.IsDouble && tile.PipLeft >= bestDouble)
                    {
                        bestDouble = tile.PipLeft;
                        starter = player;
                        starterIndex = i;
                    }
                }
            }

            if (starter != null)
            {
                // place the highest double and remove from player's hand
                var playedDouble = starter.Hand.First(t => t.IsDouble && t.PipLeft == bestDouble);
                starter.PlayDominoTile(playedDouble);
                _board.DominoTiles.Add(playedDouble);
                _currentTurn = (byte)starterIndex;
            }
            else
            {
                // fallback: first player plays their first tile (if any)
                var first = _players[0];
                var firstTile = first.Hand.FirstOrDefault();
                if (firstTile != null)
                {
                    first.PlayDominoTile(firstTile);
                    _board.DominoTiles.Add(firstTile);
                }
                _currentTurn = 0;
            }
        }

        #endregion

        #region Gameplay helpers / moves

        public void DisplayBoard()
        {
            if (_board.DominoTiles.Count == 0)
            {
                Console.WriteLine("Board is empty.");
                return;
            }

            Console.WriteLine(string.Join(" ", _board.DominoTiles.Select(t => t.ToString())));
        }

        /// <summary>
        /// Shows only actually playable tiles for the player (taking orientation into account).
        /// </summary>
        public void CheckPlayableDominoPlayerHand(IPlayer player, byte[] openEnds)
        {
            var playable = player.Hand
                .Where(d => CanPlay(d))
                .ToList();

            if (playable.Count == 0)
            {
                Console.WriteLine($"{player.Name} has no playable dominoes.");
                return;
            }

            Console.WriteLine($"{player.Name} playable tiles: " +
                string.Join(", ", playable.Select(x => x.ToString())));
        }

        /// <summary>
        /// Attempts to play a domino from a player's hand (auto-detect left/right and flip if necessary).
        /// Returns true if the play succeeded.
        /// </summary>
        public bool PlayDominoTile(IPlayer player, IDominoTile dominoTile)
        {
            // If board empty, simply place it
            if (_board.DominoTiles.Count == 0)
            {
                _board.DominoTiles.Add(dominoTile);
                player.PlayDominoTile(dominoTile);
                return true;
            }

            // Try left then right (preserve original tile removal via player.PlayDominoTile)
            if (TryPlaceLeft(player, dominoTile)) return true;
            if (TryPlaceRight(player, dominoTile)) return true;

            // No valid placement
            return false;
        }

        private bool TryPlaceLeft(IPlayer player, IDominoTile tile)
        {
            byte leftEnd = _board.DominoTiles.First().PipLeft;

            // fits as-is: tile.PipRight must match leftEnd
            if (tile.PipRight == leftEnd)
            {
                _board.DominoTiles.Insert(0, tile);
                player.PlayDominoTile(tile);
                return true;
            }

            // if tile.PipLeft matches leftEnd, flip needed
            if (tile.PipLeft == leftEnd)
            {
                var flipped = FLipDominoTile(tile);
                _board.DominoTiles.Insert(0, flipped);
                player.PlayDominoTile(tile); // remove original from hand
                return true;
            }

            return false;
        }

        private bool TryPlaceRight(IPlayer player, IDominoTile tile)
        {
            byte rightEnd = _board.DominoTiles.Last().PipRight;

            // fits as-is: tile.PipLeft must match rightEnd
            if (tile.PipLeft == rightEnd)
            {
                _board.DominoTiles.Add(tile);
                player.PlayDominoTile(tile);
                return true;
            }

            // if tile.PipRight matches rightEnd, flip needed
            if (tile.PipRight == rightEnd)
            {
                var flipped = FLipDominoTile(tile);
                _board.DominoTiles.Add(flipped);
                player.PlayDominoTile(tile);
                return true;
            }

            return false;
        }

        public void NextTurn()
        {
            _currentTurn = (byte)((_currentTurn + 1) % _players.Count);
        }

        public byte GetCurrentTurnIndex() => _currentTurn;

        public IPlayer GetCurrentPlayer() => _players[_currentTurn];

        /// <summary>
        /// Determines whether the current tile can actually be placed on either end (orientation-aware).
        /// </summary>
        public bool CanPlay(IDominoTile tile)
        {
            if (_board.DominoTiles.Count == 0) return true;
            return CanPlayLeft(tile) || CanPlayRight(tile);
        }

        public bool CanPlayLeft(IDominoTile tile)
        {
            if (_board.DominoTiles.Count == 0) return true;
            byte leftEnd = _board.DominoTiles.First().PipLeft;
            return tile.PipRight == leftEnd || tile.PipLeft == leftEnd;
        }

        public bool CanPlayRight(IDominoTile tile)
        {
            if (_board.DominoTiles.Count == 0) return true;
            byte rightEnd = _board.DominoTiles.Last().PipRight;
            return tile.PipLeft == rightEnd || tile.PipRight == rightEnd;
        }

        public bool IsRoundOver()
        {
            // Round is over if a player emptied their hand
            if (_players.Any(p => p.Hand.Count == 0)) return true;

            // Or if no one can play (board locked)
            var openEnds = GetOpenEnds();
            if (openEnds.Length == 0) return false;
            bool anyoneCanPlay = _players.Any(p => p.Hand.Any(d => CanPlay(d)));
            return !anyoneCanPlay;
        }

        public bool IsRoundDraw()
        {
            // Round draw when locked (no one can play) and sums could be used to determine winner
            return IsRoundOver() && _players.All(p => p.Hand.Count > 0);
        }

        #endregion

        #region Round resolution & scoring

        /// <summary>
        /// Called when a round ends (either someone emptied their hand or board is locked).
        /// This method calculates pip totals, awards points to round winner (if unique), and returns the round winner.
        /// If there is a tie for lowest pip total, no points are awarded and this returns null.
        /// </summary>
        public IPlayer? ResolveRound()
        {
            // 1) Compute pip sums for all players
            var sums = _players
                .Select(p => new
                {
                    Player = p,
                    Sum = p.Hand.Sum(d => d.PipLeft + d.PipRight)
                })
                .ToList();

            // 2) If any player has an empty hand, that player is the winner
            var emptyWinner = sums.FirstOrDefault(s => s.Sum == 0);
            if (emptyWinner != null)
            {
                // Compute total pips of all opponents
                int opponentSum = sums.Where(s => s.Player != emptyWinner.Player).Sum(s => s.Sum);
                int winnerSum = emptyWinner.Sum;
                int pointsEarned = opponentSum - winnerSum;  // difference scoring

                // Update winner's score
                emptyWinner.Player.Score = checked((byte)Math.Min(255, emptyWinner.Player.Score + pointsEarned));
                return emptyWinner.Player;
            }

            // 3) If no one has an empty hand, check who has the lowest pip sum
            int minSum = sums.Min(s => s.Sum);
            var lowest = sums.Where(s => s.Sum == minSum).ToList();

            // Only one player has the lowest pip total => that player is the winner
            if (lowest.Count == 1)
            {
                var winnerInfo = lowest[0];
                int totalOpponentSum = sums.Where(s => s.Player != winnerInfo.Player).Sum(s => s.Sum);
                int winnerSum = winnerInfo.Sum;
                int pointsEarned = totalOpponentSum - winnerSum; // difference scoring

                // Award points
                winnerInfo.Player.Score = checked((byte)Math.Min(255, winnerInfo.Player.Score + pointsEarned));
                return winnerInfo.Player;
            }

            // 4) Tie for the lowest sum => no winner, no points
            return null;
        }

        public bool IsMatchOver()
        {
            return _players.Any(p => p.Score >= ScoreLimit);
        }

        public IPlayer? GetMatchWinner()
        {
            if (!IsMatchOver()) return null;
            return _players.OrderByDescending(p => p.Score).First();
        }

        public void NextRound()
        {
            // If match over, invoke OnGameOver and do not start a new round
            if (IsMatchOver())
            {
                var matchWinner = GetMatchWinner();
                if (matchWinner != null)
                {
                    OnGameOver?.Invoke(matchWinner);
                }
                return;
            }
            StartRound();
        }

        #endregion

        #region Utilities

        public IDominoTile FLipDominoTile(IDominoTile dominoTile)
        {
            // flip concrete tile: create new DominoTile with swapped pips
            return new Models.DominoTile(dominoTile.PipRight, dominoTile.PipLeft);
        }

        public byte[] GetOpenEnds()
        {
            if (_board.DominoTiles.Count == 0)
                return new byte[0];

            var left = _board.DominoTiles.First().PipLeft;
            var right = _board.DominoTiles.Last().PipRight;
            return new[] { left, right };
        }

        private bool IsDominoTileValid(IDominoTile dominoTile, byte[] openEnds)
        {
            if (_board.DominoTiles.Count == 0) return true;
            return openEnds.Contains(dominoTile.PipLeft) ||
                   openEnds.Contains(dominoTile.PipRight);
        }

        public int GetPipSum(IPlayer player)
        {
            return player.Hand.Sum(t => t.PipLeft + t.PipRight);
        }

        #endregion
    }
}

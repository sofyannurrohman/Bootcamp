using System;
using System.Collections.Generic;
using System.Linq;
using DominoConsoleMVC.Models;
using DominoConsoleMVC.Interfaces;
using DominoConsoleMVC.Views;

namespace DominoConsoleMVC.Controllers
{
    public class GameController
    {
        private readonly IView _view;
        private readonly IGameRules _rules;
        private readonly List<IPlayer> _players = new();
        private readonly IBoard _board;
        private readonly IBoneyard _boneyard;
        private int _consecutivePasses = 0;

        public GameController(IView view, IGameRules rules, IBoard board, IBoneyard boneyard)
        {
            _view = view;
            _rules = rules;
            _board = board;
            _boneyard = boneyard;
        }

        public void StartGame()
        {
            _view.ShowWelcome();

            SetupPlayers();
            SetupTiles();
            DealInitialHands();

            PlayLoop();
            EndGame(); // fallback
        }

        private void SetupPlayers()
        {
            Console.Write("Enter your name: ");
            var name = Console.ReadLine()?.Trim();
            if (string.IsNullOrEmpty(name)) name = "Player1";

            _players.Add(new Player(name, isHuman: true));
            _players.Add(new Player("Computer", isHuman: false));
        }

        private void SetupTiles()
        {
            var all = new List<DominoTile>();
            for (int i = 0; i <= 6; i++)
                for (int j = i; j <= 6; j++)
                    all.Add(new DominoTile(i, j));

            var rnd = new Random();
            foreach (var t in all.OrderBy(_ => rnd.Next()))
                _boneyard.Push(t);
        }

        private void DealInitialHands()
        {
            foreach (var player in _players)
            {
                for (int i = 0; i < 7 && !_boneyard.IsEmpty; i++)
                {
                    var tile = _boneyard.Draw();
                    player.Hand.Add(tile);
                }
            }
        }

        private void PlayLoop()
        {
            int currentIndex = 0;

            while (true)
            {
                var player = _players[currentIndex];
                _view.ShowBoard(_board);

                var moves = _board.IsEmpty
                    ? player.Hand.Select(t => (t, playLeft: false)).ToList()
                    : _rules.GetPlayableMoves(_board, player);

                if (moves.Any())
                {
                    HandlePlayerMove(player, moves);
                }
                else
                {
                    HandleNoMoves(player);
                }

                if (CheckForWinner(player)) return;
                if (CheckForBlockedGame()) return;

                currentIndex = (currentIndex + 1) % _players.Count;
            }
        }

        private void HandleNoMoves(IPlayer player)
        {
            if (!_boneyard.IsEmpty)
            {
                var drawn = _boneyard.Draw();
                player.Hand.Add(drawn);
                _view.ShowMessage($"{player.Name} draws {drawn}");
                _consecutivePasses = 0;
            }
            else
            {
                _view.ShowMessage($"{player.Name} cannot move. Passes.");
                _consecutivePasses++;
            }
        }

        private void HandlePlayerMove(IPlayer player, List<(DominoTile tile, bool playLeft)> moves)
        {
            string choiceInput = _view.AskPlayerChoice(player, moves, canDraw: true);

            switch (choiceInput.ToUpperInvariant())
            {
                case "P":
                    _view.ShowMessage($"{player.Name} passes.");
                    _consecutivePasses++;
                    return;

                case "D":
                    HandleDraw(player);
                    return;
            }

            if (int.TryParse(choiceInput, out int choice) && choice >= 1 && choice <= moves.Count)
            {
                var (tile, playLeft) = moves[choice - 1];
                PlaceTile(player, tile, playLeft);
            }
            else
            {
                _view.ShowMessage("Invalid choice, skipping turn.");
                _consecutivePasses++;
            }
        }

        private void HandleDraw(IPlayer player)
        {
            if (!_boneyard.IsEmpty)
            {
                var drawnTile = _boneyard.Draw();
                player.Hand.Add(drawnTile);
                _view.ShowMessage($"{player.Name} draws {drawnTile}");
                _consecutivePasses = 0;
            }
            else
            {
                _view.ShowMessage("Boneyard is empty. Cannot draw.");
                _consecutivePasses++;
            }
        }

        private void PlaceTile(IPlayer player, DominoTile tile, bool playLeft)
        {
            if (_board.IsEmpty || !playLeft)
                _board.PlaceRight(tile);
            else
                _board.PlaceLeft(tile);

            player.Hand.Remove(tile);
            _consecutivePasses = 0;
        }

        private bool CheckForWinner(IPlayer player)
        {
            if (!player.Hand.Any())
            {
                EndGame(player);
                return true;
            }
            return false;
        }

        private bool CheckForBlockedGame()
        {
            if (_consecutivePasses >= _players.Count)
            {
                EndGame();
                return true;
            }
            return false;
        }

        private void EndGame(IPlayer? winner = null)
        {
            _view.ShowBoard(_board);

            if (winner != null)
            {
                _view.ShowMessage($"Game Over! Winner: {winner.Name}");
            }
            else
            {
                var scores = _players
                    .Select(p => new { p.Name, Score = p.Hand.Sum(t => t.Left + t.Right) })
                    .ToList();

                var minScore = scores.Min(s => s.Score);
                var winnerByScore = scores.First(s => s.Score == minScore);

                _view.ShowMessage(
                    $"Game Over! Blocked game. Winner by lowest score: {winnerByScore.Name} with {winnerByScore.Score} points."
                );
            }
        }
    }
}

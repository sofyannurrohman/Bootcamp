// Core/PokerGame.cs
using BootcampDay8.PokerGame.Enums;
using BootcampDay8.PokerGame.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace BootcampDay8.PokerGame.Core
{
    public class PokerGame
    {
        private List<Player> _players = new List<Player>();
        private ICardProvider _cardProvider;
        private IHandEvaluator _handEvaluator;
        private List<IGameEventHandler> _eventHandlers = new List<IGameEventHandler>();

        public ObservableCollection<Player> Players { get; } = new ObservableCollection<Player>();
        public int Pot { get; private set; }
        public Player CurrentPlayer { get; private set; }
        public GameState State { get; set; }
        public Action<GameState> GameStateChanged { get; set; }
        private int _currentBet;
        private int _currentRaise;
        private int _playersActed;
        public int CurrentBet => _currentBet;
        public int CurrentRaise => _currentRaise;

        public PokerGame(ICardProvider cardProvider, IHandEvaluator handEvaluator)
        {
            _cardProvider = cardProvider;
            _handEvaluator = handEvaluator;
            State = GameState.PreGame;
        }

        public void AddEventHandler(IGameEventHandler handler)
        {
            _eventHandlers.Add(handler);
        }

        public void RemoveEventHandler(IGameEventHandler handler)
        {
            _eventHandlers.Remove(handler);
        }

        public void AddPlayer(Player player)
        {
            _players.Add(player);
            Players.Add(player);
        }

        public void StartNewGame()
        {
            Pot = 0;
            foreach (var player in _players)
            {
                player.ResetForNewRound();
            }
            _cardProvider.Shuffle();
            ChangeGameState(GameState.Dealing);
        }

        public void DealInitialCards()
        {
            foreach (var player in _players.Where(p => p.IsActive))
            {
                for (int i = 0; i < 5; i++)
                {
                    var card = _cardProvider.DrawCard();
                    card.IsFaceUp = false;
                    player.Hand.AddCard(card);

                    NotifyEventHandlers(handler => handler.OnCardDealt(card, player));
                }
            }
            ChangeGameState(GameState.Betting);
            StartBettingRound(); // <<--- ensure betting actually starts
        }

        private int _smallBlind = 5;
        private int _bigBlind = 10;

        public void StartBettingRound()
        {
            _currentBet = 0;
            _currentRaise = 0;
            _playersActed = 0;

            // Reset current bets for all players
            foreach (var player in _players)
            {
                player.ResetCurrentBet();
            }

            // Post blinds if it's the first betting round
            if (State == GameState.Betting)
            {
                PostBlinds();
            }

            // Find first active player who hasn't folded
            CurrentPlayer = _players.First(p => p.IsActive && !p.HasFolded);

            NotifyEventHandlers(handler => handler.OnBettingRoundStarted());
        }

        private void PostBlinds()
        {
            var activePlayers = _players.Where(p => p.IsActive).ToList();
            if (activePlayers.Count >= 2)
            {
                // Small blind
                var smallBlindPlayer = activePlayers[0];
                var smallBlindAmount = Math.Min(_smallBlind, smallBlindPlayer.Chips);
                Pot += smallBlindPlayer.PlaceBet(smallBlindAmount);

                // Big blind
                var bigBlindPlayer = activePlayers[1];
                var bigBlindAmount = Math.Min(_bigBlind, bigBlindPlayer.Chips);
                Pot += bigBlindPlayer.PlaceBet(bigBlindAmount);
            }
        }

        public void PlayerAction(PlayerAction action, int amount = 0)
        {
            if (CurrentPlayer == null || CurrentPlayer.HasFolded || !CurrentPlayer.IsActive)
                return;

            switch (action)
            {
                case Enums.PlayerAction.Fold:
                    CurrentPlayer.Fold();
                    NotifyEventHandlers(handler => handler.OnPlayerFolded(CurrentPlayer));
                    break;

                case Enums.PlayerAction.Check:
                    if (_currentBet > CurrentPlayer.CurrentBet)
                        throw new InvalidOperationException("Cannot check when there's a bet to call");
                    NotifyEventHandlers(handler => handler.OnPlayerChecked(CurrentPlayer));
                    break;

                case Enums.PlayerAction.Call:
                    var callAmount = _currentBet - CurrentPlayer.CurrentBet;
                    if (callAmount > 0)
                    {
                        var actualCall = Math.Min(callAmount, CurrentPlayer.Chips);
                        Pot += CurrentPlayer.PlaceBet(actualCall);
                        NotifyEventHandlers(handler => handler.OnPlayerCalled(CurrentPlayer, actualCall));
                    }
                    break;

                case Enums.PlayerAction.Raise:
                    if (amount <= _currentBet)
                        throw new InvalidOperationException("Raise must be higher than current bet");

                    var raiseAmount = amount - CurrentPlayer.CurrentBet;
                    var actualRaise = Math.Min(raiseAmount, CurrentPlayer.Chips);
                    Pot += CurrentPlayer.PlaceBet(actualRaise);
                    _currentBet = CurrentPlayer.CurrentBet;
                    _currentRaise = _currentBet;
                    NotifyEventHandlers(handler => handler.OnPlayerRaised(CurrentPlayer, _currentBet));
                    break;

                case Enums.PlayerAction.Bet:
                    if (_currentBet > 0)
                        throw new InvalidOperationException("Cannot bet when there's already a bet (use raise instead)");

                    var actualBet = Math.Min(amount, CurrentPlayer.Chips);
                    Pot += CurrentPlayer.PlaceBet(actualBet);
                    _currentBet = actualBet;
                    _currentRaise = actualBet;
                    NotifyEventHandlers(handler => handler.OnPlayerBet(CurrentPlayer, actualBet));
                    break;
            }

            MoveToNextPlayer();
        }

        private void MoveToNextPlayer()
        {
            _playersActed++;

            // Check if betting round is complete
            var activePlayers = _players.Where(p => p.IsActive && !p.HasFolded).ToList();
            if (_playersActed >= activePlayers.Count && AllPlayersEqualized())
            {
                if (State == GameState.Betting)
                {
                    StartDiscardRound();
                }
                else if (State == GameState.Discarding)
                {
                    ChangeGameState(GameState.Reveal);
                    StartShowdown();
                }
                return;
            }

            // Find next active player
            var currentIndex = _players.IndexOf(CurrentPlayer);
            for (int i = 1; i <= _players.Count; i++)
            {
                var nextIndex = (currentIndex + i) % _players.Count;
                var nextPlayer = _players[nextIndex];

                if (nextPlayer.IsActive && !nextPlayer.HasFolded)
                {
                    CurrentPlayer = nextPlayer;
                    break;
                }
            }
        }

        private bool AllPlayersEqualized()
        {
            var activePlayers = _players.Where(p => p.IsActive && !p.HasFolded);
            return activePlayers.All(p => p.CurrentBet == _currentBet || p.Chips == 0);
        }

        // Add to PokerGame.cs
        public void StartDiscardRound()
        {
            ChangeGameState(GameState.Discarding);
            CurrentPlayer = _players.First(p => p.IsActive && !p.HasFolded);

            NotifyEventHandlers(handler => handler.OnDiscardRoundStarted());
        }

        public void DiscardCards(Player player, List<int> cardIndices)
        {
            if (State != GameState.Discarding)
                throw new InvalidOperationException("Not in discard round");

            if (player != CurrentPlayer)
                throw new InvalidOperationException("Not player's turn");

            // Use the RemoveCardsByIndices method from your Hand class
            var discardedCards = player.Hand.RemoveCardsByIndices(cardIndices);

            // Notify about each discarded card
            foreach (var discardedCard in discardedCards)
            {
                NotifyEventHandlers(handler => handler.OnCardDiscarded(discardedCard, player));
            }

            // Draw new cards to replace the discarded ones
            for (int i = 0; i < discardedCards.Count; i++)
            {
                var newCard = _cardProvider.DrawCard();
                newCard.IsFaceUp = false;
                player.Hand.AddCard(newCard);
                NotifyEventHandlers(handler => handler.OnCardDealt(newCard, player));
            }

            MoveToNextDiscardingPlayer();
        }

        private void MoveToNextDiscardingPlayer()
        {
            var currentIndex = _players.IndexOf(CurrentPlayer);
            for (int i = 1; i <= _players.Count; i++)
            {
                var nextIndex = (currentIndex + i) % _players.Count;
                var nextPlayer = _players[nextIndex];

                if (nextPlayer.IsActive && !nextPlayer.HasFolded)
                {
                    CurrentPlayer = nextPlayer;
                    return;
                }
            }

            // If no next player, start second betting round
            ChangeGameState(GameState.Betting);
            StartBettingRound();
        }

        public void StartShowdown()
        {
            // Reveal all cards
            foreach (var player in _players.Where(p => p.IsActive && !p.HasFolded))
            {
                foreach (var card in player.Hand.Cards)
                {
                    card.IsFaceUp = true;
                }
                NotifyEventHandlers(handler => handler.OnPlayerRevealedHand(player));
            }

            var winner = DetermineWinner();
            DistributePot(winner);
        }

        // Update the DetermineWinner method
        public Player DetermineWinner()
        {
            var activePlayers = _players.Where(p => !p.HasFolded).ToList();
            if (activePlayers.Count == 1)
                return activePlayers.First();

            // Evaluate hands and determine winner
            var playerHands = new Dictionary<Player, HandRank>();
            foreach (var player in activePlayers)
            {
                // Use GetAllCards() to get a mutable list for evaluation
                var cards = player.Hand.GetAllCards();
                var handRank = _handEvaluator.EvaluateHand(cards);
                playerHands[player] = handRank;
            }

            return playerHands.OrderByDescending(ph => ph.Value).First().Key;
        }

        private void DistributePot()
        {
            var activePlayers = _players.Where(p => !p.IsFolded).ToList();

            if (activePlayers.Count == 1)
            {
                var winner = activePlayers[0];
                winner.AddChips(_pot);
                OnGameEvent?.Invoke($"{winner.Name} wins {_pot} chips!");
            }
            else
            {
                // Split pot untuk multiple winners
                var splitAmount = _pot / activePlayers.Count;
                foreach (var player in activePlayers)
                {
                    player.AddChips(splitAmount);
                }
                OnGameEvent?.Invoke($"Split pot! Each player gets {splitAmount} chips");
            }
        }

        private void ChangeGameState(GameState newState)
        {
            State = newState;
            GameStateChanged?.Invoke(newState);
            NotifyEventHandlers(handler => handler.OnGameStateChanged(newState));
        }

        private void NotifyEventHandlers(Action<IGameEventHandler> notification)
        {
            foreach (var handler in _eventHandlers)
            {
                notification(handler);
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace PokerGame
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private PokerGame _game;
        private HumanPlayer _humanPlayer;
        
        public ObservableCollection<Card> CommunityCards { get; set; } = new ObservableCollection<Card>();
        public ObservableCollection<Card> PlayerHand { get; set; } = new ObservableCollection<Card>();
        public ObservableCollection<Player> Opponents { get; set; } = new ObservableCollection<Player>();
        
        private int _pot;
        public int Pot
        {
            get => _pot;
            set { _pot = value; OnPropertyChanged(nameof(Pot)); PotText.Text = value.ToString(); }
        }
        
        private int _currentBet;
        public int CurrentBet
        {
            get => _currentBet;
            set { _currentBet = value; OnPropertyChanged(nameof(CurrentBet)); CurrentBetText.Text = value.ToString(); }
        }
        
        private int _minRaise;
        public int MinRaise
        {
            get => _minRaise;
            set { _minRaise = value; OnPropertyChanged(nameof(MinRaise)); MinRaiseText.Text = value.ToString(); }
        }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            SetupEventHandlers();
        }

        private void SetupEventHandlers()
        {
            StartGameButton.Click += (s, e) => StartGame();
            NextRoundButton.Click += (s, e) => NextRound();
            FoldButton.Click += (s, e) => MakeDecision("fold");
            CheckButton.Click += (s, e) => MakeDecision("check");
            CallButton.Click += (s, e) => MakeDecision("call");
            RaiseButton.Click += (s, e) => MakeRaise();
            AllInButton.Click += (s, e) => MakeAllIn();
        }

        private void StartGame()
        {
            // Create players
            _humanPlayer = new HumanPlayer("You", 1000);
            var players = new List<Player>
            {
                _humanPlayer,
                new AIPlayer("AI 1", 1000),
                new AIPlayer("AI 2", 1000),
                new AIPlayer("AI 3", 1000)
            };

            // Initialize game
            _game = new PokerGame(players);
            _game.OnGameEvent += LogGameEvent;
            _game.OnPlayerDecision += HandlePlayerDecision;
            
            // Start the game
            _game.StartGame();
            StartGameButton.IsEnabled = false;
            NextRoundButton.IsEnabled = true;
            
            UpdateUI();
        }

        private void NextRound()
        {
            // This would typically be handled by the game logic
            // For simplicity, we'll just simulate a new round
            _game.ResetRound();
            UpdateUI();
        }

        private void MakeDecision(string decision)
        {
            // In a real implementation, this would be handled through the game logic
            LogGameEvent($"You decided to {decision}");
            // Simulate AI decisions
            SimulateAIDecisions();
        }

        private void MakeRaise()
        {
            if (int.TryParse(RaiseAmountTextBox.Text, out int amount))
            {
                LogGameEvent($"You raised by {amount}");
                // Simulate AI decisions
                SimulateAIDecisions();
            }
        }

        private void MakeAllIn()
        {
            LogGameEvent("You went all in!");
            // Simulate AI decisions
            SimulateAIDecisions();
        }

        private void SimulateAIDecisions()
        {
            // Simple simulation of AI decisions
            foreach (var player in _game.GetPlayers().Where(p => p is AIPlayer))
            {
                LogGameEvent($"{player.Name} decided to call");
            }
        }

        private void HandlePlayerDecision(Player player, int currentBet, int minRaise)
        {
            // Update UI based on player decision
            Dispatcher.Invoke(() =>
            {
                CurrentBet = currentBet;
                MinRaise = minRaise;
                UpdateUI();
            });
        }

        private void LogGameEvent(string message)
        {
            Dispatcher.Invoke(() =>
            {
                GameLogTextBox.AppendText($"{DateTime.Now:T}: {message}\n");
                GameLogTextBox.ScrollToEnd();
            });
        }

        private void UpdateUI()
        {
            if (_game == null) return;

            // Update community cards
            CommunityCards.Clear();
            foreach (var card in _game.GetCommunityCards())
            {
                CommunityCards.Add(card);
            }

            // Update player hand
            PlayerHand.Clear();
            foreach (var card in _humanPlayer.Hand.Cards)
            {
                PlayerHand.Add(card);
            }

            // Update opponents
            Opponents.Clear();
            foreach (var player in _game.GetPlayers().Where(p => p != _humanPlayer))
            {
                Opponents.Add(player);
            }

            // Update player info
            PlayerNameText.Text = _humanPlayer.Name;
            PlayerChipsText.Text = $"Chips: {_humanPlayer.Chips}";
            PlayerBetText.Text = $"Current Bet: {_humanPlayer.CurrentBet}";

            // Update game info
            Pot = _game.GetPot();
            CurrentBet = _game.GetCurrentBet();
            MinRaise = _game.GetMinRaise();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    // Simplified versions of the classes for demonstration
    public enum Suit { Hearts, Diamonds, Clubs, Spades }
    public enum Rank { Two = 2, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Jack, Queen, King, Ace }

    public class Card
    {
        public Suit Suit { get; set; }
        public Rank Rank { get; set; }
        
        public string Emoji
        {
            get
            {
                string suitEmoji = Suit switch
                {
                    Suit.Hearts => "♥",
                    Suit.Diamonds => "♦",
                    Suit.Clubs => "♣",
                    Suit.Spades => "♠",
                    _ => "?"
                };

                string rankSymbol = Rank switch
                {
                    Rank.Jack => "J",
                    Rank.Queen => "Q",
                    Rank.King => "K",
                    Rank.Ace => "A",
                    _ => ((int)Rank).ToString()
                };

                return $"{rankSymbol}{suitEmoji}";
            }
        }

        public Card(Suit suit, Rank rank)
        {
            Suit = suit;
            Rank = rank;
        }

        public override string ToString() => $"{Rank} of {Suit}";
    }

    public class Deck
    {
        private List<Card> _cards = new List<Card>();
        private Random _random = new Random();

        public Deck()
        {
            Reset();
        }

        public void Reset()
        {
            _cards.Clear();
            foreach (Suit suit in Enum.GetValues(typeof(Suit)))
            {
                foreach (Rank rank in Enum.GetValues(typeof(Rank)))
                {
                    _cards.Add(new Card(suit, rank));
                }
            }
            Shuffle();
        }

        public void Shuffle()
        {
            for (int i = _cards.Count - 1; i > 0; i--)
            {
                int j = _random.Next(i + 1);
                (_cards[i], _cards[j]) = (_cards[j], _cards[i]);
            }
        }

        public Card DealCard()
        {
            if (_cards.Count == 0) Reset();
            var card = _cards[0];
            _cards.RemoveAt(0);
            return card;
        }

        public int CardsRemaining => _cards.Count;
    }

    public class Hand
    {
        private List<Card> _cards = new List<Card>();
        public IReadOnlyList<Card> Cards => _cards.AsReadOnly();

        public void AddCard(Card card) => _cards.Add(card);
        public void Clear() => _cards.Clear();
        public override string ToString() => string.Join(", ", _cards.Select(c => c.ToString()));
    }

    public abstract class Player
    {
        public string Name { get; set; }
        public Hand Hand { get; set; } = new Hand();
        public int Chips { get; set; }
        public bool IsFolded { get; set; }
        public int CurrentBet { get; set; }
        public string Status => IsFolded ? "Folded" : "Active";

        protected Player(string name, int chips)
        {
            Name = name;
            Chips = chips;
        }

        public void ReceiveCard(Card card) => Hand.AddCard(card);
        public void Fold() => IsFolded = true;
        
        public void ResetForNewRound()
        {
            Hand.Clear();
            IsFolded = false;
            CurrentBet = 0;
        }

        public bool PlaceBet(int amount)
        {
            if (amount > Chips) return false;
            Chips -= amount;
            CurrentBet += amount;
            return true;
        }

        public void AddChips(int amount) => Chips += amount;
        public abstract string MakeDecision(int currentBet, int minRaise);
    }

    public class HumanPlayer : Player
    {
        public HumanPlayer(string name, int chips) : base(name, chips) { }
        
        public override string MakeDecision(int currentBet, int minRaise)
        {
            // This would be handled through UI interaction
            return "call"; // Default decision
        }
    }

    public class AIPlayer : Player
    {
        private Random _random = new Random();
        
        public AIPlayer(string name, int chips) : base(name, chips) { }
        
        public override string MakeDecision(int currentBet, int minRaise)
        {
            // Simple AI logic
            var decisions = new[] { "fold", "check", "call", "raise" };
            return decisions[_random.Next(decisions.Length)];
        }
        
        private double EvaluateHandStrength() => _random.NextDouble();
    }

    public class PokerGame
    {
        private List<Player> _players;
        private Deck _deck;
        private List<Card> _communityCards = new List<Card>();
        private int _pot;
        private int _currentBet;
        private int _minRaise;
        private int _bigBlind = 20;
        
        public event Action<string> OnGameEvent;
        public event Action<Player, int, int> OnPlayerDecision;

        public PokerGame(List<Player> players)
        {
            _players = players;
            _deck = new Deck();
        }

        public void StartGame()
        {
            OnGameEvent?.Invoke("Game started!");
            ResetRound();
        }

        public void ResetRound()
        {
            _deck.Reset();
            _communityCards.Clear();
            _pot = 0;
            _currentBet = 0;
            _minRaise = _bigBlind;
            
            foreach (var player in _players)
            {
                player.ResetForNewRound();
            }
            
            DealCards();
            PostBlinds();
            
            OnGameEvent?.Invoke("New round started");
        }

        private void DealCards()
        {
            foreach (var player in _players)
            {
                player.ReceiveCard(_deck.DealCard());
                player.ReceiveCard(_deck.DealCard());
            }
        }

        private void PostBlinds()
        {
            // Simple implementation - just have first two players post blinds
            if (_players.Count >= 2)
            {
                _players[0].PlaceBet(_bigBlind / 2); // Small blind
                _players[1].PlaceBet(_bigBlind); // Big blind
                _pot += _bigBlind * 3 / 2;
                _currentBet = _bigBlind;
            }
        }

        public List<Card> GetCommunityCards() => _communityCards;
        public int GetPot() => _pot;
        public int GetCurrentBet() => _currentBet;
        public int GetMinRaise() => _minRaise;
        public List<Player> GetPlayers() => _players;
    }
}
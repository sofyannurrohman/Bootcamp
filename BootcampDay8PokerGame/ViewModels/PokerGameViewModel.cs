// ViewModels/PokerGameViewModel.cs
using BootcampDay8.PokerGame.Core;
using BootcampDay8.PokerGame.Enums;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace BootcampDay8.PokerGame.ViewModels
{
    public class PokerGameViewModel : ViewModelBase
    {
        private BootcampDay8.PokerGame.Core.PokerGame _game; // Fully qualified name to avoid namespace confusion

        public ObservableCollection<PlayerViewModel> Players { get; } = new ObservableCollection<PlayerViewModel>();
        public ICommand DealCommand { get; }
        public ICommand BetCommand { get; }
        public ICommand FoldCommand { get; }
        public ICommand DiscardCommand { get; }
        public ICommand CallCommand { get; }
        public ICommand RaiseCommand { get; }
        private int _raiseAmount = 10; // default
        public int RaiseAmount
        {
            get => _raiseAmount;
            set
            {
                if (_raiseAmount != value)
                {
                    _raiseAmount = value;
                    OnPropertyChanged(nameof(RaiseAmount));
                }
            }
        }

        public int Pot
        {
            get => _game.Pot;
        }

        public GameState State
        {
            get => _game.State;
        }

        public PlayerViewModel? CurrentPlayer { get; private set; }

        public PokerGameViewModel(BootcampDay8.PokerGame.Core.PokerGame game)
        {
            _game = game;
            _game.GameStateChanged += OnGameStateChanged;

            // Initialize commands
            DealCommand = new RelayCommand(Deal, CanDeal);
            BetCommand = new RelayCommand(Bet, CanBet);
            RaiseCommand = new RelayCommand(Raise, CanRaise);
            FoldCommand = new RelayCommand(Fold, CanFold);
            DiscardCommand = new RelayCommand(Discard, CanDiscard);
            BetCommand = new RelayCommand(Bet, CanBet);


            Initialize();
        }

        public void Initialize()
        {
            Players.Clear();
            foreach (var player in _game.Players)
            {
                var playerVM = new PlayerViewModel(player);
                Players.Add(playerVM);
            }
        }

        private void OnGameStateChanged(GameState newState)
        {
            OnPropertyChanged(nameof(State));
            UpdateView();
        }

        private void UpdateView()
        {
            foreach (var player in Players)
            {
                player.UpdateFromModel();
            }
            OnPropertyChanged(nameof(Pot));
        }

        private void Deal()
        {
            try
            {
                _game.StartNewGame();
                _game.DealInitialCards();

                // Transition game state to betting
                _game.State = GameState.Betting;  // <-- if PokerGame.State has setter
                CurrentPlayer = Players.FirstOrDefault();

                UpdateView();
                CommandManager.InvalidateRequerySuggested(); // refresh command states
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.ToString(), "Deal Error");
            }
        }


        private bool CanDeal()
        {
            return State == GameState.PreGame || State == GameState.GameOver;
        }

        private void Bet()
        {
            try
            {
                if (CurrentPlayer == null) return;

                int betAmount = 10; // for now, fixed bet

                // Call game engine
                _game.PlayerAction(BootcampDay8.PokerGame.Enums.PlayerAction.Bet, betAmount);

                // Update the CurrentPlayer reference
                CurrentPlayer = Players.FirstOrDefault(p => p.Model == _game.CurrentPlayer);

                // Refresh ALL players from model
                foreach (var p in Players)
                    p.UpdateFromModel();

                OnPropertyChanged(nameof(CurrentPlayer));
                OnPropertyChanged(nameof(Pot));

                CommandManager.InvalidateRequerySuggested();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.ToString(), "Bet Error");
            }
        }


        private bool CanBet()
        {
            return State == GameState.Betting && CurrentPlayer != null;
        }

        private void Fold()
        {
            // Implementation for folding
        }

        private bool CanFold()
        {
            return State == GameState.Betting && CurrentPlayer != null;
        }
        private void Raise()
        {
            try
            {
                if (CurrentPlayer == null) return;

                // Use the bound RaiseAmount
                int raiseAmount = _game.CurrentBet + RaiseAmount;

                _game.PlayerAction(PlayerAction.Raise, raiseAmount);

                // Update turn & UI
                CurrentPlayer = Players.FirstOrDefault(p => p.Model == _game.CurrentPlayer);

                foreach (var p in Players)
                    p.UpdateFromModel();

                OnPropertyChanged(nameof(CurrentPlayer));
                OnPropertyChanged(nameof(Pot));
                CommandManager.InvalidateRequerySuggested();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.ToString(), "Raise Error");
            }
        }



        private bool CanRaise()
        {
            return State == GameState.Betting
                && CurrentPlayer != null
                && _game.CurrentBet > 0;
        }

        private void Discard()
        {
            // Implementation for discarding
        }

        private bool CanDiscard()
        {
            return State == GameState.Discarding && CurrentPlayer != null;
        }
    }
}
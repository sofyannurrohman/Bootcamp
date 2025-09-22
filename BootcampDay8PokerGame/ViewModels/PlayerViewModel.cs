// ViewModels/PlayerViewModel.cs
using BootcampDay8.PokerGame.Core;
using System.Collections.ObjectModel;

namespace BootcampDay8.PokerGame.ViewModels
{
    public class PlayerViewModel : ViewModelBase
    {
        private Player _player;
        
        public string Name => _player.Name;
        public int Chips => _player.Chips;
        public ObservableCollection<CardViewModel> Hand { get; } = new ObservableCollection<CardViewModel>();
        public bool IsActive => _player.IsActive;
        public bool HasFolded => _player.HasFolded;
        public Player Model => _player;
        public int CurrentBet => _player.CurrentBet;


        public PlayerViewModel(Player player)
        {
            _player = player;
            UpdateFromModel();
        }

        public void UpdateFromModel()
        {
            OnPropertyChanged(nameof(Chips));
            OnPropertyChanged(nameof(IsActive));
            OnPropertyChanged(nameof(HasFolded));
            OnPropertyChanged(nameof(CurrentBet));
            
            Hand.Clear();
            foreach (var card in _player.Hand.Cards)
            {
                Hand.Add(new CardViewModel(card));
            }
        }
    }
}
// ViewModels/CardViewModel.cs
using BootcampDay8.PokerGame.Core;
using BootcampDay8.PokerGame.Enums;
using System.Windows.Input;

namespace BootcampDay8.PokerGame.ViewModels
{
    public class CardViewModel : ViewModelBase
    {
        private Card _card;
        private bool _isSelected;
        
        public Suit Suit => _card.Suit;
        public Rank Rank => _card.Rank;
        public bool IsFaceUp
        {
            get => _card.IsFaceUp;
            set
            {
                _card.IsFaceUp = value;
                OnPropertyChanged();
            }
        }
        public string ImagePath => _card.ImagePath;
        public ICommand SelectCommand { get; }
        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                _isSelected = value;
                OnPropertyChanged();
            }
        }

        public CardViewModel(Card card)
        {
            _card = card;
            SelectCommand = new RelayCommand(SelectCard);
        }

        private void SelectCard()
        {
            IsSelected = !IsSelected;
        }
    }
}
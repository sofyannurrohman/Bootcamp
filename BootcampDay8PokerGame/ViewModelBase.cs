// ViewModelBase.cs (base class for all view models)
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BootcampDay8.PokerGame
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
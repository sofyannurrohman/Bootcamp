// Converters/CardImageConverter.cs
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace BootcampDay8.PokerGame.Converters
{
    public class CardImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is BootcampDay8.PokerGame.Core.Card card)
            {
                if (!card.IsFaceUp)
                {
                    // Return card back image
                    return new BitmapImage(new Uri("pack://application:,,,/BootcampDayPokerGame;component/Assets/Cards/Card_Back.png"));
                }

                try
                {
                    // Try to load the specific card image
                    return new BitmapImage(new Uri($"pack://application:,,,/{card.ImagePath}"));
                }
                catch
                {
                    // Fallback to card back if specific image fails
                    return new BitmapImage(new Uri("pack://application:,,,/BootcampDay8PokerGame;component/Assets/Cards/Card_Back.png"));
                }
            }

            // Default card back
            return new BitmapImage(new Uri("pack://application:,,,/BootcampDay8PokerGame;component/Assets/Cards/Card_Back.png"));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
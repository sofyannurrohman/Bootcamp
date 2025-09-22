// Converters/ImageFallbackConverter.cs
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace BootcampDay8.PokerGame.Converters
{
    public class ImageFallbackConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string imagePath)
            {
                try
                {
                    var bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri(imagePath, UriKind.RelativeOrAbsolute);
                    bitmap.CacheOption = BitmapCacheOption.OnLoad;
                    bitmap.EndInit();
                    return bitmap;
                }
                catch
                {
                    // Return a default card back image if the specific card image fails to load
                    return new BitmapImage(new Uri("/PokerGame;component/Assets/Cards/Card_Back.png", UriKind.RelativeOrAbsolute));
                }
            }
            return new BitmapImage(new Uri("/PokerGame;component/Assets/Cards/Card_Back.png", UriKind.RelativeOrAbsolute));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
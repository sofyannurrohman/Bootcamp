using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace BootcampDay8.PokerGame.Converters
{
    public class ActivePlayerConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool isActive && isActive)
            {
                return new SolidColorBrush(Colors.LightGreen);
            }
            return new SolidColorBrush(Colors.Transparent);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
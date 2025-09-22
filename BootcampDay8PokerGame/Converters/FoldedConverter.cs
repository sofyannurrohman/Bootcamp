// Converters/FoldedConverter.cs
using System;
using System.Globalization;
using System.Windows.Data;

namespace BootcampDay8.PokerGame.Converters
{
    public class FoldedConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool hasFolded && hasFolded)
            {
                return "FOLDED";
            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
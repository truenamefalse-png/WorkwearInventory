using System;
using System.Globalization;
using System.Windows.Data;

namespace WorkwearInventory.Converters
{
    public class NullToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value == null; // true, если DateReturned == null -> кнопка активна
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}
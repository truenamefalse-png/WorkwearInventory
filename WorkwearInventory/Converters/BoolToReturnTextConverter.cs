using System;
using System.Globalization;
using System.Windows.Data;

namespace WorkwearInventory.Converters
{
    public class BoolToReturnTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return "Вернуть";
            return "Возвращено";
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}
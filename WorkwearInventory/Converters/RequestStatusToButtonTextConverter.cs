using System;
using System.Globalization;
using System.Windows.Data;

namespace WorkwearInventory.Converters
{
    public class RequestStatusToButtonTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string status && status == "Новая") return "Выполнить";
            return "Выполнена";
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}
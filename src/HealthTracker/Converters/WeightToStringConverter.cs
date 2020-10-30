using System;
using System.Globalization;
using Xamarin.Forms;

namespace HealthTracker.Converters
{
    public class WeightToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((decimal)value).ToString("##0.0");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return decimal.TryParse((string)value, out var result) ? result : 0.0m;
        }
    }
}

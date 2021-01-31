using System;
using System.Globalization;
using Xamarin.Forms;

namespace HealthTracker.Converters
{
    public class DaysSinceToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is int days))
                return string.Empty;

            if (days == int.MaxValue)
                return "-";

            return days.ToString("0");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
using System;
using System.Globalization;
using Xamarin.Forms;

namespace HealthTracker.Converters
{
    public class AverageDaysToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is double days))
                return string.Empty;

            if (days == double.MaxValue)
                return "-";

            return days.ToString("0.0");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

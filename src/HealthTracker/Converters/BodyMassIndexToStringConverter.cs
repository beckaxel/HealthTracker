using System;
using System.Globalization;
using Xamarin.Forms;

namespace HealthTracker.Converters
{
    public class BodyMassIndexToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is float weight) || weight == default)
                return "-";
            return Math.Round((float)value, 1, MidpointRounding.AwayFromZero).ToString("##0.0");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return float.TryParse((string)value, out var result) ? result : 0f;
        }
    }
}

using System;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace HealthTracker.Converters
{
    public class TrendValueToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is float floatValue))
                return "-";
            var intValue = System.Convert.ToInt32(Math.Round(floatValue * 1000, 0, MidpointRounding.AwayFromZero));

            var stringValue = new StringBuilder();
            if (intValue > 0)
                stringValue.Append('+');
            else if (intValue == 0)
                stringValue.Append('±');
            else
                stringValue.Append('-');
            stringValue.Append(Math.Abs(intValue));
            return stringValue.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

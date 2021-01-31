using System;
using System.Globalization;
using Xamarin.Forms;

namespace HealthTracker.Converters
{
    public class BooleanToCheckBoxGlyphConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is bool flag && flag ? "\uf14a" : "\uf0c8";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

using System;
using System.Globalization;
using HealthTracker.ViewModels;
using Xamarin.Forms;

namespace HealthTracker.Converters
{
    public class TrendDirectionToRotationAngleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is TrendDirection trend))
                return 0d;

            switch (trend)
            {
                case TrendDirection.FastDown:
                    return 90d;
                case TrendDirection.SlowDown:
                    return 45d;
                case TrendDirection.SlowUp:
                    return -45d;
                case TrendDirection.FastUp:
                    return -90d;
            }

            return 0d;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

using System;
using System.Globalization;
using Xamarin.Forms;

namespace HealthTracker.Converters
{
    public class LongToFileSizeStringConverter : IValueConverter
    {
        private string[] FileSizeFormats = new[]
        {
            "{0} B",
            "{0:0.#} KB",
            "{0:0.##} MB",
            "{0:0.##} TB"
        };

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var index = 0;
            var size = System.Convert.ToDouble(value);
            while (size >= 1024d && index++ < FileSizeFormats.Length - 1)
                size /= 1024d;
            return string.Format(FileSizeFormats[index], Math.Round(size, MidpointRounding.AwayFromZero));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

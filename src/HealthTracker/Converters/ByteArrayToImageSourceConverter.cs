using System;
using System.Globalization;
using System.IO;
using Xamarin.Forms;

namespace HealthTracker.Converters
{
    public class ByteArrayToImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is byte[] imageBytes))
                return null;

            return ImageSource.FromStream(() => new MemoryStream(imageBytes));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

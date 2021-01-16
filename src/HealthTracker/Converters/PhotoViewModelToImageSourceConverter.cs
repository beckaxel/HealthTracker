using System;
using System.Globalization;
using System.IO;
using HealthTracker.ViewModels;
using Xamarin.Forms;

namespace HealthTracker.Converters
{
    public class PhotoViewModelToImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is PhotoViewModel photo))
                return null;

            if (photo.OriginDirectoryName == null || photo.FileName == null)
                return null;

            return ImageSource.FromFile(Path.Combine(photo.OriginDirectoryName, photo.FileName));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

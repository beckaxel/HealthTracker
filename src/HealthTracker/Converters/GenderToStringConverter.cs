using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using HealthTracker.Models;
using Xamarin.Forms;

namespace HealthTracker.Converters
{
    public class GenderToStringConverter : IValueConverter
    {
        private static readonly Dictionary<string, Gender> Translation = new Dictionary<string, Gender>
        {
            { "Keine Angabe", Gender.None },
            { "weiblich", Gender.Female },
            { "männlich", Gender.Male }
        };

        public static List<string> Values => Translation.Keys.ToList();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var gender = value as Gender? ?? Gender.None;
            if (!Translation.ContainsValue(gender))
                gender = Gender.None;
            return Translation.First(t => t.Value == gender).Key;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var key = (string)value;
            if (!Translation.ContainsKey(key))
                return Gender.None;
            return Translation[key];
        }
    }
}

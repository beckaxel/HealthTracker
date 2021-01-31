using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using HealthTracker.Models;
using Xamarin.Forms;

namespace HealthTracker.Converters
{
    public class DietToStringConverter : IValueConverter
    {
        private static readonly Dictionary<string, Diet> Translation = new Dictionary<string, Diet>
        {
            { "Fleisch", Diet.Meat },
            { "Fisch", Diet.Fish },
            { "Vegetarisch", Diet.Vegetarian },
            { "Vegan", Diet.Vegan }
        };

        public static List<string> Values => Translation.Keys.ToList();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var diet = value as Diet? ?? Diet.Vegan;
            if (!Translation.ContainsValue(diet))
                diet = Diet.Vegan;
            return Translation.First(t => t.Value == diet).Key;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var key = (string)value;
            if (!Translation.ContainsKey(key))
                return Diet.Vegan;
            return Translation[key];
        }
    }
}

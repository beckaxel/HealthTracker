using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using HealthTracker.Models;
using Xamarin.Forms;

namespace HealthTracker.Converters
{
    public class MealTypeToStringConverter : IValueConverter
    {
        private static readonly Dictionary<string, MealType> Translation = new Dictionary<string, MealType>
        {
            { "Snack", MealType.Snack },
            { "Frühstück", MealType.Breakfast },
            { "Mittagessen", MealType.Lunch },
            { "Abendessen", MealType.Dinner }
        };

        public static List<string> Values => Translation.Keys.ToList();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var mealType = value as MealType? ?? MealType.Snack;
            if (!Translation.ContainsValue(mealType))
                mealType = MealType.Snack;
            return Translation.First(t => t.Value == mealType).Key;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var key = (string)value;
            if (!Translation.ContainsKey(key))
                return MealType.Snack;
            return Translation[key];
        }
    }
}

using System;
using System.Linq;
using HealthTracker.Models;

namespace HealthTracker.Storage
{
    public static class MealQueries
    {
        public static Meal LatestOrDefault(this IQueryable<Meal> source)
        {
            return source.OrderByDescending(m => m.EatingTime).FirstOrDefault();
        }

        public static IQueryable<Meal> Filter(this IQueryable<Meal> source, string name)
        {
            var fromDate = MVVM.Filter.Translate(name);
            return source.Where(m => m.EatingTime >= fromDate);
        }

        public static int GetDaysSinceLast(this IQueryable<Meal> source, Diet diet)
        {
            var date = source
                .GroupBy(m => m.EatingTime.Date)
                .OrderByDescending(g => g.Key)
                .Where(g => g.Max(m => m.Diet) == diet)
                .Select(g => g.Key)
                .FirstOrDefault();

            if (date == default)
                return int.MaxValue;
            
            return Convert.ToInt32(Math.Floor((DateTime.UtcNow - date).TotalDays));
        }

        public static double GetAverageDaysBetweenMeals(this IQueryable<Meal> source, Diet diet)
        {
            var dates = source
                .GroupBy(m => m.EatingTime.Date)
                .OrderBy(g => g.Key)
                .Where(g => g.Max(m => m.Diet) == diet)
                .Select(g => g.Key)
                .ToList();

            var diffs = dates
                .Distinct()
                .Select((date, idx) => idx <= 0 ? (double?)null : date.Subtract(dates[idx - 1]).TotalDays)
                .Where(d => d.HasValue)
                .Select(d => d.Value)
                .ToList();

            if (diffs.Count == 0)
                return double.MaxValue;

            return diffs.Average();
        }
    }
}

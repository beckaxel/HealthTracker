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

        public static IQueryable<Meal> LastXDays(this IQueryable<Meal> source, int days)
        {
            var fromDate = DateTime.Today.Subtract(TimeSpan.FromDays(days));
            return source.Where(m => m.EatingTime >= fromDate);
        }
    }
}

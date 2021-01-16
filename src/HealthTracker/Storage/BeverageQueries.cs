using System;
using System.Linq;
using HealthTracker.Models;

namespace HealthTracker.Storage
{
    public static class BeverageQueries
    {
        public static IQueryable<Beverage> Today(this IQueryable<Beverage> source)
        {
            var today = DateTime.Today;
            return source
                .Where(b => b.DrinkingTime >= today)
                .OrderByDescending(b => b.DrinkingTime);
        }

        public static Beverage LatestOrDefault(this IQueryable<Beverage> source)
        {
            return source.OrderByDescending(b => b.DrinkingTime).FirstOrDefault();
        }

        public static IQueryable<Beverage> LastXDays(this IQueryable<Beverage> source, int days)
        {
            var fromDate = DateTime.Today.Subtract(TimeSpan.FromDays(days));
            return source
                .OrderByDescending(b => b.DrinkingTime)
                .Where(b => b.DrinkingTime >= fromDate);
        }
    }
}

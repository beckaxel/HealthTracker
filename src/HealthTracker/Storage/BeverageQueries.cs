using System;
using System.Linq;
using HealthTracker.Models;

namespace HealthTracker.Storage
{
    public static class BeverageQueries
    {
        public static float AmountToday(this IQueryable<Beverage> source)
        {
            var today = DateTime.Today;
            return source.Where(b => b.DrinkingTime >= today).Sum(b => b.Amount);
        }

        public static Beverage LatestOrDefault(this IQueryable<Beverage> source)
        {
            return source.OrderByDescending(b => b.DrinkingTime).FirstOrDefault();
        }
    }
}

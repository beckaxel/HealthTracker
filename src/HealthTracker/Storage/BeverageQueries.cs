using System;
using System.Linq;
using HealthTracker.Models;

namespace HealthTracker.Storage
{
    public static class BeverageQueries
    {
        public static Beverage LatestOrDefault(this IQueryable<Beverage> source)
        {
            return source.OrderByDescending(b => b.DrinkingTime).FirstOrDefault();
        }

        public static IQueryable<Beverage> Filter(this IQueryable<Beverage> source, string name)
        {
            var fromDate = MVVM.Filter.Translate(name);
            return source.Where(b => b.DrinkingTime >= fromDate);
        }
    }
}

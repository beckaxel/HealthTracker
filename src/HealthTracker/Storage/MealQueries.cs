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
    }
}

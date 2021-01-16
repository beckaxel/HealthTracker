using System;
using System.Collections.Generic;
using System.Linq;
using HealthTracker.Models;
using HealthTracker.ViewModels;

namespace HealthTracker.Storage
{
    public static class BeverageProjections
    {
        public static List<DrinkingQuantityPerDayViewModel> SelectIntoDrinkingQuantityPerDayViewModel(this IQueryable<Beverage> source, HealthTrackerDbContext healthTrackerDbContext)
        {

            return source
                .GroupBy(b => b.DrinkingTime.Date)
                .OrderByDescending(g => g.Key)
                .Select(g => new DrinkingQuantityPerDayViewModel
                {
                    Day = g.Key,
                    Quantity = g.Sum(g => g.Quantity),
                    DrinkingQuantityPerDay = healthTrackerDbContext.User.Select(u => u.DailyDrinkingQuantity).FirstOrDefault()
                })
                .ToList();
        }
    }
}

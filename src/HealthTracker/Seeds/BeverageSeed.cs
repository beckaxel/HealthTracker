using System;
using System.Linq;
using HealthTracker.Models;
using HealthTracker.Storage;

namespace HealthTracker.Seeds
{
    public static class BeverageSeed
    {
        public static void Seed(HealthTrackerDbContext healthTrackerDbContext)
        {
            if (healthTrackerDbContext.Beverage.Any())
                return;

            healthTrackerDbContext.Add(new Beverage { DrinkingTime = DateTime.Today.AddHours(9), Quantity = 150f });
            healthTrackerDbContext.Add(new Beverage { DrinkingTime = DateTime.Today.AddHours(10), Quantity = 200f });
            healthTrackerDbContext.Add(new Beverage { DrinkingTime = DateTime.Today.AddHours(11), Quantity = 250f });
            healthTrackerDbContext.Add(new Beverage { DrinkingTime = DateTime.Today.AddHours(14), Quantity = 200f });
            healthTrackerDbContext.Add(new Beverage { DrinkingTime = DateTime.Today.AddHours(15), Quantity = 250f });
            healthTrackerDbContext.Add(new Beverage { DrinkingTime = DateTime.Today.AddDays(-1).AddHours(9), Quantity = 250f });
            healthTrackerDbContext.Add(new Beverage { DrinkingTime = DateTime.Today.AddDays(-1).AddHours(11), Quantity = 250f });
            healthTrackerDbContext.Add(new Beverage { DrinkingTime = DateTime.Today.AddDays(-1).AddHours(12), Quantity = 250f });
            healthTrackerDbContext.Add(new Beverage { DrinkingTime = DateTime.Today.AddDays(-1).AddHours(13), Quantity = 250f });
            healthTrackerDbContext.Add(new Beverage { DrinkingTime = DateTime.Today.AddDays(-1).AddHours(15), Quantity = 250f });
            healthTrackerDbContext.Add(new Beverage { DrinkingTime = DateTime.Today.AddDays(-1).AddHours(17), Quantity = 250f });
            healthTrackerDbContext.Add(new Beverage { DrinkingTime = DateTime.Today.AddDays(-1).AddHours(19), Quantity = 250f });
            healthTrackerDbContext.Add(new Beverage { DrinkingTime = DateTime.Today.AddDays(-1).AddHours(21), Quantity = 250f });
            healthTrackerDbContext.Add(new Beverage { DrinkingTime = DateTime.Today.AddDays(-2).AddHours(8), Quantity = 250f });
            healthTrackerDbContext.Add(new Beverage { DrinkingTime = DateTime.Today.AddDays(-2).AddHours(10), Quantity = 250f });
            healthTrackerDbContext.Add(new Beverage { DrinkingTime = DateTime.Today.AddDays(-2).AddHours(12), Quantity = 250f });
            healthTrackerDbContext.Add(new Beverage { DrinkingTime = DateTime.Today.AddDays(-2).AddHours(13), Quantity = 250f });
            healthTrackerDbContext.Add(new Beverage { DrinkingTime = DateTime.Today.AddDays(-2).AddHours(16), Quantity = 250f });
            healthTrackerDbContext.Add(new Beverage { DrinkingTime = DateTime.Today.AddDays(-2).AddHours(17), Quantity = 250f });
            healthTrackerDbContext.Add(new Beverage { DrinkingTime = DateTime.Today.AddDays(-2).AddHours(19), Quantity = 250f });
            healthTrackerDbContext.Add(new Beverage { DrinkingTime = DateTime.Today.AddDays(-2).AddHours(20), Quantity = 250f });

            healthTrackerDbContext.SaveChanges();
        }
    }
}

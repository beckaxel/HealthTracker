using System;
using HealthTracker.Models;
using HealthTracker.Storage;

namespace HealthTracker.Seeds
{
    public static class DrinkingSeed
    {
        public static void Seed(IDrinkingStorage drinkingStorage)
        {
            drinkingStorage.Insert(new Beverage { Date = DateTime.Today.AddHours(9), Amount = 150f });
            drinkingStorage.Insert(new Beverage { Date = DateTime.Today.AddHours(10), Amount = 200f });
            drinkingStorage.Insert(new Beverage { Date = DateTime.Today.AddHours(11), Amount = 250f });
            drinkingStorage.Insert(new Beverage { Date = DateTime.Today.AddHours(14), Amount = 200f });
            drinkingStorage.Insert(new Beverage { Date = DateTime.Today.AddHours(15), Amount = 250f });
            drinkingStorage.Insert(new Beverage { Date = DateTime.Today.AddDays(-1).AddHours(9), Amount = 250f });
            drinkingStorage.Insert(new Beverage { Date = DateTime.Today.AddDays(-1).AddHours(11), Amount = 250f });
            drinkingStorage.Insert(new Beverage { Date = DateTime.Today.AddDays(-1).AddHours(12), Amount = 250f });
            drinkingStorage.Insert(new Beverage { Date = DateTime.Today.AddDays(-1).AddHours(13), Amount = 250f });
            drinkingStorage.Insert(new Beverage { Date = DateTime.Today.AddDays(-1).AddHours(15), Amount = 250f });
            drinkingStorage.Insert(new Beverage { Date = DateTime.Today.AddDays(-1).AddHours(17), Amount = 250f });
            drinkingStorage.Insert(new Beverage { Date = DateTime.Today.AddDays(-1).AddHours(19), Amount = 250f });
            drinkingStorage.Insert(new Beverage { Date = DateTime.Today.AddDays(-1).AddHours(21), Amount = 250f });
            drinkingStorage.Insert(new Beverage { Date = DateTime.Today.AddDays(-2).AddHours(8), Amount = 250f });
            drinkingStorage.Insert(new Beverage { Date = DateTime.Today.AddDays(-2).AddHours(10), Amount = 250f });
            drinkingStorage.Insert(new Beverage { Date = DateTime.Today.AddDays(-2).AddHours(12), Amount = 250f });
            drinkingStorage.Insert(new Beverage { Date = DateTime.Today.AddDays(-2).AddHours(13), Amount = 250f });
            drinkingStorage.Insert(new Beverage { Date = DateTime.Today.AddDays(-2).AddHours(16), Amount = 250f });
            drinkingStorage.Insert(new Beverage { Date = DateTime.Today.AddDays(-2).AddHours(17), Amount = 250f });
            drinkingStorage.Insert(new Beverage { Date = DateTime.Today.AddDays(-2).AddHours(19), Amount = 250f });
            drinkingStorage.Insert(new Beverage { Date = DateTime.Today.AddDays(-2).AddHours(20), Amount = 250f });
        }
    }
}

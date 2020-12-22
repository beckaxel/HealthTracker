using System;
using System.Collections.Generic;
using System.Linq;
using HealthTracker.Models;
using HealthTracker.Seeds;

namespace HealthTracker.Storage.Impl
{
    public class EFDrinkingStorage : IDrinkingStorage
    {
        private readonly IHealthTrackerDbContextFactory _healthTrackerDbContextFactory;

        public EFDrinkingStorage(IHealthTrackerDbContextFactory healthTrackerDbContextFactory)
        {
            _healthTrackerDbContextFactory = healthTrackerDbContextFactory;
            using var context = _healthTrackerDbContextFactory.Create();
            if (!context.Drinking.Any())
                DrinkingSeed.Seed(this);
        }

        public IEnumerable<Beverage> All()
        {
            using var context = _healthTrackerDbContextFactory.Create();
            return context.Drinking.ToList();
        }

        public float AmountToday()
        {
            var today = DateTime.Today;
            using var context = _healthTrackerDbContextFactory.Create();
            return context.Drinking.Where(d => d.Date >= today).Sum(d => d.Amount);
        }

        public Beverage Find(int id)
        {
            using var context = _healthTrackerDbContextFactory.Create();
            return context.Find<Beverage>(id);
        }

        public void Insert(Beverage drinking)
        {
            using (var context = _healthTrackerDbContextFactory.Create())
            {
                context.Add(drinking);
                context.SaveChanges();
            }
        }

        public Beverage LatestOrDefault()
        {
            using var context = _healthTrackerDbContextFactory.Create();
            return context.Drinking
                .OrderByDescending(b => b.Date)
                .FirstOrDefault();
        }

        public void Remove(Beverage drinking)
        {
            using (var context = _healthTrackerDbContextFactory.Create())
            {
                var dbDrinking = context.Find<Beverage>(drinking.BeverageId);
                context.Remove(dbDrinking);
                context.SaveChanges();
            }
        }

        public void Update(Beverage drinking)
        {
            using (var context = _healthTrackerDbContextFactory.Create())
            {
                var dbDrinking = context.Find<Beverage>(drinking.BeverageId);
                if (dbDrinking == null)
                    return;

                dbDrinking.Date = drinking.Date;
                dbDrinking.Amount = drinking.Amount;
                context.SaveChanges();
            }
        }
    }
}

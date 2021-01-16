using System;
using System.IO;
using System.Linq;
using HealthTracker.Seeds;
using Microsoft.EntityFrameworkCore;
using Xamarin.Essentials;

namespace HealthTracker.Storage
{
    public class DbContextFactory : IDbContextFactory
    {
        public static string DatabasePath = Path.Combine(FileSystem.AppDataDirectory, "HealthTracker.db3");

        private bool _migrated = false;

        public HealthTrackerDbContext CreateHealthTrackerDbContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<HealthTrackerDbContext>();
            optionsBuilder.UseSqlite($"Filename={DatabasePath}");

            var healthTrackerDbContext = new HealthTrackerDbContext(optionsBuilder.Options);
            if (!_migrated)
                healthTrackerDbContext.Database.Migrate();

            UserSeed.Seed(healthTrackerDbContext);
            //BeverageSeed.Seed(healthTrackerDbContext);
            BodyMeasurementSeed.Seed(healthTrackerDbContext);

            foreach (var meal in healthTrackerDbContext.Meal.Include(m => m.Photos).Where(m => m.EatingTime <= new DateTime(2021, 1, 5)))
            {
                foreach (var photo in meal.Photos)
                    healthTrackerDbContext.Remove(photo);
                healthTrackerDbContext.Remove(meal);
            }

            return healthTrackerDbContext;
        }
    }
}

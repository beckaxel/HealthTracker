using System.IO;
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
            BeverageSeed.Seed(healthTrackerDbContext);
            BodyMeasurementSeed.Seed(healthTrackerDbContext);

            return healthTrackerDbContext;
        }
    }
}

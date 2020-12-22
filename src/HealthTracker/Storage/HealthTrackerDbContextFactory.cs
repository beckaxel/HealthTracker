using System.IO;
using Microsoft.EntityFrameworkCore;
using Xamarin.Essentials;

namespace HealthTracker.Storage
{
    public class HealthTrackerDbContextFactory : IHealthTrackerDbContextFactory
    {
        public static string DatabasePath = Path.Combine(FileSystem.AppDataDirectory, "HealthTracker.db3");

        private bool _migrated = false;

        public HealthTrackerDbContext Create()
        {
            var optionsBuilder = new DbContextOptionsBuilder<HealthTrackerDbContext>();
            optionsBuilder.UseSqlite($"Filename={DatabasePath}");

            var healthTrackerDbContext = new HealthTrackerDbContext(optionsBuilder.Options);
            if (!_migrated)
                healthTrackerDbContext.Database.Migrate();

            return healthTrackerDbContext;
        }
    }
}

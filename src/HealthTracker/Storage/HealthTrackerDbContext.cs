using System.IO;
using HealthTracker.Models;
using Microsoft.EntityFrameworkCore;
using Xamarin.Essentials;

namespace HealthTracker.Storage
{
    public class HealthTrackerDbContext : DbContext
    {
        public DbSet<Setting> Setting { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Tag> Tag { get; set; }
        public DbSet<Beverage> Beverage { get; set; }
        public DbSet<Meal> Meal { get; set; }
        public DbSet<BodyMeasurement> BodyMeasurement { get; set; }
        public DbSet<SleepLog> SleepLog { get; set; }

        public HealthTrackerDbContext(DbContextOptions options)
            : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
        }
    }
}

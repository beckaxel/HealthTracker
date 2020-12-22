using System;
using HealthTracker.Storage;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace HealthTracker.DummyApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }

    public class HealthTrackerDbContextFactory : IDesignTimeDbContextFactory<HealthTrackerDbContext>
    {
        public HealthTrackerDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<HealthTrackerDbContext>();
            optionsBuilder.UseSqlite("Data Source=HealthTracker.db");

            return new HealthTrackerDbContext(optionsBuilder.Options);
        }
    }
}

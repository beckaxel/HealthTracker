using System;
using System.Globalization;
using System.Linq;
using HealthTracker.Common;
using HealthTracker.Models;
using HealthTracker.Storage;

namespace HealthTracker.Seeds
{
    public static class UserSeed
    {
        public static void Seed(HealthTrackerDbContext healthTrackerDbContext)
        {
            if (healthTrackerDbContext.User.Any())
                return;

            healthTrackerDbContext.Add(new User { Gender = Gender.Male, Height = 165f, BirthDate = DateTime.Parse("1979-08-31 00:00:00", CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal | DateTimeStyles.AdjustToUniversal) });
            healthTrackerDbContext.SaveChanges();
        }
    }
}
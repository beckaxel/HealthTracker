using System;
using System.Globalization;
using HealthTracker.Common;
using HealthTracker.Storage;

namespace HealthTracker.Seeds
{
    public static class UserSeed
    {
        public static void Seed(IUserStorage userStorage)
        {
            var user = userStorage.GetOrAdd();
            user.BirthDate = DateTime.Parse("1979-08-31 00:00:00", CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal | DateTimeStyles.AdjustToUniversal);
            user.Height = 165m;
            user.Gender = Gender.Male;
            userStorage.Update(user);
        }
    }
}
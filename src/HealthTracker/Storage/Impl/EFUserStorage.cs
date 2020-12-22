using System;
using System.Linq;
using HealthTracker.Models;
using HealthTracker.Seeds;

namespace HealthTracker.Storage.Impl
{
    public class EFUserStorage : IUserStorage
    {
        private readonly IHealthTrackerDbContextFactory _healthTrackerDbContextFactory;

        public EFUserStorage(IHealthTrackerDbContextFactory healthTrackerDbContextFactory)
        {
            _healthTrackerDbContextFactory = healthTrackerDbContextFactory;
            using var context = _healthTrackerDbContextFactory.Create();
            if (!context.User.Any())
                UserSeed.Seed(this);
            
        }

        public User GetOrAdd()
        {
            using (var context = _healthTrackerDbContextFactory.Create())
            {
                var user = context.User.FirstOrDefault();
                if (user == null)
                {
                    user = new User();
                    context.Add(user);
                    context.SaveChanges();
                }
                return user;
            }
        }

        public void Update(User user)
        {
            using (var context = _healthTrackerDbContextFactory.Create())
            {
                var dbUser = context.User.Find(user.UserId);
                if (dbUser == null)
                    return;

                dbUser.Height = user.Height;
                dbUser.BirthDate = user.BirthDate;
                dbUser.Gender = user.Gender;

                context.SaveChanges();
            }

        }
    }
}

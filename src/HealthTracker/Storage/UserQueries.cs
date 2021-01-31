using System.Linq;
using HealthTracker.Models;

namespace HealthTracker.Storage
{
    public static class UserQueries
    {
        public static User GetOrAddUser(this HealthTrackerDbContext context)
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
}

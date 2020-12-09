using SQLite;
using HealthTracker.Models;
using HealthTracker.Seeds;

namespace HealthTracker.Storage.Impl
{
    public class SQLiteUserStorage : SQLiteStorageBase<User>, IUserStorage
    {
        public SQLiteUserStorage(SQLiteConnection connection)
            : base(connection)
        {
            if (ShouldSeed())
            {
                UserSeed.Seed(this);
            }
        }

        public User GetOrAdd()
        {
            var user = Connection.Table<User>().FirstOrDefault();
            if (user == null)
            {
                user = new User();
                Connection.Insert(user);
            }
            return user;
        }

        public void Update(User user)
        {
            Connection.Update(user);
        }
    }
}

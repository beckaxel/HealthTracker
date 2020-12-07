using SQLite;
using HealthTracker.Models;
using System.Linq;
using HealthTracker.Seeds;

namespace HealthTracker.Storage.Impl
{
    public class SQLiteUserStorage : IUserStorage
    {
        protected SQLiteConnection Connection { get; }

        public SQLiteUserStorage(SQLiteConnection connection)
        {
            connection.CreateTable<User>(CreateFlags.None);
            Connection = connection;

            if (!Connection.Table<User>().Any())
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

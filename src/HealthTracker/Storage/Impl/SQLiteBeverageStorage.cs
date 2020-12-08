using System.Collections.Generic;
using System.Linq;
using HealthTracker.Models;
using HealthTracker.Seeds;
using SQLite;

namespace HealthTracker.Storage.Impl
{
    public class SQLiteBeverageStorage : IBeverageStorage
    {
        protected SQLiteConnection Connection { get; }

        public SQLiteBeverageStorage(SQLiteConnection connection)
        {
            connection.CreateTable<Beverage>(CreateFlags.None);
            Connection = connection;

            if (!Connection.Table<Beverage>().Any())
            {
                BeverageSeed.Seed(this);
            }
        }

        public IEnumerable<Beverage> All()
        {
            return Connection.Table<Beverage>();
        }

        public Beverage Find(int id)
        {
            return Connection.Find<Beverage>(id);
        }

        public bool Exists(Beverage beverage)
        {
            return Connection.Find<Beverage>(beverage.Id) != default;
        }

        public void Insert(Beverage beverage)
        {
            Connection.Insert(beverage);
        }

        public void Remove(Beverage beverage)
        {
            Connection.Delete(beverage);
        }

        public void Update(Beverage beverage)
        {
            Connection.Update(beverage);
        }

        public void UpdateOrInsert(Beverage beverage)
        {
            if (Connection.Find<Beverage>(beverage.Id) != default)
                Connection.Update(beverage);
            else
                Connection.Insert(beverage);
        }

        public Beverage LatestOrDefault()
        {
            return Connection
                .Table<Beverage>()
                .OrderByDescending(b => b.Date)
                .FirstOrDefault();
        }
    }
}

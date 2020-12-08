using System.Collections.Generic;
using System.Linq;
using HealthTracker.Models;
using HealthTracker.Seeds;
using SQLite;

namespace HealthTracker.Storage.Impl
{
    public class SQLiteBeverageTypeStorage : IBeverageTypeStorage
    {
        protected SQLiteConnection Connection { get; }

        public SQLiteBeverageTypeStorage(SQLiteConnection connection)
        {
            connection.CreateTable<BeverageType>(CreateFlags.None);
            Connection = connection;

            if (!Connection.Table<BeverageType>().Any())
            {
                BeverageTypeSeed.Seed(this);
            }
        }

        public IEnumerable<BeverageType> All()
        {
            return Connection.Table<BeverageType>();
        }

        public BeverageType Find(int id)
        {
            return Connection.Find<BeverageType>(id);
        }

        public bool Exists(BeverageType beverageType)
        {
            return Connection.Find<BeverageType>(beverageType.Id) != default;
        }

        public void Insert(BeverageType beverageType)
        {
            Connection.Insert(beverageType);
        }

        public void Remove(BeverageType beverageType)
        {
            Connection.Delete(beverageType);
        }

        public void Update(BeverageType beverageType)
        {
            Connection.Update(beverageType);
        }

        public void UpdateOrInsert(BeverageType beverageType)
        {
            if (Connection.Find<BeverageType>(beverageType.Id) != default)
                Connection.Update(beverageType);
            else
                Connection.Insert(beverageType);
        }
    }
}

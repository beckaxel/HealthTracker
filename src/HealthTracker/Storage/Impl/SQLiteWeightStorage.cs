using System;
using System.Collections.Generic;
using System.Linq;
using HealthTracker.Models;
using SQLite;

namespace HealthTracker.Storage.Impl
{
    public class SQLiteWeightStorage : IWeightStorage
    {
        protected SQLiteConnection Connection { get; }

        public SQLiteWeightStorage(SQLiteConnection connection)
        {
            connection.CreateTable<Weight>(CreateFlags.None);
            Connection = connection;
            if (!Connection.Table<Weight>().Any())
            {
                Insert(new Weight { Date = DateTime.Now.AddDays(-5), Amount = 66.2m });
                Insert(new Weight { Date = DateTime.Now.AddDays(-4), Amount = 65.9m });
                Insert(new Weight { Date = DateTime.Now.AddDays(-3), Amount = 65.8m });
                Insert(new Weight { Date = DateTime.Now.AddDays(-2), Amount = 65.2m });
                Insert(new Weight { Date = DateTime.Now.AddDays(-1), Amount = 65.5m });
                Insert(new Weight { Date = DateTime.Now, Amount = 65.3m });
            }
        }

        public IEnumerable<Weight> All()
        {
            return Connection.Table<Weight>();
        }

        public Weight Find(int id)
        {
            return Connection.Find<Weight>(id);
        }

        public bool Exists(Weight weight)
        {
            return Connection.Find<Weight>(weight.Id) != default;
        }

        public void Insert(Weight weight)
        {
            Connection.Insert(weight);
        }

        public void Remove(Weight weight)
        {
            Connection.Delete(weight);
        }

        public void Update(Weight weight)
        {
            Connection.Update(weight);
        }

        public void UpdateOrInsert(Weight weight)
        {
            if (Connection.Find<Weight>(weight.Id) != default)
                Connection.Update(weight);
            else
                Connection.Insert(weight);
        }

        public Weight LatestOrDefault()
        {
            return Connection
                .Table<Weight>()
                .OrderByDescending(w => w.Date)
                .FirstOrDefault();
        }
    }
}

using HealthTracker.Models;
using HealthTracker.Seeds;
using SQLite;

namespace HealthTracker.Storage.Impl
{
    public class SQLiteBeverageStorage : SQLiteActivityEntityStorageBase<Beverage>, IBeverageStorage
    {
        public SQLiteBeverageStorage(SQLiteConnection connection)
            : base(connection)
        {
            if (ShouldSeed())
            {
                BeverageSeed.Seed(this);
            }
        }
    }
}

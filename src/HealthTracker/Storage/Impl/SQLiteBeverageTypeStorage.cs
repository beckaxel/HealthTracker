using System.Linq;
using HealthTracker.Models;
using HealthTracker.Seeds;
using SQLite;

namespace HealthTracker.Storage.Impl
{
    public class SQLiteBeverageTypeStorage : SQLiteEntityStorageBase<BeverageType>, IBeverageTypeStorage
    {
        public SQLiteBeverageTypeStorage(SQLiteConnection connection)
            : base(connection)
        {   
            if (ShouldSeed())
            {
                BeverageTypeSeed.Seed(this);
            }
        }
    }
}

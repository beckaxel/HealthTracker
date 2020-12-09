using HealthTracker.Models;
using HealthTracker.Seeds;
using SQLite;

namespace HealthTracker.Storage.Impl
{
    public class SQLiteWeightStorage : SQLiteActivityEntityStorageBase<Weight>, IWeightStorage
    {
        public SQLiteWeightStorage(SQLiteConnection connection)
            : base(connection)
        {
            if (ShouldSeed())
            {
                WeightSeed.Seed(this);
            }
        }
    }
}

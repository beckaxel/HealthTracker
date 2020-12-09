using HealthTracker.Models;
using SQLite;

namespace HealthTracker.Storage.Impl
{
    public abstract class SQLiteActivityEntityStorageBase<T> : SQLiteEntityStorageBase<T>, IActivityEntityStorage<T>
        where T : ActivityEntity, new()
    {
        protected SQLiteActivityEntityStorageBase(SQLiteConnection connection)
            : base(connection)
        {
        }

        public T LatestOrDefault()
        {
            return Connection
                .Table<T>()
                .OrderByDescending(e => e.Date)
                .FirstOrDefault();
        }
    }
}

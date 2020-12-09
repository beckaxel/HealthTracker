using System.Linq;
using HealthTracker.Models;
using SQLite;

namespace HealthTracker.Storage.Impl
{
    public abstract class SQLiteStorageBase<T>
        where T : Entity, new()
    {
        protected SQLiteConnection Connection { get; }

        protected SQLiteStorageBase(SQLiteConnection connection)
        {
            connection.CreateTable<T>(CreateFlags.None);
            Connection = connection;
        }

        protected bool ShouldSeed()
        {
            return !Connection.Table<T>().Any();
        }
    }
}

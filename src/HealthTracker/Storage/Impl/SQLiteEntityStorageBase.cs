using System.Collections.Generic;
using System.Linq;
using HealthTracker.Models;
using SQLite;

namespace HealthTracker.Storage.Impl
{
    public class SQLiteEntityStorageBase<T> : SQLiteStorageBase<T>, IEntityStorage<T>
        where T : Entity, new()
    {
        protected SQLiteEntityStorageBase(SQLiteConnection connection)
            : base(connection)
        {
        }

        public IEnumerable<T> All()
        {
            return Connection.Table<T>();
        }

        public T Find(int id)
        {
            return Connection.Find<T>(id);
        }

        public void Insert(T entity)
        {
            Connection.Insert(entity);
        }

        public void Remove(T entity)
        {
            Connection.Delete(entity);
        }

        public void Update(T entity)
        {
            Connection.Update(entity);
        }
    }
}

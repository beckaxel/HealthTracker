using System.Collections.Generic;
using HealthTracker.Models;

namespace HealthTracker.Storage
{
    public interface IEntityStorage<T>
        where T : Entity, new()
    {
        IEnumerable<T> All();
        T Find(int id);
        void Insert(T entity);
        void Remove(T entity);
        void Update(T entity);
    }
}

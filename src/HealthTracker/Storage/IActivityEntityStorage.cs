using HealthTracker.Models;

namespace HealthTracker.Storage
{
    public interface IActivityEntityStorage<T> : IEntityStorage<T>
        where T : ActivityEntity, new()
    {
        T LatestOrDefault();
    }
}

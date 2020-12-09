using HealthTracker.Models;

namespace HealthTracker.Storage
{
    public static class EntityStorageExtensions
    {
        public static bool Exists<T>(this IEntityStorage<T> entityStorage, T entity)
            where T : Entity, new()
        {
            return entityStorage.Find(entity.Id) != default;
        }

        public static void UpdateOrInsert<T>(this IEntityStorage<T> entityStorage, T entity)
            where T : Entity, new()
        {
            if (entityStorage.Exists(entity))
                entityStorage.Update(entity);
            else
                entityStorage.Insert(entity);
        }
    }
}

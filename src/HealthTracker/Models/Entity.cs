using SQLite;

namespace HealthTracker.Models
{
    public abstract class Entity
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
    }
}

using SQLite;

namespace HealthTracker.Models
{
    public class Setting : Entity
    {
        [Indexed]
        public string Key { get; set; }

        public string Value { get; set; }
    }
}

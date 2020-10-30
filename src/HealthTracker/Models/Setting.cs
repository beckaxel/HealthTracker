using SQLite;

namespace HealthTracker.Models
{
    public class Setting
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [Indexed]
        public string Key { get; set; }

        public string Value { get; set; }
    }
}

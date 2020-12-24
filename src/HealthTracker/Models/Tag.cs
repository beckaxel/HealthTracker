using System.Collections.Generic;

namespace HealthTracker.Models
{
    public class Tag
    {
        public int TagId { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Beverage> Beverages { get; set; } = new HashSet<Beverage>();

        public virtual ICollection<Meal> Meals { get; set; } = new HashSet<Meal>();

        public virtual ICollection<BodyMeasurement> BodyMeasurements { get; set; } = new HashSet<BodyMeasurement>();

        public virtual ICollection<SleepLog> SleepLogs { get; set; } = new HashSet<SleepLog>();
    }
}

using System;
using SQLite;

namespace HealthTracker.Models
{
    public abstract class ActivityEntity : Entity
    {
        [Indexed]
        public DateTime Date { get; set; }
    }
}

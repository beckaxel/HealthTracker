using System;
using System.Collections.Generic;

namespace HealthTracker.Models
{
    public class Photo
    {
        public int PhotoId { get; set; }

        public string FileName { get; set; }

        public DateTime RecordingTime { get; set; }

        public byte[] Content { get; set; }

        public virtual ICollection<Beverage> Beverages { get; set; } = new HashSet<Beverage>();
 
        public virtual ICollection<Meal> Meals { get; set; } = new HashSet<Meal>();
    }
}

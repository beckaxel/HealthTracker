using System;
using System.Collections.Generic;

namespace HealthTracker.Models
{
    public class Photo : IDatable
    {
        public int PhotoId { get; set; }

        public DateTime Date { get; set; }

        public byte[] Content { get; set; }

        public virtual ICollection<Beverage> Beverages { get; set; } = new HashSet<Beverage>();
 
        public virtual ICollection<Meal> Meals { get; set; } = new HashSet<Meal>();
    }
}

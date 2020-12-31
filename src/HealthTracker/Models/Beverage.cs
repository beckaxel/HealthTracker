using System;
using System.Collections.Generic;

namespace HealthTracker.Models
{
    public class Beverage
    {
        public int BeverageId { get; set; }

        public string Name { get; set; }

        public DateTime DrinkingTime { get; set; }

        public virtual ICollection<Photo> Photos { get; set; } = new HashSet<Photo>();

        public virtual ICollection<Food> Foods { get; set; } = new HashSet<Food>();

        public float Amount { get; set; }
    }
}

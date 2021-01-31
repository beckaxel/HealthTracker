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

        public float Quantity { get; set; }
    }
}

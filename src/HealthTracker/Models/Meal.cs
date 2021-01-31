using System;
using System.Collections.Generic;

namespace HealthTracker.Models
{
    public class Meal
    {
        public int MealId { get; set; }

        public string Name { get; set; }

        public DateTime EatingTime { get; set; }

        public MealType MealType { get; set; }

        public Diet Diet { get; set; }

        public virtual ICollection<Photo> Photos { get; set; } = new HashSet<Photo>();
    }
}

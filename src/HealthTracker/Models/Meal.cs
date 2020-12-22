using System;
using System.Collections.Generic;

namespace HealthTracker.Models
{
    public class Meal : INameable, IDatable, IPhotographable, ITagable
    {
        public int MealId { get; set; }

        public string Name { get; set; }

        public DateTime Date { get; set; }

        public virtual ICollection<Photo> Photos { get; set; } = new HashSet<Photo>();

        public virtual ICollection<Tag> Tags { get; set; } = new HashSet<Tag>();
    }
}

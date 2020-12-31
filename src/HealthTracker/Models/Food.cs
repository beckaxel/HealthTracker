using System.Collections.Generic;

namespace HealthTracker.Models
{
    public class Food
    {
        public int FoodId { get; set; }

        public string Name { get; set; }

        public Diet Diet { get; set; }

        public float AlcoholContent { get; set; }

        public bool ContainsSugar { get; set; }

        public bool ContainsLactose { get; set; }

        public bool ContainsGluten { get; set; }

        public virtual ICollection<Beverage> Beverages { get; set; } = new HashSet<Beverage>();

        public virtual ICollection<Meal> Meals { get; set; } = new HashSet<Meal>();
    }
}

using System.Linq;
using HealthTracker.Models;
using HealthTracker.Storage;

namespace HealthTracker.Seeds
{
    public static class FoodSeed
    {
        public static void Seed(HealthTrackerDbContext healthTrackerDbContext)
        {
            if (healthTrackerDbContext.Food.Any())
                return;

            healthTrackerDbContext.Add(new Food
            {
                Name = "Fleisch",
                Diet = Diet.None,
                ContainsSugar = false,
                ContainsLactose = false,
                ContainsGluten = false,
                AlcoholContent = 0
            });

            healthTrackerDbContext.Add(new Food
            {
                Name = "Fisch",
                Diet = Diet.Pescetarian,
                ContainsSugar = false,
                ContainsLactose = false,
                ContainsGluten = false,
                AlcoholContent = 0
            });

            healthTrackerDbContext.Add(new Food
            {
                Name = "Weizen",
                Diet = Diet.Vegan,
                ContainsSugar = false,
                ContainsLactose = false,
                ContainsGluten = true,
                AlcoholContent = 0
            });

            healthTrackerDbContext.Add(new Food
            {
                Name = "Milch",
                Diet = Diet.Vegetarian,
                ContainsSugar = false,
                ContainsLactose = true,
                ContainsGluten = false,
                AlcoholContent = 0
            });

            healthTrackerDbContext.Add(new Food
            {
                Name = "Ei",
                Diet = Diet.Vegetarian,
                ContainsSugar = false,
                ContainsLactose = false,
                ContainsGluten = false,
                AlcoholContent = 0
            });

            healthTrackerDbContext.Add(new Food
            {
                Name = "Joghurt",
                Diet = Diet.Vegetarian,
                ContainsSugar = false,
                ContainsLactose = true,
                ContainsGluten = false,
                AlcoholContent = 0
            });

            healthTrackerDbContext.Add(new Food
            {
                Name = "Schmand",
                Diet = Diet.Vegetarian,
                ContainsSugar = false,
                ContainsLactose = true,
                ContainsGluten = false,
                AlcoholContent = 0
            });

            healthTrackerDbContext.Add(new Food
            {
                Name = "Saure Sahne",
                Diet = Diet.Vegetarian,
                ContainsSugar = false,
                ContainsLactose = true,
                ContainsGluten = false,
                AlcoholContent = 0
            });

            healthTrackerDbContext.Add(new Food
            {
                Name = "Apfel",
                Diet = Diet.Vegan,
                ContainsSugar = false,
                ContainsLactose = false,
                ContainsGluten = false,
                AlcoholContent = 0
            });

            healthTrackerDbContext.Add(new Food
            {
                Name = "Clementine",
                Diet = Diet.Vegan,
                ContainsSugar = false,
                ContainsLactose = false,
                ContainsGluten = false,
                AlcoholContent = 0
            });

            healthTrackerDbContext.Add(new Food
            {
                Name = "Birne",
                Diet = Diet.Vegan,
                ContainsSugar = false,
                ContainsLactose = false,
                ContainsGluten = false,
                AlcoholContent = 0
            });

            healthTrackerDbContext.Add(new Food
            {
                Name = "Kiwi",
                Diet = Diet.Vegan,
                ContainsSugar = false,
                ContainsLactose = false,
                ContainsGluten = false,
                AlcoholContent = 0
            });

            healthTrackerDbContext.Add(new Food
            {
                Name = "Orange",
                Diet = Diet.Vegan,
                ContainsSugar = false,
                ContainsLactose = false,
                ContainsGluten = false,
                AlcoholContent = 0
            });

            healthTrackerDbContext.Add(new Food
            {
                Name = "Banane",
                Diet = Diet.Vegan,
                ContainsSugar = false,
                ContainsLactose = false,
                ContainsGluten = false,
                AlcoholContent = 0
            });

            healthTrackerDbContext.Add(new Food
            {
                Name = "Sauerkirschen",
                Diet = Diet.Vegan,
                ContainsSugar = true,
                ContainsLactose = false,
                ContainsGluten = false,
                AlcoholContent = 0
            });

            healthTrackerDbContext.Add(new Food
            {
                Name = "Fleisch",
                Diet = Diet.None,
                ContainsSugar = false,
                ContainsLactose = false,
                ContainsGluten = false,
                AlcoholContent = 0
            });

            healthTrackerDbContext.SaveChanges();
        }
    }
}

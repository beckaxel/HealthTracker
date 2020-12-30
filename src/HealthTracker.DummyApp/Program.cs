using System;
using HealthTracker.Models;
using HealthTracker.MVVM.Mapping;
using HealthTracker.Storage;
using HealthTracker.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace HealthTracker.DummyApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            MapFromModelToViewModel();
            MapFromViewModelToModel();

            Console.WriteLine("ready");
        }

        private static void MapFromModelToViewModel()
        {
            var meal = new Meal
            {
                MealId = 1,
                EatingTime = DateTime.Today,
                Name = "Mein erstes Essen"
            };
            meal.Photos.Add(new Photo
            {
                PhotoId = 1,
                RecordingTime = DateTime.Today.AddHours(1),
                FileName = "MeinErstesEssen_001.jpg"
            });
            meal.Photos.Add(new Photo
            {
                PhotoId = 2,
                RecordingTime = DateTime.Today.AddHours(2),
                FileName = "MeinErstesEssen_002.jpg"
            });
            /*
            meal.Tags.Add(new Tag
            {
                TagId = 1,
                Name = "Fleisch"
            });
            meal.Tags.Add(new Tag
            {
                TagId = 2,
                Name = "Milch"
            });
            */
            var mealViewModel = new MealViewModel();

            var mapper = new ModelViewModelMapper();
            mapper.Map(meal, mealViewModel);
        }

        private static void MapFromViewModelToModel()
        {
            var mealViewModel = new MealViewModel
            {
                EatingTime = DateTime.Today,
                Name = "Mein erstes Essen"
            };
            mealViewModel.Photos.Add(new PhotoViewModel
            {
                RecordingTime = DateTime.Today.AddHours(1),
                FileName = "MeinErstesEssen_001.jpg"
            });
            mealViewModel.Photos.Add(new PhotoViewModel
            {
                RecordingTime = DateTime.Today.AddHours(2),
                FileName = "MeinErstesEssen_002.jpg"
            });
            /*
            mealViewModel.Tags.Add(new TagViewModel
            {
                Name = "Fleisch"
            });
            mealViewModel.Tags.Add(new TagViewModel
            {
                Name = "Milch"
            });
            */
            var meal = new Meal();

            var mapper = new ModelViewModelMapper();
            mapper.Map(mealViewModel, meal);
        }
    }

    public class HealthTrackerDbContextFactory : IDesignTimeDbContextFactory<HealthTrackerDbContext>
    {
        public HealthTrackerDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<HealthTrackerDbContext>();
            optionsBuilder.UseSqlite("Data Source=HealthTracker.db");

            return new HealthTrackerDbContext(optionsBuilder.Options);
        }
    }
}

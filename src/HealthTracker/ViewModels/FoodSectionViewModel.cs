﻿using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using HealthTracker.MVVM;
using HealthTracker.Services;
using HealthTracker.Storage;
using Microsoft.EntityFrameworkCore;
using Xamarin.Forms;

namespace HealthTracker.ViewModels
{
    public class FoodSectionViewModel : SectionMainViewModel
    {
        private readonly HealthTrackerDbContext _healthTrackerDbContext;

        public FoodSectionViewModel
        (
            INavigationService navigationService,
            IDbContextFactory dbContextFactory
        )
            : base(navigationService)
        {
            _healthTrackerDbContext = dbContextFactory.CreateHealthTrackerDbContext();
            Meals.AddRange(_healthTrackerDbContext.Meal
                .Include(m => m.Photos)
                .LastXDays(14)
                .OrderByDescending(m => m.EatingTime)
                .Select(m => new MealViewModel { Parameter = m }));
        }

        protected override void Dispose(bool disposing)
        {
            _healthTrackerDbContext.Dispose();
            base.Dispose(disposing);
        }

        #region FilterContent

        protected override void FilterContent(string activeFilter)
        {

        }

        #endregion

        #region Meals

        public ObservableCollection<MealViewModel> Meals { get; } = new ObservableCollection<MealViewModel>();

        #endregion

        #region AddMeal

        private ICommand _addMealCommand;

        public ICommand AddMealCommand => GetLazyProperty(ref _addMealCommand, () => new Command(AddMeal));

        public void AddMeal()
        {
            NavigationService.NavigateTo("EditMeal");
        }

        #endregion

        #region OpenMealCommand

        private ICommand _openMealCommand;

        public ICommand OpenMealCommand => GetLazyProperty(ref _openMealCommand, () => new Command(id => OpenMeal((int)id)));

        public void OpenMeal(int mealId)
        {
            var meal = _healthTrackerDbContext
                .Meal
                .Include(m => m.Photos)
                .FirstOrDefault(m => m.MealId == mealId);

            if (meal == null)
                return;

            NavigationService.NavigateTo("EditMeal", meal);
        }

        #endregion
    }
}

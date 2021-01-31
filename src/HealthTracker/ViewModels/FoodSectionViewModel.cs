using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using HealthTracker.Models;
using HealthTracker.MVVM;
using HealthTracker.Services;
using HealthTracker.Skia;
using HealthTracker.Storage;
using HealthTracker.Themes;
using Microcharts;
using Microsoft.EntityFrameworkCore;
using Xamarin.Forms;

namespace HealthTracker.ViewModels
{
    public class FoodSectionViewModel : FilterableSectionMainViewModel<Meal>
    {
        public FoodSectionViewModel
        (
            IDbContextFactory dbContextFactory,
            INavigationService navigationService
        )
            : base(dbContextFactory, navigationService)
        {
        }

        #region FilterContent

        protected override Task<List<Meal>> GetFilteredItemsAsync(IQueryable<Meal> query, string filterName)
        {
            return query
                .Include(m => m.Photos)
                .Filter(filterName)
                .OrderByDescending(m => m.EatingTime)
                .ToListAsync();
        }

        protected override Task<bool> UpdateListAsync(List<Meal> filteredItems, string filterName)
        {
            return Task.Run(() => UpdateList(filteredItems, filterName));
        }

        private bool UpdateList(List<Meal> filteredItems, string filterName)
        {
            Meals.Clear();
            if (filteredItems.Count == 0)
                return false;

            Meals.AddRange(filteredItems.Select(w => new MealViewModel { Parameter = w }));
            return true;
        }

        protected override Task<bool> UpdateSummaryAsync(List<Meal> filteredItems, string filterName)
        {
            return Task.Run(() => UpdateSummary(filteredItems, filterName));
        }

        private bool UpdateSummary(List<Meal> filteredItems, string filterName)
        {
            UpdateDaysSinceLastX();
            return filterName == "Heute"
                ? UpdateTodaySummary(filteredItems, filterName)
                : UpdateChartSummary(filteredItems, filterName);
        }

        private void UpdateDaysSinceLastX()
        {
            DaysSinceLastMeatMeal = HealthTrackerDbContext.Meal.GetDaysSinceLast(Diet.Meat);
            AverageDaysBetweenMeatMeals = HealthTrackerDbContext.Meal.GetAverageDaysBetweenMeals(Diet.Meat);

            DaysSinceLastFishMeal = HealthTrackerDbContext.Meal.GetDaysSinceLast(Diet.Fish);
            AverageDaysBetweenFishMeals = HealthTrackerDbContext.Meal.GetAverageDaysBetweenMeals(Diet.Fish);

            DaysSinceLastVegetarianMeal = HealthTrackerDbContext.Meal.GetDaysSinceLast(Diet.Vegetarian);
            AverageDaysBetweenVegetarianMeals = HealthTrackerDbContext.Meal.GetAverageDaysBetweenMeals(Diet.Vegetarian);

            DaysSinceLastVeganMeal = HealthTrackerDbContext.Meal.GetDaysSinceLast(Diet.Vegan);
            AverageDaysBetweenVeganMeals = HealthTrackerDbContext.Meal.GetAverageDaysBetweenMeals(Diet.Vegan);
        }

        private bool UpdateTodaySummary(List<Meal> filteredItems, string filterName)
        {
            if (filteredItems.Count == 0)
                return false;

            EatingToday = new EatingPerDayViewModel
            {
                Day = DateTime.Today,
                FirstEatingTime = filteredItems.Last().EatingTime.ToLocalTime(),
                LastEatingTime = filteredItems.First().EatingTime.ToLocalTime(),
                Diet = filteredItems.Max(m => m.Diet),
                HadBreakfast = filteredItems.Any(m => m.MealType == MealType.Breakfast),
                HadLunch = filteredItems.Any(m => m.MealType == MealType.Lunch),
                HadDinner = filteredItems.Any(m => m.MealType == MealType.Dinner),
                NumberOfSnacks = filteredItems.Count(m => m.MealType == MealType.Snack)
            };

            From = Filter.FormatDateTime(filterName, EatingToday.FirstEatingTime);
            To = Filter.FormatDateTime(filterName, EatingToday.LastEatingTime);

            return true;
        }

        private bool UpdateChartSummary(List<Meal> filteredItems, string filterName)
        {
            var groupedItems = filteredItems
                .GroupBy(m => m.EatingTime.ToLocalTime().Date)
                .OrderBy(g => g.Key)
                .ToList();

            if (groupedItems.Count == 0)
                return false;

            From = Filter.FormatDateTime(filterName, groupedItems.First().Key);
            To = Filter.FormatDateTime(filterName, groupedItems.Last().Key);

            var theme = new LightTheme();
            var chartBuilder = new SimpleBarChartBuilder
            {
                BackgroundColor = Color.Transparent,
                ForegroundColor = (Color)theme["SecondaryColor"]
            };

            var values = Enumerable.Empty<float>();
            if (filterName == "Jahr")
            {
                chartBuilder.MinValue = 0;
                chartBuilder.MaxValue = 1;

                EatingYear = new EatingPerYearViewModel
                {
                    TotalEatingDays = groupedItems.Count(),
                    MeatDaysPerYear = groupedItems.Count(g => g.Max(m => m.Diet == Diet.Meat)),
                    FishDaysPerYear = groupedItems.Count(g => g.Max(m => m.Diet == Diet.Fish)),
                    VegetarianDaysPerYear = groupedItems.Count(g => g.Max(m => m.Diet == Diet.Vegetarian)),
                    VeganDaysPerYear = groupedItems.Count(g => g.Max(m => m.Diet == Diet.Vegan))
                };

                values = new[]
                {
                    EatingYear.VeganDaysPerYearPercent,
                    EatingYear.VegetarianDaysPerYearPercent,
                    EatingYear.FishDaysPerYearPercent,
                    EatingYear.MeatDaysPerYearPercent
                };
            }
            else
            {
                chartBuilder.MinValue = Convert.ToSingle(Diet.Vegan);
                chartBuilder.MaxValue = Convert.ToSingle(Diet.Meat) + 1f;

                values = groupedItems.Select(g => Convert.ToSingle(g.Max(g => g.Diet)) + 1f);
            }

            chartBuilder.AddValueRange(values);
            Chart = chartBuilder.ToChart();

            return true;
        }

        #endregion

        #region Meals

        public ObservableCollection<MealViewModel> Meals { get; } = new ObservableCollection<MealViewModel>();

        #endregion

        #region Chart Summary

        private Chart _chart;
        public Chart Chart
        {
            get => _chart;
            set => SetProperty(ref _chart, value);
        }

        private string _from;
        public string From
        {
            get => _from;
            set => SetProperty(ref _from, value);
        }

        private string _to;
        public string To
        {
            get => _to;
            set => SetProperty(ref _to, value);
        }

        #endregion

        private EatingPerDayViewModel _eatingToday;
        public EatingPerDayViewModel EatingToday
        {
            get => _eatingToday;
            set => SetProperty(ref _eatingToday, value);
        }

        public int _daysSinceLastMeatMeal;
        public int DaysSinceLastMeatMeal
        {
            get => _daysSinceLastMeatMeal;
            set => SetProperty(ref _daysSinceLastMeatMeal, value);
        }

        private double _averageDaysBetweenMeatMeals;
        public double AverageDaysBetweenMeatMeals
        {
            get => _averageDaysBetweenMeatMeals;
            set => SetProperty(ref _averageDaysBetweenMeatMeals, value);
        }

        public int _daysSinceLastFishMeal;
        public int DaysSinceLastFishMeal
        {
            get => _daysSinceLastFishMeal;
            set => SetProperty(ref _daysSinceLastFishMeal, value);
        }

        private double _averageDaysBetweenFishMeals;
        public double AverageDaysBetweenFishMeals
        {
            get => _averageDaysBetweenFishMeals;
            set => SetProperty(ref _averageDaysBetweenFishMeals, value);
        }

        public int _daysSinceLastVegetarianMeal;
        public int DaysSinceLastVegetarianMeal
        {
            get => _daysSinceLastVegetarianMeal;
            set => SetProperty(ref _daysSinceLastVegetarianMeal, value);
        }

        private double _averageDaysBetweenVegetarianMeals;
        public double AverageDaysBetweenVegetarianMeals
        {
            get => _averageDaysBetweenVegetarianMeals;
            set => SetProperty(ref _averageDaysBetweenVegetarianMeals, value);
        }

        public int _daysSinceLastVeganMeal;
        public int DaysSinceLastVeganMeal
        {
            get => _daysSinceLastVeganMeal;
            set => SetProperty(ref _daysSinceLastVeganMeal, value);
        }

        private double _averageDaysBetweenVeganMeals;
        public double AverageDaysBetweenVeganMeals
        {
            get => _averageDaysBetweenVeganMeals;
            set => SetProperty(ref _averageDaysBetweenVeganMeals, value);
        }

        private EatingPerYearViewModel _eatingYear;
        public EatingPerYearViewModel EatingYear
        {
            get => _eatingYear;
            set => SetProperty(ref _eatingYear, value);
        }

        #region AddItem

        protected override void AddItem()
        {
            NavigationService.NavigateTo("EditMeal");
        }

        #endregion

        #region OpenMealCommand

        private ICommand _openMealCommand;

        public ICommand OpenMealCommand => GetLazyProperty(ref _openMealCommand, () => new Command(id => OpenMeal((int)id)));

        public void OpenMeal(int mealId)
        {
            var meal = HealthTrackerDbContext
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

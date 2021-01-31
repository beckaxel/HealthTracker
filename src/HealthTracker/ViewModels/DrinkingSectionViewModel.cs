using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using HealthTracker.Common;
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
    public class DrinkingSectionViewModel : FilterableSectionMainViewModel<Beverage>
    {
        public DrinkingSectionViewModel
        (
            IDbContextFactory dbContextFactory,
            INavigationService navigationService
        )
            : base(dbContextFactory, navigationService)
        {
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        #region FilterContent

        protected override Task<List<Beverage>> GetFilteredItemsAsync(IQueryable<Beverage> query, string filterName)
        {
            return query
                .Filter(filterName)
                .OrderByDescending(b => b.DrinkingTime)
                .ToListAsync();
        }

        protected override Task<bool> UpdateListAsync(List<Beverage> filteredItems, string filterName)
        {
            return Task.Run(() => UpdateList(filteredItems, filterName));
        }

        private bool UpdateList(List<Beverage> filteredItems, string filterName)
        {
            BeveragesToday.Clear();
            if (filteredItems.Count == 0)
                return false;

            BeveragesToday.AddRange(filteredItems.Select(w => new BeverageViewModel { Parameter = w }));
            return true;
        }

        protected override Task<bool> UpdateSummaryAsync(List<Beverage> filteredItems, string filterName)
        {
            return Task.Run(() => UpdateSummary(filteredItems, filterName));
        }

        private bool UpdateSummary(List<Beverage> filteredItems, string filterName)
        {
            DailyDrinkingQuantity = HealthTrackerDbContext.GetOrAddUser().DailyDrinkingQuantity;
            return filterName == "Heute"
                ? UpdateTodaySummary(filteredItems)
                : UpdateChartSummary(filteredItems, filterName);
        }

        private bool UpdateTodaySummary(List<Beverage> filteredItems)
        {
            DrinkingQuantityToday = filteredItems
                    .SelectIntoDrinkingQuantityPerDayViewModel(HealthTrackerDbContext)
                    .FirstOrDefault();

            if (DrinkingQuantityToday != null)
                return true;

            DrinkingQuantityToday = new DrinkingQuantityPerDayViewModel
            {
                Day = DateTime.Today,
                Quantity = 0f
            };

            return true;
        }

        private bool UpdateChartSummary(List<Beverage> filteredItems, string filterName)
        {
            var groupedItems = filteredItems
                .GroupBy(b => b.DrinkingTime.ToLocalTime().Date, b => b.Quantity)
                .OrderBy(g => g.Key)
                .ToList();

            if (groupedItems.Count == 0)
                return false;

            From = Filter.FormatDateTime(filterName, groupedItems.First().Key);
            To = Filter.FormatDateTime(filterName, groupedItems.Last().Key);

            var values = groupedItems.Select(g => g.Sum());
            MinQuantity = values.Min();
            AverageQuantity = values.Average();
            MaxQuantity = values.Max();

            if (filterName == "Jahr")
            {
                values = groupedItems
                    .GroupBy(g => g.Key.GetWeekOfYear(CultureInfo.CurrentCulture), g => g.Average())
                    .Select(g => g.Average());
            }

            var theme = new LightTheme();
            var chartBuilder = new SimpleLineChartBuilder
            {
                BackgroundColor = Color.Transparent,
                ForegroundColor = (Color)theme["SecondaryColor"]
            };

            chartBuilder.AddValueRange(values);
            Chart = chartBuilder.ToChart();

            return true;
        }

        #endregion

        private float _dailyDrinkingQuantity;
        public float DailyDrinkingQuantity
        {
            get => _dailyDrinkingQuantity;
            set => SetProperty(ref _dailyDrinkingQuantity, value);
        }

        public ObservableCollection<BeverageViewModel> BeveragesToday { get; } = new ObservableCollection<BeverageViewModel>();

        private DrinkingQuantityPerDayViewModel _drinkingQuantityToday;
        public DrinkingQuantityPerDayViewModel DrinkingQuantityToday
        {
            get => _drinkingQuantityToday;
            set => SetProperty(ref _drinkingQuantityToday, value);
        }

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

        private float _minQuantity;
        public float MinQuantity
        {
            get => _minQuantity;
            set
            {
                if (SetProperty(ref _minQuantity, value))
                    OnPropertyChanged(nameof(MinPercent));
            }
        }

        public float MinPercent => (MinQuantity <= 0 || DailyDrinkingQuantity <= 0) ? 0 : MinQuantity / DailyDrinkingQuantity;


        private float _averageQuantity;
        public float AverageQuantity
        {
            get => _averageQuantity;
            set
            {
                if (SetProperty(ref _averageQuantity, value))
                    OnPropertyChanged(nameof(AveragePercent));
            }
        }

        public float AveragePercent => (AverageQuantity <= 0 || DailyDrinkingQuantity <= 0) ? 0 : AverageQuantity / DailyDrinkingQuantity;


        private float _maxQuantity;
        public float MaxQuantity
        {
            get => _maxQuantity;
            set
            {
                if (SetProperty(ref _maxQuantity, value))
                    OnPropertyChanged(nameof(MaxPercent));
            }
        }

        public float MaxPercent => (MaxQuantity <= 0 || DailyDrinkingQuantity <= 0) ? 0 : MaxQuantity / DailyDrinkingQuantity;

        #endregion

        #region AddItem

        protected override void AddItem()
        {
            NavigationService.NavigateTo("EditBeverage");
        }

        #endregion

        #region OpenBeverageCommand

        private ICommand _openBeverageCommand;

        public ICommand OpenBeverageCommand => GetLazyProperty(ref _openBeverageCommand, () => new Command(id => OpenBeverage((int)id)));

        public void OpenBeverage(int beverageId)
        {
            var beverage = HealthTrackerDbContext
                .Beverage
                .FirstOrDefault(b => b.BeverageId == beverageId);

            if (beverage == null)
                return;

            NavigationService.NavigateTo("EditBeverage", beverage);
        }

        #endregion
    }
}

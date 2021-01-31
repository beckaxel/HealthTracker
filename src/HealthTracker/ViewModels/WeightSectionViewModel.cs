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
    public class WeightSectionViewModel : FilterableSectionMainViewModel<BodyMeasurement>
    {
        public WeightSectionViewModel
        (
            IDbContextFactory dbContextFactory,
            INavigationService navigationService
        )
            : base(dbContextFactory, navigationService)
        {
            
        }

        #region FilterContent

        protected override Task<List<BodyMeasurement>> GetFilteredItemsAsync(IQueryable<BodyMeasurement> query, string filterName)
        {
            return query
                .Filter(filterName)
                .Where(bm => bm.Weight.HasValue && bm.Weight > 0)
                .OrderByDescending(w => w.MeasureTime)
                .ToListAsync();
        }

        protected override Task<bool> UpdateListAsync(List<BodyMeasurement> filteredItems, string filterName)
        {
            return Task.Run(() => UpdateList(filteredItems, filterName));
        }

        private bool UpdateList(List<BodyMeasurement> filteredItems, string filterName)
        {
            Weights.Clear();
            if (filteredItems.Count == 0)
                return false;

            Weights.AddRange(filteredItems.Select(w => new WeightViewModel { Parameter = w }));
            return true;
        }

        protected override Task<bool> UpdateSummaryAsync(List<BodyMeasurement> filteredItems, string filterName)
        {
            return Task.Run(() => UpdateSummary(filteredItems, filterName));
        }

        private bool UpdateSummary(List<BodyMeasurement> filteredItems, string filterName)
        {
            UserHeight = HealthTrackerDbContext.GetOrAddUser().Height;
            return filterName == "Heute"
                ? UpdateTodaySummary(filteredItems)
                : UpdateChartSummary(filteredItems, filterName);
        }

        private bool UpdateTodaySummary(List<BodyMeasurement> filteredItems)
        {
            var actualMeasurement = filteredItems.FirstOrDefault();
            if (actualMeasurement == null)
                return false;

            ActualWeight = actualMeasurement.Weight.Value;
            ActualWeightMeasureTime = actualMeasurement.MeasureTime.ToLocalTime();
            return true;
        }

        private bool UpdateChartSummary(List<BodyMeasurement> filteredItems, string filterName)
        {
            if (!UpdateTodaySummary(filteredItems))
                return false;

            var groupedItems = filteredItems
                .GroupBy(w => w.MeasureTime.ToLocalTime().Date, w => w.Weight.Value)
                .OrderBy(g => g.Key)
                .ToList();

            if (groupedItems.Count == 0)
                return false;

            From = Filter.FormatDateTime(filterName, groupedItems.First().Key);
            To = Filter.FormatDateTime(filterName, groupedItems.Last().Key);

            var values = groupedItems.Select(g => g.Average());
            MinWeight = values.Min();
            AverageWeight = values.Average();
            MaxWeight = values.Max();

            var theme = new LightTheme();

            var chartBuilder = new SimpleLineChartBuilder
            {
                BackgroundColor = Color.Transparent,
                ForegroundColor = (Color)theme["SecondaryColor"]
            };

            if (filterName == "Jahr")
            {
                chartBuilder.PointSize = 10;
                chartBuilder.LineMode = LineMode.Spline;

                values = groupedItems
                    .GroupBy(g => g.Key.GetWeekOfYear(CultureInfo.CurrentCulture), g => g.Average())
                    .Select(g => g.Average());
            }

            chartBuilder.AddValueRange(values);
            Chart = chartBuilder.ToChart();

            return true;
        }

        #endregion

        private float _userHeight;
        public float UserHeight
        {
            get => _userHeight;
            set => SetProperty(ref _userHeight, value);
        }

        #region Weights

        public ObservableCollection<WeightViewModel> Weights { get; } = new ObservableCollection<WeightViewModel>();

        #endregion

        #region AddItem

        protected override void AddItem()
        {
            NavigationService.NavigateTo("EditWeight");
        }

        #endregion

        #region OpenWeightCommand

        private ICommand _openWeightCommand;

        public ICommand OpenWeightCommand => GetLazyProperty(ref _openWeightCommand, () => new Command(id => OpenWeight((int)id)));

        public void OpenWeight(int weightId)
        {
            var weight = HealthTrackerDbContext
                .BodyMeasurement
                .FirstOrDefault(bm => bm.BodyMeasurementId == weightId);

            if (weight == null)
                return;

            NavigationService.NavigateTo("EditWeight", weight);
        }

        #endregion

        #region Summary

        private DateTime _actualWeightMeasureTime;
        public DateTime ActualWeightMeasureTime
        {
            get => _actualWeightMeasureTime;
            set => SetProperty(ref _actualWeightMeasureTime, value);
        }

        private float _actualWeight;
        public float ActualWeight
        {
            get => _actualWeight;
            set
            {
                if (SetProperty(ref _actualWeight, value))
                    OnPropertyChanged(nameof(ActualBodyMassIndex));
            }
        }

        public float ActualBodyMassIndex => CalcBodyMassIndex(ActualWeight);

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

        private float _minWeight;
        public float MinWeight
        {
            get => _minWeight;
            set
            {
                if (SetProperty(ref _minWeight, value))
                    OnPropertyChanged(nameof(MinBodyMassIndex));
            }
        }

        public float MinBodyMassIndex => CalcBodyMassIndex(MinWeight);

        private float _averageWeight;
        public float AverageWeight
        {
            get => _averageWeight;
            set
            {
                if (SetProperty(ref _averageWeight, value))
                    OnPropertyChanged(nameof(AverageBodyMassIndex));
            }
        }

        public float AverageBodyMassIndex => CalcBodyMassIndex(AverageWeight);

        private float _maxWeight;
        public float MaxWeight
        {
            get => _maxWeight;
            set
            {
                if (SetProperty(ref _maxWeight, value))
                    OnPropertyChanged(nameof(MaxBodyMassIndex));
            }
        }

        public float MaxBodyMassIndex => CalcBodyMassIndex(MaxWeight);

        private Chart _chart;
        public Chart Chart
        {
            get => _chart;
            set => SetProperty(ref _chart, value);
        }

        private float CalcBodyMassIndex(float weight)
        {
            if (weight <= 0 || UserHeight <= 0)
                return 0;
            var height = UserHeight / 100;
            return weight / (height * height);
        }

        #endregion
    }
}

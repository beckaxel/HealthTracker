using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using HealthTracker.MVVM;
using HealthTracker.Services;
using HealthTracker.Skia;
using HealthTracker.Storage;
using HealthTracker.Themes;
using Microcharts;
using Xamarin.Forms;

namespace HealthTracker.ViewModels
{
    public class WeightSectionViewModel : SectionMainViewModel
    {
        private readonly IBodyMeasurementStorage _weightStorage;

        public WeightSectionViewModel
        (
            INavigationService navigationService,
            IBodyMeasurementStorage weightStorage
        )
            : base(navigationService)
        {
            _weightStorage = weightStorage;
            Task.Run(() =>
            {
                Weights.AddRange(_weightStorage.LastXDays(14).OrderByDescending(w => w.Date).Select(w => new WeightViewModel { Parameter = w }));
                Weights.CollectionChanged += (s, e) => UpdateChartAsync();
                UpdateChartAsync();
            });
        }

        #region Weights

        public ObservableCollection<WeightViewModel> Weights { get; } = new ObservableCollection<WeightViewModel>();

        #endregion

        #region AddWeight

        private ICommand _addWeightCommand;

        public ICommand AddWeightCommand => GetLazyProperty(ref _addWeightCommand, () => new Command(AddWeight));
        
        public void AddWeight()
        {
            NavigationService.NavigateTo("EditWeight");
        }

        #endregion

        #region Chart

        private Chart _chart;
        public Chart Chart
        {
            get => _chart;
            set => SetProperty(ref _chart, value);
        }

        private bool _isChartLoading;
        public bool IsChartLoading
        {
            get => _isChartLoading;
            set => SetProperty(ref _isChartLoading, value);
        }

        
        protected Task UpdateChartAsync()
        {
            return Task.Run(() => UpdateChart());
        }

        protected void UpdateChart()
        {
            
            Task.Run(() => IsChartLoading = true);
            var theme = new LightTheme();

            var chartBuilder = new SimpleLineChartBuilder
            {
                BackgroundColor = (Color)theme["PrimaryDarkColor"],
                ForegroundColor = (Color)theme["SecondaryColor"]
            };

            var values = Weights
                .GroupBy(w => w.Date, w => w.Amount)
                .OrderBy(g => g.Key)
                .Select(g => g.Average())
                .Where(v => v.HasValue)
                .Select(v => v.Value);

            chartBuilder.AddValueRange(values);
            Chart = chartBuilder.ToChart();
            Task.Run(() => IsChartLoading = false);
        }

        #endregion
    }
}

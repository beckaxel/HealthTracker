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
        private readonly HealthTrackerDbContext _healthTrackerDbContext;

        public WeightSectionViewModel
        (
            INavigationService navigationService,
            IDbContextFactory dbContextFactory
        )
            : base(navigationService)
        {
            _healthTrackerDbContext = dbContextFactory.CreateHealthTrackerDbContext();
            Task.Run(() =>
            {
                FilterContent("Woche");
            });
        }

        protected override void Dispose(bool disposing)
        {
            _healthTrackerDbContext.Dispose();
            base.Dispose(disposing);
        }

        #region FilterContent

        protected override void FilterContent(string activeFilter)
        {
            Weights.Clear();
            Weights.AddRange(_healthTrackerDbContext
                    .BodyMeasurement
                    .Filter(activeFilter)
                    .OrderByDescending(w => w.MeasureTime)
                    .Select(w => new WeightViewModel { Parameter = w }));

            //Weights.CollectionChanged += (s, e) => UpdateChartAsync();
            UpdateChartAsync();
        }

        #endregion

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

        #region OpenWeightCommand

        private ICommand _openWeightCommand;

        public ICommand OpenWeightCommand => GetLazyProperty(ref _openWeightCommand, () => new Command(id => OpenWeight((int)id)));

        public void OpenWeight(int weightId)
        {
            var weight = _healthTrackerDbContext
                .BodyMeasurement
                .FirstOrDefault(bm => bm.BodyMeasurementId == weightId);

            if (weight == null)
                return;

            NavigationService.NavigateTo("EditWeight", weight);
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
                .GroupBy(w => w.DateOfMeasureTime, w => w.Weight)
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

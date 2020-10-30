using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using HealthTracker.MVVM;
using HealthTracker.Services;
using HealthTracker.Skia;
using HealthTracker.Storage;
using Microcharts;
using Xamarin.Forms;

namespace HealthTracker.ViewModels
{
    public class WeightSectionViewModel : SectionMainViewModel
    {
        private readonly IWeightStorage _weightStorage;

        public WeightSectionViewModel
        (
            INavigationService navigationService,
            IWeightStorage weightStorage
        )
            : base(navigationService)
        {
            _weightStorage = weightStorage;
            Weights.AddRange(_weightStorage.All().OrderByDescending(w => w.Date).Select(w => new WeightViewModel { Parameter = w }));
            Weights.CollectionChanged += (s, e) => UpdateChart();
            UpdateChart();
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

        public void UpdateChart()
        {
            var chartBuilder = new SimpleLineChartBuilder();

            var values = Weights
                .GroupBy(w => w.Date, w => w.Amount)
                .Select(g => Convert.ToSingle(g.Average()));

            chartBuilder.AddValueRange(values);
            Chart = chartBuilder.ToChart();
        }

        #endregion
    }
}

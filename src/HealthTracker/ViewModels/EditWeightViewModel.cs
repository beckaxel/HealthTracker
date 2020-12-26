using System;
using System.Windows.Input;
using HealthTracker.Models;
using HealthTracker.Services;
using HealthTracker.Storage;
using Xamarin.Forms;

namespace HealthTracker.ViewModels
{
    public class EditWeightViewModel : WeightViewModel
    {
        private const float StepSize = 0.1f;

        protected INavigationService NavigationService { get; }
        protected IBodyMeasurementStorage WeightStorage { get; }

        public EditWeightViewModel(INavigationService navigationService, IBodyMeasurementStorage weightStorage)
        {
            NavigationService = navigationService;
            WeightStorage = weightStorage;
        }

        #region Parameter Handling

        protected override void OnParameterChanged(object oldValue, object newValue)
        {
            if (newValue == MVVM.Parameter.Empty)
            {
                var latest = WeightStorage.LatestOrDefault();
                if (latest == null)
                    return;

                MapFrom(new BodyMeasurement
                {
                    MeasureTime = DateTime.UtcNow,
                    Weight = latest.Weight
                });
            }
            else
            {
                base.OnParameterChanged(oldValue, newValue);
            }
        }

        #endregion

        #region IncreaseWeight

        private ICommand _increaseWeightCommand;

        public ICommand IncreaseWeightCommand => GetLazyProperty(ref _increaseWeightCommand, () => new Command(IncreaseWeight));

        public void IncreaseWeight()
        {
            Weight += StepSize;
        }

        #endregion

        #region DecreaseWeight

        private ICommand _decreaseWeightCommand;

        public ICommand DecreaseWeightCommand => GetLazyProperty(ref _decreaseWeightCommand, () => new Command(DecreaseWeight));

        public void DecreaseWeight()
        {
            Weight -= StepSize;
        }

        #endregion

        #region Cancel

        private ICommand _cancelCommand;

        public ICommand CancelCommand => GetLazyProperty(ref _cancelCommand, () => new Command(Cancel));

        public void Cancel()
        {
            NavigationService.NavigateToActiveSection();
        }

        #endregion

        #region Save

        private ICommand _saveCommand;

        public ICommand SaveCommand => GetLazyProperty(ref _saveCommand, () => new Command(Save));
        
        public void Save()
        {
            var bodyMeasurement = new BodyMeasurement();
            MapTo(bodyMeasurement);
            WeightStorage.Insert(bodyMeasurement);
            NavigationService.NavigateToActiveSection();
        }

        #endregion
    }
}

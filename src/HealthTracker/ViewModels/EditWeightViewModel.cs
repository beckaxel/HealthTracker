using System.Windows.Input;
using HealthTracker.Models;
using HealthTracker.MVVM;
using HealthTracker.Services;
using HealthTracker.Storage;
using Xamarin.Forms;

namespace HealthTracker.ViewModels
{
    public class EditWeightViewModel : WeightViewModel
    {
        private const float StepSize = 0.1f;

        private INavigationService _navigationService;
        private HealthTrackerDbContext _healthTrackerDbContext;

        public EditWeightViewModel
        (
            INavigationService navigationService,
            IDbContextFactory dbContextFactory
        )
        {
            _navigationService = navigationService;
            _healthTrackerDbContext = dbContextFactory.CreateHealthTrackerDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _healthTrackerDbContext.Dispose();
            base.Dispose(disposing);
        }

        #region Parameter Handling

        protected override void OnParameterChanged(object oldValue, object newValue)
        {
            base.OnParameterChanged(oldValue, newValue);
            if (newValue == MVVM.Parameter.Empty)
            {
                EditMode = EditMode.Create;
                var latest = _healthTrackerDbContext.BodyMeasurement.LatestOrDefault();
                if (latest != null)
                    BodyMeasurement.Weight = latest.Weight;

                _healthTrackerDbContext.Add(BodyMeasurement);
                MapFrom(BodyMeasurement);
            }
            else if (newValue is BodyMeasurement)
            {
                EditMode = EditMode.Edit;
                _healthTrackerDbContext.Attach(BodyMeasurement);
            }
        }

        #endregion

        #region EditMode

        private EditMode _editMode;

        public EditMode EditMode
        {
            get => _editMode;
            set => SetProperty(ref _editMode, value);
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
            _navigationService.NavigateToActiveSection();
        }

        #endregion

        #region Save

        private ICommand _saveCommand;

        public ICommand SaveCommand => GetLazyProperty(ref _saveCommand, () => new Command(Save));
        
        public void Save()
        {
            MapTo(BodyMeasurement);
            _healthTrackerDbContext.SaveChanges();
            _navigationService.NavigateToActiveSection();
        }

        #endregion

        #region Delete

        private ICommand _deleteCommand;

        public ICommand DeleteCommand => GetLazyProperty(ref _deleteCommand, () => new Command(Delete, () => EditMode == EditMode.Edit));

        public void Delete()
        {
            if (EditMode != EditMode.Edit)
                return;

            _healthTrackerDbContext.Remove(BodyMeasurement);
            _healthTrackerDbContext.SaveChanges();
            _navigationService.NavigateToActiveSection();
        }

        #endregion
    }
}

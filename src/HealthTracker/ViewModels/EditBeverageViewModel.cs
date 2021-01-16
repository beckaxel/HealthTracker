using System.Windows.Input;
using HealthTracker.Models;
using HealthTracker.MVVM;
using HealthTracker.Services;
using HealthTracker.Storage;
using Xamarin.Forms;

namespace HealthTracker.ViewModels
{
    public class EditBeverageViewModel : BeverageViewModel
    {
        private const float StepSize = 50.0f;
        private readonly INavigationService _navigationService;
        private readonly HealthTrackerDbContext _healthTrackerDbContext;

        public EditBeverageViewModel
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
                var latest = _healthTrackerDbContext.Beverage.LatestOrDefault();
                if (latest != null)
                    Beverage.Quantity = latest.Quantity;

                _healthTrackerDbContext.Add(Beverage);
                MapFrom(Beverage);
            }
            else if (newValue is Beverage)
            {
                EditMode = EditMode.Edit;
                _healthTrackerDbContext.Attach(Beverage);
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

        #region Increase Quantity

        private ICommand _increaseQuantityCommand;

        public ICommand IncreaseQuantityCommand => GetLazyProperty(ref _increaseQuantityCommand, () => new Command(IncreaseQuantity));

        public void IncreaseQuantity()
        {
            Quantity += StepSize;
            OnPropertyChanged(nameof(Quantity));
        }

        #endregion

        #region Decrease Quantity

        private ICommand _decreaseQuantityCommand;

        public ICommand DecreaseQuantityCommand => GetLazyProperty(ref _decreaseQuantityCommand, () => new Command(DecreaseQuantity));

        public void DecreaseQuantity()
        {
            Quantity -= StepSize;
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
            MapTo(Beverage);
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

            _healthTrackerDbContext.Remove(Beverage);
            _healthTrackerDbContext.SaveChanges();
            _navigationService.NavigateToActiveSection();
        }

        #endregion
    }
}

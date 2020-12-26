using System;
using System.Threading.Tasks;
using System.Windows.Input;
using HealthTracker.Services;
using HealthTracker.Storage;
using Xamarin.Forms;

namespace HealthTracker.ViewModels
{
    public class EditMealViewModel : MealViewModel
    {
        private readonly ICameraService _cameraService;

        public EditMealViewModel
        (
            INavigationService navigationService,
            IDbContextFactory dbContextFactory,
            ICameraService cameraService
        )
            : base(navigationService, dbContextFactory)
        {
            _cameraService = cameraService;
        }

        #region Parameter Handling

        protected override void OnParameterChanged(object oldValue, object newValue)
        {
            base.OnParameterChanged(oldValue, newValue);
            if (newValue == MVVM.Parameter.Empty)
            {
                var latest = HealthTrackerDbContext.Meal.LatestOrDefault();
                if (latest == null)
                    return;

                Meal.EatingTime = DateTime.UtcNow;
                Meal.Name = latest.Name;

                MapFrom(Meal);
            }
        }

        #endregion

        #region TakePhotoCommand

        private ICommand _takePhotoCommand;

        public ICommand TakePhotoCommand => GetLazyProperty(ref _takePhotoCommand, () => new Command(async () => await TakePhoto()));

        public async Task TakePhoto()
        {
            var stream = await _cameraService.TakePhotoAsync();
            if (stream == null)
                return;

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
            SaveChanges();
            NavigationService.NavigateToActiveSection();
        }

        #endregion
    }
}

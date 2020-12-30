using System;
using System.Threading.Tasks;
using System.Windows.Input;
using HealthTracker.Models;
using HealthTracker.Services;
using HealthTracker.Storage;
using Xamarin.Forms;

namespace HealthTracker.ViewModels
{
    public class EditMealViewModel : MealViewModel
    {
        private readonly INavigationService _navigationService;
        private readonly HealthTrackerDbContext _healthTrackerDbContext;
        private readonly ICameraService _cameraService;

        public EditMealViewModel
        (
            INavigationService navigationService,
            IDbContextFactory dbContextFactory,
            ICameraService cameraService
        )   
        {
            _navigationService = navigationService;
            _healthTrackerDbContext = dbContextFactory.CreateHealthTrackerDbContext();
            _cameraService = cameraService;
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
                var latest = _healthTrackerDbContext.Meal.LatestOrDefault();
                if (latest == null)
                    return;

                Meal.Name = latest.Name;
                _healthTrackerDbContext.Add(Meal);
                MapFrom(Meal);
            }
            else if (newValue is Meal)
            {
                _healthTrackerDbContext.Attach(Meal);
            }
        }

        #endregion

        #region TakePhotoCommand

        private ICommand _takePhotoCommand;

        public ICommand TakePhotoCommand => GetLazyProperty(ref _takePhotoCommand, () => new Command(async () => await TakePhoto()));

        public async Task TakePhoto()
        {
            var photoTaken = await _cameraService.TakePhotoAsync();
            if (photoTaken == null)
                return;

            var photo = new Photo
            {
                FileName = photoTaken.FileName,
                RecordingTime = DateTime.UtcNow,
                Content = photoTaken.Content
            };

            Meal.Photos.Add(photo);
            Photos.Add(new PhotoViewModel { Parameter = photo });
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
            MapTo(Meal);
            _healthTrackerDbContext.SaveChanges();
            _navigationService.NavigateToActiveSection();
        }

        #endregion
    }
}

using System;
using System.Windows.Input;
using HealthTracker.Models;
using HealthTracker.MVVM;
using HealthTracker.Services;
using HealthTracker.Storage;
using Xamarin.Forms;

namespace HealthTracker.ViewModels
{
    public class UserViewModel : ViewModelBase
    {
        protected User User { get; private set; }
        private readonly HealthTrackerDbContext _healthTrackerDbContext;
        private readonly INavigationService _navigationService;

        public UserViewModel
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

        protected override void OnParameterChanged(object oldValue, object newValue)
        {
            if (newValue == MVVM.Parameter.Empty)
            {
                User = _healthTrackerDbContext.GetOrAddUser();
                MapFrom(User);
            }
            else if (newValue is User user)
            {
                User = user;
                _healthTrackerDbContext.Attach(User);
                MapFrom(User);
            }
        }

        #region Id

        private int? _id;

        public int? Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        #endregion

        #region Birthdate

        private DateTime _birthDate;

        public DateTime BirthDate
        {
            get => _birthDate;
            set
            {
                value = new DateTime(value.Year, value.Month, value.Day, 0, 0, 0, DateTimeKind.Utc);
                if (SetProperty(ref _birthDate, value) && !IsMapping)
                    Save();
            }
        }

        #endregion

        #region Height

        private float _height;

        public float Height
        {
            get => _height;
            set
            {
                if (SetProperty(ref _height, value) && !IsMapping)
                    Save();
            }
        }

        #endregion

        #region Gender

        private Gender _gender;

        public Gender Gender
        {
            get => _gender;
            set
            {
                if (SetProperty(ref _gender, value) && !IsMapping)
                    Save();
            }
        }

        #endregion

        #region Daily Drinking Quantity

        private float _dailyDrinkingQuantity;

        public float DailyDrinkingQuantity
        {
            get => _dailyDrinkingQuantity;
            set
            {
                if (SetProperty(ref _dailyDrinkingQuantity, value) && !IsMapping)
                    Save();
            }
        }

        #endregion

        #region Storage

        protected void Save()
        {
            MapTo(User);
            _healthTrackerDbContext.SaveChanges();
        }

        #endregion

        #region Close

        private ICommand _closeCommand;

        public ICommand CloseCommand => GetLazyProperty(ref _closeCommand, () => new Command(Close));

        public void Close()
        {
            _navigationService.NavigateToActiveSection();
        }

        #endregion
    }
}

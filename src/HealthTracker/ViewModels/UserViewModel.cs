using System;
using System.Windows.Input;
using HealthTracker.Common;
using HealthTracker.Models;
using HealthTracker.MVVM;
using HealthTracker.Services;
using HealthTracker.Storage;
using Xamarin.Forms;

namespace HealthTracker.ViewModels
{
    public class UserViewModel : ViewModelBase
    {
        private bool _loading = false;

        protected INavigationService NavigationService { get; }
        protected IUserStorage UserStorage { get; }

        public UserViewModel(INavigationService navigationService, IUserStorage userStorage)
        {
            NavigationService = navigationService;
            UserStorage = userStorage;
            Load();
        }

        #region Id

        private int? _id;

        public int? Id
        {
            get => _id;
            protected set => SetProperty(ref _id, value);
        }

        #endregion

        #region Birthdate

        private DateTime? _birthDate;

        public DateTime? BirthDate
        {
            get => _birthDate;
            set
            {
                SetProperty(ref _birthDate, value);
                Save();
            }
        }

        #endregion

        #region Height

        private decimal? _height;

        public decimal? Height
        {
            get => _height;
            set
            {
                SetProperty(ref _height, value);
                Save();
            }
        }

        #endregion

        #region Gender

        private Gender? _gender;

        public Gender? Gender
        {
            get => _gender;
            set
            {
                SetProperty(ref _gender, value);
                Save();
            }
        }

        #endregion

        #region Storage

        protected void Load()
        {
            _loading = true;

            var user = UserStorage.GetOrAdd();
            _id = user.Id;
            _birthDate = user.BirthDate.Date;
            _height = user.Height;
            _gender = user.Gender;

            _loading = false;
        }

        protected void Save()
        {
            if (_loading)
                return;

            UserStorage.Update
            (
                new User
                {
                    Id = Id ?? default,
                    BirthDate = BirthDate?.Date ?? default,
                    Height = Height ?? default,
                    Gender = Gender ?? default
                }
            );
        }

        #endregion

        #region Close

        private ICommand _closeCommand;

        public ICommand CloseCommand => GetLazyProperty(ref _closeCommand, () => new Command(Close));

        public void Close()
        {
            NavigationService.NavigateToActiveSection();
        }

        #endregion
    }
}

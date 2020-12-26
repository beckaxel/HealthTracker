using System;
using HealthTracker.Common;
using HealthTracker.Models;
using HealthTracker.MVVM;
using HealthTracker.Services;
using HealthTracker.Storage;

namespace HealthTracker.ViewModels
{
    public class MealViewModel : ViewModelBase
    {
        protected INavigationService NavigationService { get; }
        protected Meal Meal { get; private set; }
        protected HealthTrackerDbContext HealthTrackerDbContext { get; }

        public MealViewModel
        (
            INavigationService navigationService,
            IDbContextFactory dbContextFactory
        )
        {
            HealthTrackerDbContext = dbContextFactory.CreateHealthTrackerDbContext();
            NavigationService = navigationService;
        }

        protected override void Dispose(bool disposing)
        {
            HealthTrackerDbContext.Dispose();
            base.Dispose(disposing);
        }

        #region Parameter Handling

        protected override void OnParameterChanged(object oldValue, object newValue)
        {
            if (newValue == MVVM.Parameter.Empty)
            {
                Meal = new Meal
                {
                    EatingTime = DateTime.UtcNow
                };
                HealthTrackerDbContext.Add(Meal);
                MapFrom(Meal);
            }
            else if (newValue is Meal meal)
            {
                Meal = meal;
                HealthTrackerDbContext.Attach(Meal);
                MapFrom(Meal);
            }
        }

        #endregion

        #region Id

        private int? _mealId;

        public int? MealId
        {
            get => _mealId;
            protected set => SetProperty(ref _mealId, value);
        }

        #endregion

        #region Name

        private string _name;

        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        #endregion

        #region Date and Time

        private DateAndTime _eatingTime;

        public DateTime EatingTime
        {
            get => _eatingTime.UtcDateTime;
            set
            {
                _eatingTime.UtcDateTime = value;
                OnPropertyChanged(nameof(DateOfEatingTime));
                OnPropertyChanged(nameof(TimeOfEatingTime));
            }
        }

        public DateTime DateOfEatingTime
        {
            get => _eatingTime.LocalDate;
            set
            {
                _eatingTime.LocalDate = value;
                OnPropertyChanged(nameof(DateOfEatingTime));
            }
        }

        public TimeSpan TimeOfEatingTime
        {
            get => _eatingTime.LocalTime;
            set
            {
                _eatingTime.LocalTime = value;
                OnPropertyChanged(nameof(TimeOfEatingTime));
            }
        }

        #endregion


        #region SaveChanges

        protected void SaveChanges()
        {
            MapTo(Meal);
            HealthTrackerDbContext.SaveChanges();
        }

        #endregion
    }
}

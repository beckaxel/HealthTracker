using System;
using HealthTracker.Common;
using HealthTracker.Models;
using HealthTracker.MVVM;
using HealthTracker.Services;
using HealthTracker.Storage;

namespace HealthTracker.ViewModels
{
    public class BeverageViewModel : ViewModelBase
    {
        protected INavigationService NavigationService { get; }
        protected Beverage Beverage { get; private set; }
        protected HealthTrackerDbContext HealthTrackerDbContext { get; }

        public BeverageViewModel
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
                Beverage = new Beverage
                {
                    DrinkingTime = DateTime.UtcNow
                };
                HealthTrackerDbContext.Add(Beverage);
                MapFrom(Beverage);
            }
            else if (newValue is Beverage beverage)
            {
                Beverage = beverage;
                HealthTrackerDbContext.Attach(Beverage);
                MapFrom(Beverage);
            }
        }

        #endregion

        #region Id

        private int? _beverageId;

        public int? BeverageId
        {
            get => _beverageId;
            protected set => SetProperty(ref _beverageId, value);
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

        private DateAndTime _drinkingTime;

        public DateTime DrinkingTime
        {
            get => _drinkingTime.UtcDateTime;
            set
            {
                _drinkingTime.UtcDateTime = value;
                OnPropertyChanged(nameof(DateOfDrinkingTime));
                OnPropertyChanged(nameof(TimeOfDrinkingTime));
            }
        }

        public DateTime DateOfDrinkingTime
        {
            get => _drinkingTime.LocalDate;
            set
            {
                _drinkingTime.LocalDate = value;
                OnPropertyChanged(nameof(DateOfDrinkingTime));
            }
        }

        public TimeSpan TimeOfDrinkingTime
        {
            get => _drinkingTime.LocalTime;
            set
            {
                _drinkingTime.LocalTime = value;
                OnPropertyChanged(nameof(TimeOfDrinkingTime));
            }
        }

        #endregion

        #region Amount

        private float _amount;

        public float Amount
        {
            get => _amount;
            set => SetProperty(ref _amount, value);
        }
        
        #endregion

        #region SaveChanges

        protected void SaveChanges()
        {
            MapTo(Beverage);
            HealthTrackerDbContext.SaveChanges();
        }

        #endregion
    }
}

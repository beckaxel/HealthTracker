using System;
using HealthTracker.Common;
using HealthTracker.Models;
using HealthTracker.MVVM;

namespace HealthTracker.ViewModels
{
    public class BeverageViewModel : ViewModelBase
    {
        protected Beverage Beverage { get; private set; }
     
        #region Parameter Handling

        protected override void OnParameterChanged(object oldValue, object newValue)
        {
            if (newValue == MVVM.Parameter.Empty)
            {
                Beverage = new Beverage
                {
                    DrinkingTime = DateTime.UtcNow
                };
                MapFrom(Beverage);
            }
            else if (newValue is Beverage beverage)
            {
                Beverage = beverage;
                MapFrom(Beverage);
            }
        }

        #endregion

        #region Id

        private int? _beverageId;

        public int? BeverageId
        {
            get => _beverageId;
            set => SetProperty(ref _beverageId, value);
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

        #region Quantity

        private float _quantity;

        public float Quantity
        {
            get => _quantity;
            set => SetProperty(ref _quantity, value);
        }
        
        #endregion
    }
}

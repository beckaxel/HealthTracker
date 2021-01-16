using System;
using HealthTracker.MVVM;

namespace HealthTracker.ViewModels
{
    public class DrinkingQuantityPerDayViewModel : ViewModelBase
    {
        private DateTime _day;
        public DateTime Day
        {
            get => _day;
            set => SetProperty(ref _day, value);
        }


        private float _quantity;
        public float Quantity
        {
            get => _quantity;
            set
            {
                SetProperty(ref _quantity, value);
                OnPropertyChanged(nameof(Percent));
            }
        }

        private float _drinkingQuantityPerDay;
        public float DrinkingQuantityPerDay
        {
            get => _drinkingQuantityPerDay;
            set
            {
                SetProperty(ref _drinkingQuantityPerDay, value);
                OnPropertyChanged(nameof(Percent));
            } 
        }

        public float Percent => (Quantity <= 0 || DrinkingQuantityPerDay <= 0) ? 0 : Quantity / DrinkingQuantityPerDay;
    }
}

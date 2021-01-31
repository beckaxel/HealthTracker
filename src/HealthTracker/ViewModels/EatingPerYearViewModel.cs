using System;
using HealthTracker.MVVM;

namespace HealthTracker.ViewModels
{
    public class EatingPerYearViewModel : ViewModelBase
    {
        private int _totalEatingDays;
        public int TotalEatingDays
        {
            get => _totalEatingDays;
            set
            {
                if (SetProperty(ref _totalEatingDays, value))
                {
                    OnPropertyChanged(nameof(MeatDaysPerYearPercent));
                    OnPropertyChanged(nameof(FishDaysPerYearPercent));
                    OnPropertyChanged(nameof(VegetarianDaysPerYearPercent));
                    OnPropertyChanged(nameof(VeganDaysPerYearPercent));
                }
            }
        }

        private int _meatDaysPerYear;
        public int MeatDaysPerYear
        {
            get => _meatDaysPerYear;
            set
            {
                if (SetProperty(ref _meatDaysPerYear, value))
                    OnPropertyChanged(nameof(MeatDaysPerYearPercent));
            }
        }

        public float MeatDaysPerYearPercent => (MeatDaysPerYear <= 0 || TotalEatingDays <= 0) ? 0 : MeatDaysPerYear / Convert.ToSingle(TotalEatingDays);

        private int _fishDaysPerYear;
        public int FishDaysPerYear
        {
            get => _fishDaysPerYear;
            set
            {
                if (SetProperty(ref _fishDaysPerYear, value))
                    OnPropertyChanged(nameof(FishDaysPerYearPercent));
            }
        }

        public float FishDaysPerYearPercent => (FishDaysPerYear <= 0 || TotalEatingDays <= 0) ? 0 : FishDaysPerYear / Convert.ToSingle(TotalEatingDays);

        private int _vegetarianDaysPerYear;
        public int VegetarianDaysPerYear
        {
            get => _vegetarianDaysPerYear;
            set
            {
                if (SetProperty(ref _vegetarianDaysPerYear, value))
                    OnPropertyChanged(nameof(VegetarianDaysPerYearPercent));
            }
        }

        public float VegetarianDaysPerYearPercent => (VegetarianDaysPerYear <= 0 || TotalEatingDays <= 0) ? 0 : VegetarianDaysPerYear / Convert.ToSingle(TotalEatingDays);

        private int _veganDaysPerYear;
        public int VeganDaysPerYear
        {
            get => _veganDaysPerYear;
            set
            {
                if (SetProperty(ref _veganDaysPerYear, value))
                    OnPropertyChanged(nameof(VeganDaysPerYearPercent));
            }
        }

        public float VeganDaysPerYearPercent => (VeganDaysPerYear <= 0 || TotalEatingDays <= 0) ? 0 : VeganDaysPerYear / Convert.ToSingle(TotalEatingDays);

    }
}

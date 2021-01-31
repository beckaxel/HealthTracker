using System;
using HealthTracker.Models;
using HealthTracker.MVVM;

namespace HealthTracker.ViewModels
{
    public class EatingPerDayViewModel : ViewModelBase
    {
        private DateTime _day;
        public DateTime Day
        {
            get => _day;
            set => SetProperty(ref _day, value);
        }

        private DateTime _firstEatingTime;
        public DateTime FirstEatingTime
        {
            get => _firstEatingTime;
            set => SetProperty(ref _firstEatingTime, value);
        }

        private DateTime _lastEatingTime;
        public DateTime LastEatingTime
        {
            get => _lastEatingTime;
            set => SetProperty(ref _lastEatingTime, value);
        }

        private Diet _diet;
        public Diet Diet
        {
            get => _diet;
            set => SetProperty(ref _diet, value);
        }

        private bool _hadBreakfast;
        public bool HadBreakfast
        {
            get => _hadBreakfast;
            set => SetProperty(ref _hadBreakfast, value);
        }

        private bool _hadLunch;
        public bool HadLunch
        {
            get => _hadLunch;
            set => SetProperty(ref _hadLunch, value);
        }

        private bool _hadDinner;
        public bool HadDinner
        {
            get => _hadDinner;
            set => SetProperty(ref _hadDinner, value);
        }

        private int _numberOfSnacks;
        public int NumberOfSnacks
        {
            get => _numberOfSnacks;
            set => SetProperty(ref _numberOfSnacks, value);
        }
    }
}

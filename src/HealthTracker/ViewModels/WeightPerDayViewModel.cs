using System;
using HealthTracker.MVVM;

namespace HealthTracker.ViewModels
{
    public class WeightPerDayViewModel : ViewModelBase
    {
        private DateTime _day;
        public DateTime Day
        {
            get => _day;
            set
            {
                if (SetProperty(ref _day, value))
                {
                    OnPropertyChanged(nameof(ActualBodyMassIndex));
                    OnPropertyChanged(nameof(YesterdayBodyMassIndex));
                    OnPropertyChanged(nameof(LastWeekBodyMassIndex));
                    OnPropertyChanged(nameof(LastMonthBodyMassIndex));
                    OnPropertyChanged(nameof(LastYearBodyMassIndex));
                }
            }
        }

        private float _userHeight;
        public float UserHeight
        {
            get => _userHeight;
            set => SetProperty(ref _userHeight, value);
        }

        private float CalcBodyMassIndex(float weight)
        {
            if (weight <= 0 || UserHeight <= 0)
                return 0;
            var height = UserHeight / 100;
            return weight / (height * height);
        }

        public DateTime _actualMeasureTime;
        public DateTime ActualMeasureTime
        {
            get => _actualMeasureTime;
            set => SetProperty(ref _actualMeasureTime, value);
        }
                
        public TrendDirection TrendDirection
        {
            get
            {
                if (TrendValue < -0.100f)
                    return TrendDirection.FastDown;
                if (TrendValue < -0.020f)
                    return TrendDirection.SlowDown;
                if (TrendValue <= 0.020f)
                    return TrendDirection.None;
                if (TrendValue <= 0.100f)
                    return TrendDirection.SlowUp;
                return TrendDirection.FastUp;
            }
        }

        private float _trendValue;
        public float TrendValue
        {
            get => _trendValue;
            set
            {
                if (SetProperty(ref _trendValue, value))
                    OnPropertyChanged(nameof(TrendDirection));
            }
        }

        private float _actualWeight;
        public float ActualWeight
        {
            get => _actualWeight;
            set
            {
                if (SetProperty(ref _actualWeight, value))
                    OnPropertyChanged(nameof(ActualBodyMassIndex));
            }
        }

        public float ActualBodyMassIndex => CalcBodyMassIndex(ActualWeight);

        private float _yesterdayWeight;
        public float YesterdayWeight
        {
            get => _yesterdayWeight;
            set
            {
                if (SetProperty(ref _yesterdayWeight, value))
                    OnPropertyChanged(nameof(YesterdayBodyMassIndex));
            }
        }

        public float YesterdayBodyMassIndex => CalcBodyMassIndex(YesterdayWeight);

        private float _lastWeekWeight;
        public float LastWeekWeight
        {
            get => _lastWeekWeight;
            set
            {
                if (SetProperty(ref _lastWeekWeight, value))
                    OnPropertyChanged(nameof(LastWeekBodyMassIndex));
            }
        }

        public float LastWeekBodyMassIndex => CalcBodyMassIndex(LastWeekWeight);

        private float _lastMonthWeight;
        public float LastMonthWeight
        {
            get => _lastMonthWeight;
            set
            {
                if (SetProperty(ref _lastMonthWeight, value))
                    OnPropertyChanged(nameof(LastMonthBodyMassIndex));
            }
        }

        public float LastMonthBodyMassIndex => CalcBodyMassIndex(LastMonthWeight);

        private float _lastYearWeight;
        public float LastYearWeight
        {
            get => _lastYearWeight;
            set
            {
                if (SetProperty(ref _lastYearWeight, value))
                    OnPropertyChanged(nameof(LastYearBodyMassIndex));
            }
        }

        public float LastYearBodyMassIndex => CalcBodyMassIndex(LastYearWeight);
    }
}

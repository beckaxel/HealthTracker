using System;
using HealthTracker.Common;
using HealthTracker.Models;
using HealthTracker.MVVM;

namespace HealthTracker.ViewModels
{
    public class WeightViewModel : ViewModelBase
    {
        protected BodyMeasurement BodyMeasurement { get; private set; }

        #region Parameter Handling

        protected override void OnParameterChanged(object oldValue, object newValue)
        {
            if (newValue == MVVM.Parameter.Empty)
            {
                BodyMeasurement = new BodyMeasurement
                {
                    MeasureTime = DateTime.UtcNow
                };
                MapFrom(BodyMeasurement);
            }
            else if (newValue is BodyMeasurement bodyMeasurement)
            {
                BodyMeasurement = bodyMeasurement;
                MapFrom(BodyMeasurement);
            }
        }

        #endregion

        #region Id

        private int? _bodyMeasurmentId;

        public int? BodyMeasurementId
        {
            get => _bodyMeasurmentId;
            set => SetProperty(ref _bodyMeasurmentId, value);
        }

        #endregion

        #region Date and Time

        private DateAndTime _measureTime;

        public DateTime MeasureTime
        {
            get => _measureTime.UtcDateTime;
            set
            {
                _measureTime.UtcDateTime = value;
                OnPropertyChanged(nameof(DateOfMeasureTime));
                OnPropertyChanged(nameof(TimeOfMeasureTime));
            }
        }

        public DateTime DateOfMeasureTime
        {
            get => _measureTime.LocalDate;
            set
            {
                _measureTime.LocalDate = value;
                OnPropertyChanged(nameof(DateOfMeasureTime));
            }
        }

        public TimeSpan TimeOfMeasureTime
        {
            get => _measureTime.LocalTime;
            set
            {
                _measureTime.LocalTime = value;
                OnPropertyChanged(nameof(TimeOfMeasureTime));
            }
        }

        #endregion

        #region Weight

        private float? _weight;

        public float? Weight
        {
            get => _weight;
            set => SetProperty(ref _weight, value);
        }

        #endregion
    }
}

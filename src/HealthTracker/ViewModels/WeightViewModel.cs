using System;
using HealthTracker.Common;
using HealthTracker.Models;
using HealthTracker.MVVM;

namespace HealthTracker.ViewModels
{
    public class WeightViewModel : ViewModelBase
    {
        #region Parameter Handling

        protected override void OnParameterChanged(object oldValue, object newValue)
        {
            if (newValue == MVVM.Parameter.Empty)
            {
                FromWeight(new BodyMeasurement
                {
                    Date = DateTime.UtcNow,
                    Weight = default
                });
            }
            else if (newValue is BodyMeasurement weight)
            {
                FromWeight(weight);
            }
        }

        #endregion

        #region Id

        private int? _id;

        public int? Id
        {
            get => _id;
            protected set => SetProperty(ref _id, value);
        }

        #endregion

        #region Date and Time

        private DateAndTime _dateAndTime;

        public DateTime Date
        {
            get => _dateAndTime.LocalDate;
            set
            {
                _dateAndTime.LocalDate = value;
                OnPropertyChanged(nameof(Date));
            }
        }

        public TimeSpan Time
        {
            get => _dateAndTime.LocalTime;
            set
            {
                _dateAndTime.LocalTime = value;
                OnPropertyChanged(nameof(Date));
            }
        }

        #endregion

        #region Amount

        private float? _amount;

        public float? Amount
        {
            get => _amount;
            set => SetProperty(ref _amount, value);
        }

        #endregion

        #region Conversion

        protected void FromWeight(BodyMeasurement weight)
        {
            Id = weight.BodyMeasurementId;
            Date = weight.Date.ToLocalTime().Date;
            Time = weight.Date.ToLocalTime().TimeOfDay;
            Amount = weight.Weight;
        }

        protected BodyMeasurement ToWeight()
        {
            return new BodyMeasurement
            {
                BodyMeasurementId = Id ?? default,
                Date = _dateAndTime.UtcDateTime,
                Weight = Amount ?? default
            };
        }

        #endregion
    }
}

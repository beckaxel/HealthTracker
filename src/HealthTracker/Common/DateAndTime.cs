using System;
namespace HealthTracker.Common
{
    public struct DateAndTime
    {
        public DateTime UtcDateTime { get; set; }

        public DateTime UtcDate
        {
            get => UtcDateTime.Date;
            set => UtcDateTime = value.Date.Add(UtcDateTime.TimeOfDay);
        }

        public TimeSpan UtcTime
        {
            get => UtcDateTime.TimeOfDay;
            set => UtcDateTime = UtcDateTime.Date.Add(value);
        }

        public DateTime LocalDateTime
        {
            get => UtcDateTime.ToLocalTime();
            set => UtcDateTime = value.ToUniversalTime();
        }

        public DateTime LocalDate
        {
            get => LocalDateTime.Date;
            set => LocalDateTime = value.Date.Add(LocalTime);
        }

        public TimeSpan LocalTime
        {
            get => LocalDateTime.TimeOfDay;
            set => LocalDateTime = LocalDateTime.Date.Add(value);
        }
    }
}

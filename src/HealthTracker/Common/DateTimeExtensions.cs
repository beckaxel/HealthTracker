using System;
using System.Globalization;

namespace HealthTracker.Common
{
    public static class DateTimeExtensions
    {
        public static int GetWeekOfYear(this DateTime dateTime, CultureInfo culture)
        {   
            var calendar = culture.Calendar;
            var weekRule = culture.DateTimeFormat.CalendarWeekRule;
            var firstDayOfWeek = culture.DateTimeFormat.FirstDayOfWeek;
            return calendar.GetWeekOfYear(dateTime, weekRule, firstDayOfWeek);
        }
    }
}

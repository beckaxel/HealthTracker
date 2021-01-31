using System;
using System.Globalization;
using HealthTracker.Common;
using Xamarin.Forms;

namespace HealthTracker.MVVM
{
    [ContentProperty(nameof(Name))]
    public class Filter : BindableObject
    {
        public string Name { get; set; }

        public static DateTime Translate(string name)
        {
            var fromDate = DateTime.Today.ToUniversalTime();
            if (name == "Woche")
                fromDate = fromDate.AddDays(-6);
            else if (name == "Monat")
                fromDate = fromDate.AddMonths(-1);
            else if (name == "Jahr")
                fromDate = fromDate.AddYears(-1);

            return fromDate;
        }

        public static string FormatDateTime(string name, DateTime dateTime)
        {
            if (name == "Woche")
                return dateTime.ToString("dddd");
            else if (name == "Monat")
                return dateTime.ToString("dd.MM");
            else if (name == "Jahr")
                return $"KW {dateTime.GetWeekOfYear(CultureInfo.CurrentCulture):00} {dateTime:yyyy}";
            else
                return dateTime.ToString("t");
        }
    }
}
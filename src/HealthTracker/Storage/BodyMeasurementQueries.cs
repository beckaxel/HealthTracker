using System;
using System.Linq;
using HealthTracker.Models;

namespace HealthTracker.Storage
{
    public static class BodyMeasurementQueries
    {
        public static BodyMeasurement LatestOrDefault(this IQueryable<BodyMeasurement> source)
        {
            return source.OrderByDescending(bm => bm.MeasureTime).FirstOrDefault();
        }

        public static IQueryable<BodyMeasurement> Filter(this IQueryable<BodyMeasurement> source, string name)
        {
            var fromDate = DateTime.Today;
            if (name == "Woche")
                fromDate = fromDate.AddDays(-7);
            else if (name == "Monat")
                fromDate = fromDate.AddMonths(-1);
            else if (name == "Jahr")
                fromDate = fromDate.AddYears(-1);

            return source.Where(bm => bm.MeasureTime >= fromDate);
        }
    }
}

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

        public static IQueryable<BodyMeasurement> LastXDays(this IQueryable<BodyMeasurement> source, int days)
        {
            var fromDate = DateTime.Today.Subtract(TimeSpan.FromDays(days));
            return source.Where(bm => bm.MeasureTime >= fromDate);
        }
    }
}

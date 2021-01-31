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
            var fromDate = MVVM.Filter.Translate(name);
            return source.Where(bm => bm.MeasureTime >= fromDate);
        }
    }
}

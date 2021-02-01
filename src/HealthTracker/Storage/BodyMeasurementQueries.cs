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

        public static float CalcTrendValue(this IQueryable<BodyMeasurement> source, DateTime pointInTime)
        {
            var from = pointInTime.ToUniversalTime().Date.AddDays(-14);
            var to = pointInTime.ToUniversalTime().Date;

            var weights = source
                .Where(m => m.Weight.HasValue && m.MeasureTime.Date >= from && m.MeasureTime.Date <= pointInTime)
                .OrderByDescending(m => m.MeasureTime)
                .Select(m => m.Weight.Value)
                .ToList();

            var lastWeight = weights.First();
            var average = weights.Average();

            return (lastWeight - average) / 7; 
        }

        public static float GetYesterdayWeight(this IQueryable<BodyMeasurement> source, DateTime pointInTime)
        {
            var date = pointInTime.ToUniversalTime().Date.AddDays(-1);
            return source
                .GroupBy(m => m.MeasureTime.Date)
                .Where(g => g.Key == date)
                .Select(g => g
                    .Where(m => m.Weight.HasValue)
                    .Average(m => m.Weight.Value))
                .FirstOrDefault();
        }

        public static float GetLastWeekWeight(this IQueryable<BodyMeasurement> source, DateTime pointInTime)
        {
            var date = pointInTime.ToUniversalTime().Date.AddDays(-7);
            return source
                .GroupBy(m => m.MeasureTime.Date)
                .Where(g => g.Key == date)
                .Select(g => g
                    .Where(m => m.Weight.HasValue)
                    .Average(m => m.Weight.Value))
                .FirstOrDefault();
        }

        public static float GetLastMonthWeight(this IQueryable<BodyMeasurement> source, DateTime pointInTime)
        {
            var date = pointInTime.ToUniversalTime().Date.AddMonths(-1);
            return source
                .GroupBy(m => m.MeasureTime.Date)
                .Where(g => g.Key == date)
                .Select(g => g
                    .Where(m => m.Weight.HasValue)
                    .Average(m => m.Weight.Value))
                .FirstOrDefault();
        }

        public static float GetLastYearWeight(this IQueryable<BodyMeasurement> source, DateTime pointInTime)
        {
            var date = pointInTime.ToUniversalTime().Date.AddYears(-1);
            return source
                .GroupBy(m => m.MeasureTime.Date)
                .Where(g => g.Key == date)
                .Select(g => g
                    .Where(m => m.Weight.HasValue)
                    .Average(m => m.Weight.Value))
                .FirstOrDefault();
        }
    }
}

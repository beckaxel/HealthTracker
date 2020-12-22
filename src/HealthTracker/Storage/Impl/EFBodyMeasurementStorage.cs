using System;
using System.Collections.Generic;
using System.Linq;
using HealthTracker.Models;
using HealthTracker.Seeds;

namespace HealthTracker.Storage.Impl
{
    public class EFBodyMeasurementStorage : IBodyMeasurementStorage
    {
        private readonly IHealthTrackerDbContextFactory _healthTrackerDbContextFactory;

        public EFBodyMeasurementStorage(IHealthTrackerDbContextFactory healthTrackerDbContextFactory)
        {
            _healthTrackerDbContextFactory = healthTrackerDbContextFactory;
            using var context = _healthTrackerDbContextFactory.Create();
            if (!context.BodyMeasurement.Any())
                BodyMeasurementSeed.Seed(this);
        }

        public IEnumerable<BodyMeasurement> All()
        {
            using var context = _healthTrackerDbContextFactory.Create();
            return context.BodyMeasurement.ToList();
        }

        public IEnumerable<BodyMeasurement> LastXDays(int days)
        {
            var fromDate = DateTime.Today.Subtract(TimeSpan.FromDays(days));
            using var context = _healthTrackerDbContextFactory.Create();
            return context.BodyMeasurement.Where(bm => bm.Date >= fromDate).ToList();
        }

        public BodyMeasurement Find(int id)
        {
            using var context = _healthTrackerDbContextFactory.Create();
            return context.Find<BodyMeasurement>(id);
        }

        public void Insert(BodyMeasurement bodyMeasurement)
        {
            using (var context = _healthTrackerDbContextFactory.Create())
            {
                context.Add(bodyMeasurement);
                context.SaveChanges();
            }
        }

        public BodyMeasurement LatestOrDefault()
        {
            using var context = _healthTrackerDbContextFactory.Create();
            return context.BodyMeasurement
                .OrderByDescending(w => w.Date)
                .FirstOrDefault();
        }

        public void Remove(BodyMeasurement bodyMeasurement)
        {
            using (var context = _healthTrackerDbContextFactory.Create())
            {
                var dbBodyMeasurement = context.Find<BodyMeasurement>(bodyMeasurement.BodyMeasurementId);
                context.Remove(dbBodyMeasurement);
                context.SaveChanges();
            }
        }

        public void Update(BodyMeasurement bodyMeasurement)
        {
            using (var context = _healthTrackerDbContextFactory.Create())
            {
                var dbBodyMeasurement = context.Find<BodyMeasurement>(bodyMeasurement.BodyMeasurementId);
                if (dbBodyMeasurement == null)
                    return;

                dbBodyMeasurement.Date = bodyMeasurement.Date;
                dbBodyMeasurement.Weight = bodyMeasurement.Weight;
                context.SaveChanges();
            }
        }
    }
}

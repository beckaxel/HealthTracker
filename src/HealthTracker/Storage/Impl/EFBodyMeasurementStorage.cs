using System;
using System.Collections.Generic;
using System.Linq;
using HealthTracker.Models;

namespace HealthTracker.Storage.Impl
{
    public class EFBodyMeasurementStorage : IBodyMeasurementStorage
    {
        private readonly IDbContextFactory _healthTrackerDbContextFactory;

        public EFBodyMeasurementStorage(IDbContextFactory healthTrackerDbContextFactory)
        {
            _healthTrackerDbContextFactory = healthTrackerDbContextFactory;
        }

        public IEnumerable<BodyMeasurement> All()
        {
            using var context = _healthTrackerDbContextFactory.CreateHealthTrackerDbContext();
            return context.BodyMeasurement.ToList();
        }

        public IEnumerable<BodyMeasurement> LastXDays(int days)
        {
            using var context = _healthTrackerDbContextFactory.CreateHealthTrackerDbContext();
            return context.BodyMeasurement.LastXDays(days).ToList();
        }

        public BodyMeasurement Find(int id)
        {
            using var context = _healthTrackerDbContextFactory.CreateHealthTrackerDbContext();
            return context.Find<BodyMeasurement>(id);
        }

        public void Insert(BodyMeasurement bodyMeasurement)
        {
            using (var context = _healthTrackerDbContextFactory.CreateHealthTrackerDbContext())
            {
                context.Add(bodyMeasurement);
                context.SaveChanges();
            }
        }

        public BodyMeasurement LatestOrDefault()
        {
            using var context = _healthTrackerDbContextFactory.CreateHealthTrackerDbContext();
            return context.BodyMeasurement.LatestOrDefault();
        }

        public void Remove(BodyMeasurement bodyMeasurement)
        {
            using (var context = _healthTrackerDbContextFactory.CreateHealthTrackerDbContext())
            {
                var dbBodyMeasurement = context.Find<BodyMeasurement>(bodyMeasurement.BodyMeasurementId);
                context.Remove(dbBodyMeasurement);
                context.SaveChanges();
            }
        }

        public void Update(BodyMeasurement bodyMeasurement)
        {
            using (var context = _healthTrackerDbContextFactory.CreateHealthTrackerDbContext())
            {
                var dbBodyMeasurement = context.Find<BodyMeasurement>(bodyMeasurement.BodyMeasurementId);
                if (dbBodyMeasurement == null)
                    return;

                dbBodyMeasurement.MeasureTime = bodyMeasurement.MeasureTime;
                dbBodyMeasurement.Weight = bodyMeasurement.Weight;
                context.SaveChanges();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using HealthTracker.Models;

namespace HealthTracker.Storage
{
    public interface IBodyMeasurementStorage
    {
        IEnumerable<BodyMeasurement> All();
        public IEnumerable<BodyMeasurement> LastXDays(int days);
        BodyMeasurement Find(int id);
        void Insert(BodyMeasurement entity);
        BodyMeasurement LatestOrDefault();
        void Remove(BodyMeasurement entity);
        void Update(BodyMeasurement entity);
    }
}
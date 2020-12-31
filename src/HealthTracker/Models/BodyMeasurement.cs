using System;
using System.Collections.Generic;

namespace HealthTracker.Models
{
    public class BodyMeasurement
    {
        public int BodyMeasurementId { get; set; }

        public DateTime MeasureTime { get; set; }

        public float? Weight { get; set; }
    }
}

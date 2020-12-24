using System;
using System.Collections.Generic;

namespace HealthTracker.Models
{
    public class BodyMeasurement
    {
        public int BodyMeasurementId { get; set; }

        public DateTime MeasureTime { get; set; }

        public virtual ICollection<Tag> Tags { get; set; } = new HashSet<Tag>();

        public float? Weight { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace HealthTracker.Models
{
    public class BodyMeasurement : IDatable, ITagable
    {
        public int BodyMeasurementId { get; set; }

        public DateTime Date { get; set; }

        public virtual ICollection<Tag> Tags { get; set; } = new HashSet<Tag>();

        public float Weight { get; set; }
    }
}

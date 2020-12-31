using System;
using System.Collections.Generic;

namespace HealthTracker.Models
{
    public class SleepLog
    {
        public int SleepLogId { get; set; }

        public DateTime Date { get; set; }

        public DateTime BedTime { get; set; }

        public DateTime LightOffTime { get; set; }

        public DateTime FallAsleepTime { get; set; }

        public virtual ICollection<Waking> Wakings { get; set; } = new HashSet<Waking>();

        public DateTime WakeUpTime { get; set; }

        public DateTime GetUpTime { get; set; }
    }
}

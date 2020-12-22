using System;
using System.Collections.Generic;

namespace HealthTracker.Models
{
    public class SleepLog : IDatable, ITagable
    {
        public int SleepLogId { get; set; }

        public DateTime Date { get; set; }

        public virtual ICollection<Tag> Tags { get; set; } = new HashSet<Tag>();

        public DateTime BedTime { get; set; }

        public DateTime LightOffTime { get; set; }

        public DateTime FallAsleepTime { get; set; }

        public virtual ICollection<Waking> Wakings { get; set; } = new HashSet<Waking>();

        public DateTime WakeUpTime { get; set; }

        public DateTime GetUpTime { get; set; }
    }
}

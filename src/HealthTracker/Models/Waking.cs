using System;

namespace HealthTracker.Models
{
    public class Waking
    {
        public int WakingId { get; set; }

        public int SleepLogId { get; set; }
        public SleepLog SleepLog { get; set; }

        public TimeSpan Duration { get; set; }
    }
}
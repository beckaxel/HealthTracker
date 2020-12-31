using System;

namespace HealthTracker.Models
{
    public class User
    {
        public int UserId { get; set; }

        public DateTime BirthDate { get; set; }

        public float Height { get; set; }

        public Gender Gender { get; set; }
    }
}

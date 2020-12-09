using System;
using HealthTracker.Common;

namespace HealthTracker.Models
{
    public class User : Entity
    {   
        public DateTime BirthDate { get; set; }

        public decimal Height { get; set; }

        public Gender Gender { get; set; }
    }
}

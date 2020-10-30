using System;
using SQLite;
using HealthTracker.Common;

namespace HealthTracker.Models
{
    public class User
    {   
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public DateTime BirthDate { get; set; }

        public decimal Height { get; set; }

        public Gender Gender { get; set; }
    }
}

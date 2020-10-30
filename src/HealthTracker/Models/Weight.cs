using System;
using SQLite;

namespace HealthTracker.Models
{
    public class Weight
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [Indexed]
        public DateTime Date { get; set; }

        public decimal Amount { get; set; }        
    }
}

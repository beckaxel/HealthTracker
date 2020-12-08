using System;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace HealthTracker.Models
{
    public class Beverage
    {   
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [ForeignKey(typeof(BeverageType))]
        public int BeverageTypeId { get; set; }

        [Indexed]
        public DateTime Date { get; set; }

        public decimal Amount { get; set; }

        [ManyToOne]
        public BeverageType BeverageType { get; set; }
    }
}

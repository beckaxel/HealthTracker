using System.Collections.Generic;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace HealthTracker.Models
{
    public class BeverageType
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Name { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.None)]
        public List<Beverage> Beverages { get; set; }
    }
}
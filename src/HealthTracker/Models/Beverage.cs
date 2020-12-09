using SQLite;

namespace HealthTracker.Models
{
    public class Beverage : ActivityEntity
    {   
        [Indexed]
        public int BeverageTypeId { get; set; }

        public decimal Amount { get; set; }
    }
}

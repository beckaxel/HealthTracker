using System.Collections.Generic;
using HealthTracker.Models;

namespace HealthTracker.Storage
{
    public interface IDrinkingStorage
    {
        IEnumerable<Beverage> All();
        Beverage Find(int id);
        void Insert(Beverage drinking);
        Beverage LatestOrDefault();
        float AmountToday();
        void Remove(Beverage drinking);
        void Update(Beverage drinking);
    }
}
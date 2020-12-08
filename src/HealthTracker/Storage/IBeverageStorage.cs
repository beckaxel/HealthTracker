using System;
using System.Collections.Generic;
using HealthTracker.Models;

namespace HealthTracker.Storage
{
    public interface IBeverageStorage
    {
        IEnumerable<Beverage> All();
        Beverage Find(int id);
        bool Exists(Beverage beverage);
        void Insert(Beverage beverage);
        void Remove(Beverage beverage);
        void Update(Beverage beverage);
        void UpdateOrInsert(Beverage beverage);
        Beverage LatestOrDefault();
    }
}

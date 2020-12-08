using System.Collections.Generic;
using HealthTracker.Models;

namespace HealthTracker.Storage
{
    public interface IBeverageTypeStorage
    {
        IEnumerable<BeverageType> All();
        BeverageType Find(int id);
        bool Exists(BeverageType beverageType);
        void Insert(BeverageType beverageType);
        void Remove(BeverageType beverageType);
        void Update(BeverageType beverageType);
        void UpdateOrInsert(BeverageType beverageType);
    }
}

using System.Collections.Generic;
using HealthTracker.Models;

namespace HealthTracker.Storage
{
    public interface IWeightStorage
    {
        IEnumerable<Weight> All();
        Weight Find(int id);
        bool Exists(Weight weight);
        void Insert(Weight weight);
        void Remove(Weight weight);
        void Update(Weight weight);
        void UpdateOrInsert(Weight weight);
        Weight LatestOrDefault();
    }
}
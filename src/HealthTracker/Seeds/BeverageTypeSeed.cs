using HealthTracker.Models;
using HealthTracker.Storage;

namespace HealthTracker.Seeds
{
    public static class BeverageTypeSeed
    {
        public static void Seed(IBeverageTypeStorage beverageTypeStorage)
        {
            beverageTypeStorage.Insert(new BeverageType { Name = "Wasser" });
            beverageTypeStorage.Insert(new BeverageType { Name = "Tee" });
            beverageTypeStorage.Insert(new BeverageType { Name = "Limonade" });
            beverageTypeStorage.Insert(new BeverageType { Name = "Cola" });
            beverageTypeStorage.Insert(new BeverageType { Name = "Milch" });
        }
    }
}

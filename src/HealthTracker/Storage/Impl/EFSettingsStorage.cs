using System.Collections.Generic;
using System.Linq;
using HealthTracker.Models;

namespace HealthTracker.Storage.Impl
{
    public class EFSettingsStorage : ISettingsStorage
    {
        private readonly IDbContextFactory _healthTrackerDbContextFactory;

        public EFSettingsStorage(IDbContextFactory healthTrackerDbContextFactory)
        {
            _healthTrackerDbContextFactory = healthTrackerDbContextFactory;
        }

        public IEnumerable<string> AllKeys
        {
            get
            {
                using var context = _healthTrackerDbContextFactory.CreateHealthTrackerDbContext();
                return context.Setting.Select(s => s.Key).ToList();
            }
        }

        public string GetValue(string key)
        {
            using var conext = _healthTrackerDbContextFactory.CreateHealthTrackerDbContext();
            return conext.Setting
                .Where(s => s.Key == key)
                .Select(s => s.Value)
                .FirstOrDefault();
        }

        public bool HasKey(string key)
        {
            using var context = _healthTrackerDbContextFactory.CreateHealthTrackerDbContext();
            return context.Setting.Any(s => s.Key == key);
        }

        public void SetValue(string key, string value)
        {
            using (var context = _healthTrackerDbContextFactory.CreateHealthTrackerDbContext())
            {
                var setting = context.Setting.FirstOrDefault(s => s.Key == key);
                if (setting == null)
                    context.Add(new Setting { Key = key, Value = value });
                else
                    setting.Value = value;

                context.SaveChanges();
            }
        }
    }
}

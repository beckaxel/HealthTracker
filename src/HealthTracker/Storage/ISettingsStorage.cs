using System.Collections.Generic;

namespace HealthTracker.Storage
{
    public interface ISettingsStorage
    {
        IEnumerable<string> AllKeys { get; }
        bool HasKey(string key);
        string GetValue(string key);
        void SetValue(string key, string value);
    }
}

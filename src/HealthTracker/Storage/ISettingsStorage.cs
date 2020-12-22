using System.Collections.Generic;

namespace HealthTracker.Storage
{
    public interface ISettingsStorage
    {
        IEnumerable<string> AllKeys { get; }

        string GetValue(string key);
        bool HasKey(string key);
        void SetValue(string key, string value);
    }
}
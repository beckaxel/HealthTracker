using System;
using System.Collections.Generic;
using System.Linq;
using HealthTracker.Models;
using SQLite;

namespace HealthTracker.Storage.Impl
{
    public class SQLiteSettingsStorage : ISettingsStorage
    {
        protected SQLiteConnection Connection { get; }

        public SQLiteSettingsStorage(SQLiteConnection connection)
        {
            connection.CreateTable<Setting>(CreateFlags.None);
            Connection = connection;
        }

        public IEnumerable<string> AllKeys => Connection.Table<Setting>().Select(s => s.Key);

        public bool HasKey(string key)
        {
            return Connection
                .Table<Setting>()
                .Any(s => s.Key == key);
        }

        public string GetValue(string key)
        {
            return Connection
                .Table<Setting>()
                .Where(s => s.Key == key)
                .Select(s => s.Value)
                .FirstOrDefault();
        }

        public void SetValue(string key, string value)
        {
            var setting = Connection
                .Table<Setting>()
                .FirstOrDefault(s => s.Key == key);

            if (setting == null)
            {
                Connection.Insert(new Setting { Key = key, Value = value });
            }
            else
            {   
                setting.Value = value;
                Connection.Update(setting);
            }
        }
    }
}

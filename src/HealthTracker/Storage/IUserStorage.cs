using System;
using HealthTracker.Models;

namespace HealthTracker.Storage
{
    public interface IUserStorage
    {
        User GetOrAdd();
        void Update(User user);
    }
}

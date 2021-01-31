using System;
using System.Collections.Generic;
using HealthTracker.MVVM;

namespace HealthTracker.Services
{
    public interface IViewService
    {
        string ViewNameSuffix { get; }
        string GetNameOfView(ViewBase view);
        string GetNameOfView(Type viewType);
        IEnumerable<TBaseType> GetViews<TBaseType>() where TBaseType : ViewBase;
        ViewBase GetView(string name);
    }
}

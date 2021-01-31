using System;
using HealthTracker.MVVM;

namespace HealthTracker.Services
{
    public interface IViewModelService
    {
        string ViewModelNameSuffix { get; }
        string GetNameOfViewModel(ViewModelBase view);
        string GetNameOfViewModel(Type viewModelType);
        ViewModelBase GetViewModel(string name);
    }
}

using HealthTracker.MVVM;

namespace HealthTracker.Services
{
    public interface IViewModelService
    {
        string ViewModelNameSuffix { get; }
        ViewModelBase GetViewModel(string name);
    }
}

using HealthTracker.MVVM;

namespace HealthTracker.Services
{
    public interface IViewModelService
    {
        ViewModelBase GetViewModel(string name);
    }
}

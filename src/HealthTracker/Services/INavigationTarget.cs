using HealthTracker.MVVM;

namespace HealthTracker.Services
{
    public interface INavigationTarget
    {
        ViewBase CurrentView { get; set; }
    }
}

using HealthTracker.MVVM;
using HealthTracker.Services;

namespace HealthTracker.ViewModels
{
    public class SleepingSectionViewModel : SectionMainViewModel
    {
        public SleepingSectionViewModel(INavigationService navigationService)
            : base(navigationService)
        {
        }
    }
}

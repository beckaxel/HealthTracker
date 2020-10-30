using HealthTracker.MVVM;
using HealthTracker.Services;

namespace HealthTracker.ViewModels
{
    public class SleepSectionViewModel : SectionMainViewModel
    {
        public SleepSectionViewModel(INavigationService navigationService)
            : base(navigationService)
        {
        }
    }
}

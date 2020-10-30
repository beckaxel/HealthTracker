using HealthTracker.MVVM;
using HealthTracker.Services;

namespace HealthTracker.ViewModels
{
    public class BeveragesSectionViewModel : SectionMainViewModel
    {
        public BeveragesSectionViewModel(INavigationService navigationService)
            : base(navigationService)
        {
        }
    }
}

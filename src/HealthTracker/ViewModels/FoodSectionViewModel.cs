using HealthTracker.MVVM;
using HealthTracker.Services;

namespace HealthTracker.ViewModels
{
    public class FoodSectionViewModel : SectionMainViewModel
    {
        public FoodSectionViewModel(INavigationService navigationService)
            : base(navigationService)
        {
        }
    }
}

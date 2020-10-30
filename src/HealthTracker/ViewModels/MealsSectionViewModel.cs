using HealthTracker.MVVM;
using HealthTracker.Services;

namespace HealthTracker.ViewModels
{
    public class MealsSectionViewModel : SectionMainViewModel
    {
        public MealsSectionViewModel(INavigationService navigationService)
            : base(navigationService)
        {
        }
    }
}

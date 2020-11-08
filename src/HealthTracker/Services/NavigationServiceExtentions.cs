using HealthTracker.MVVM;

namespace HealthTracker.Services
{
    public static class NavigationServiceExtentions
    {
        public static void NavigateTo(this INavigationService navigationService, string name)
        {
            navigationService.NavigateTo(name, Parameter.Empty);
        }

        public static void NavigateToActiveSection(this INavigationService navigationService)
        {
            navigationService.NavigateTo(navigationService.ActiveSection?.Name ?? "NoEnabledSection");
        }
    }
}

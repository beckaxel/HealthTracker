using System.Windows.Input;
using HealthTracker.MVVM;
using HealthTracker.Services;
using Xamarin.Forms;

namespace HealthTracker.ViewModels
{
    public class NoEnabledSectionViewModel : ViewModelBase
    {
        public INavigationService NavigationService { get; }

        public NoEnabledSectionViewModel(INavigationService navigationService)
        {
            NavigationService = navigationService;
        }

        #region Settings

        private ICommand _openSettingsCommand;

        public ICommand OpenSettingsCommand => GetLazyProperty(ref _openSettingsCommand, () => new Command(OpenSettings));

        private void OpenSettings()
        {
            NavigationService.NavigateTo("Settings");
        }

        #endregion
    }
}

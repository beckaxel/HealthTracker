using System.Collections.Generic;
using System.Windows.Input;
using HealthTracker.MVVM;
using HealthTracker.Services;
using HealthTracker.Storage;
using Xamarin.Forms;

namespace HealthTracker.ViewModels
{
    public class SettingsViewModel : ViewModelBase
    {
        protected INavigationService NavigationService { get; }
        protected ISettingsStorage SettingsStorage { get; }

        public SettingsViewModel(INavigationService navigationService, ISettingsStorage settingsStorage)
        {
            NavigationService = navigationService;
            SettingsStorage = settingsStorage;
        }

        #region Sections

        public IEnumerable<SectionViewModel> Sections => NavigationService.AllSections;

        #endregion

        #region Close

        private ICommand _closeCommand;

        public ICommand CloseCommand => GetLazyProperty(ref _closeCommand, () => new Command(Close));

        public void Close()
        {
            NavigationService.NavigateToActiveSection();
        }

        #endregion
    }
}

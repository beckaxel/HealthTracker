using System.Collections.ObjectModel;
using System.Windows.Input;
using HealthTracker.Services;
using HealthTracker.ViewModels;
using Xamarin.Forms;

namespace HealthTracker.MVVM
{
    public abstract class SectionMainViewModel : ViewModelBase
    {
        protected INavigationService NavigationService { get; }

        protected SectionMainViewModel(INavigationService navigationService)
        {
            NavigationService = navigationService;
            NavigationService.ActiveSectionChanged += NavigationService_ActiveSectionChanged;
        }

        private void NavigationService_ActiveSectionChanged(object sender, ActiveSectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(ActiveSection));
        }

        #region UserSettings

        private ICommand _openUserSettingsCommand;

        public ICommand OpenUserSettingsCommand => GetLazyProperty(ref _openUserSettingsCommand, () => new Command(OpenUserSettings));

        private void OpenUserSettings()
        {
            NavigationService.NavigateTo("User");
        }

        #endregion

        #region AppSettings

        private ICommand _openAppSettingsCommand;

        public ICommand OpenAppSettingsCommand => GetLazyProperty(ref _openAppSettingsCommand, () => new Command(OpenAppSettings));

        private void OpenAppSettings()
        {
            NavigationService.NavigateTo("Settings");
        }

        #endregion

        #region ActiveSection

        public SectionViewModel ActiveSection => NavigationService.ActiveSection;

        #endregion

        #region EnabledSections

        public ObservableCollection<SectionViewModel> Sections => NavigationService.EnabledSections;

        #endregion

        #region NavigateToSection

        private ICommand _navigateToSectionCommand;

        public ICommand NavigateToSectionCommand => GetLazyProperty(ref _navigateToSectionCommand, () => new Command(p => NavigateToSection(p as string)));

        private void NavigateToSection(string name)
        {
            NavigationService.NavigateTo(name);
            NavigationService.ActiveSectionChanged -= NavigationService_ActiveSectionChanged;
        }

        #endregion
    }
}

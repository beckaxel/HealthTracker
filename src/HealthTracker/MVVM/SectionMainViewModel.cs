﻿using System.Collections.ObjectModel;
using System.Windows.Input;
using HealthTracker.Services;
using HealthTracker.ViewModels;
using Xamarin.Forms;

namespace HealthTracker.MVVM
{
    public abstract class SectionMainViewModel : ViewModelBase
    {   
        protected INavigationService NavigationService { get; }
        
        protected SectionMainViewModel
        (   
            INavigationService navigationService
        )
        {   
            NavigationService = navigationService;
            NavigationService.ActiveSectionChanged += NavigationService_ActiveSectionChanged;
        }

        private void NavigationService_ActiveSectionChanged(object sender, ActiveSectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(ActiveSection));
        }

        #region UserSettings

        private ICommand _openUserCommand;

        public ICommand OpenUserCommand => GetLazyProperty(ref _openUserCommand, () => new Command(OpenUser));

        private void OpenUser()
        {
            NavigationService.NavigateTo("User");
        }

        #endregion

        #region Settings

        private ICommand _openSettingsCommand;

        public ICommand OpenSettingsCommand => GetLazyProperty(ref _openSettingsCommand, () => new Command(OpenSettings));

        private void OpenSettings()
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

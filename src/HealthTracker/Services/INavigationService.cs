using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using HealthTracker.MVVM;
using HealthTracker.ViewModels;

namespace HealthTracker.Services
{
    public interface INavigationService
    {
        event EventHandler<ActiveSectionChangedEventArgs> ActiveSectionChanged;
        SectionViewModel ActiveSection { get; }
        ICollection<SectionViewModel> AllSections { get; }
        ObservableCollection<SectionViewModel> EnabledSections { get; }
        SectionViewModel FindSectionViewModel(SectionMainViewModel sectionMainViewModel);
        void NavigateTo(string name, object parameter);
        void Initialize();
    }
}
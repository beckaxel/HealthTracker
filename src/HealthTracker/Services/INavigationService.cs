using System;
using System.Collections.ObjectModel;
using HealthTracker.ViewModels;

namespace HealthTracker.Services
{
    public interface INavigationService
    {
        event EventHandler<ActiveSectionChangedEventArgs> ActiveSectionChanged;
        SectionViewModel ActiveSection { get; }
        ObservableCollection<SectionViewModel> EnabledSections { get; }
        void NavigateTo(string name, object parameter);
    }
}
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using HealthTracker.ViewModels;

namespace HealthTracker.Services
{
    public interface INavigationService
    {
        event EventHandler<ActiveSectionChangedEventArgs> ActiveSectionChanged;
        SectionViewModel ActiveSection { get; }
        ICollection<SectionViewModel> AllSections { get; }
        ObservableCollection<SectionViewModel> EnabledSections { get; }
        void NavigateTo(string name, object parameter);
    }
}
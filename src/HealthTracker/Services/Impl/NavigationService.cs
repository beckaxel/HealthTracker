using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using HealthTracker.MVVM;
using HealthTracker.ViewModels;

namespace HealthTracker.Services.Impl
{
    //ToDo: NavigationService History hinzufügen
    public class NavigationService : INavigationService
    {
        private readonly INavigationTarget _navigationTarget;
        private readonly IViewService _viewService;
        private readonly IViewModelService _viewModelService;

        public NavigationService
        (
            INavigationTarget navigationTarget,
            IViewService viewService,
            IViewModelService viewModelService
        )
        {
            _navigationTarget = navigationTarget;
            _viewService = viewService;
            _viewModelService = viewModelService;
            _allSections = LoadSections();
        }

        #region Sections

        private readonly Dictionary<string, SectionViewModel> _allSections;
        public ICollection<SectionViewModel> AllSections => _allSections.Values; 

        public ObservableCollection<SectionViewModel> EnabledSections { get; } = new ObservableCollection<SectionViewModel>();

        private Dictionary<string, SectionViewModel> LoadSections()
        {
            var allSections = DetectSections();
            foreach (SectionViewModel section in allSections.Values)
            {
                section.PropertyChanged += Section_PropertyChanged;
                if (!section.Disabled)
                    EnabledSections.Add(section);
            }
            return allSections;
        }

        private Dictionary<string, SectionViewModel> DetectSections()
        {
            var views = _viewService.GetViews<SectionMainView>();
            return views
                .Select(v => new SectionViewModel(_viewService.GetNameOfView(v))
                {
                    DisplayName = v.SectionName,
                    IconImageSource = v.SectionIconImageSource,
                    Collation = v.SectionCollation
                })
                .OrderBy(s => s.Collation)
                .ToDictionary(s => s.Name);
        }

        public event EventHandler<ActiveSectionChangedEventArgs> ActiveSectionChanged;

        private SectionViewModel _activeSection;
        public SectionViewModel ActiveSection
        {
            get => _activeSection;
            private set
            {
                var oldActiveSection = _activeSection;
                _activeSection = value;
                ActiveSectionChanged?.Invoke(this, new ActiveSectionChangedEventArgs(oldActiveSection, value));
            }
        }

        private void Section_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (!(sender is SectionViewModel section))
                return;

            if (e.PropertyName == nameof(SectionViewModel.Disabled))
            {
                if (section.Disabled)
                    EnabledSections.Remove(section);
                else
                    EnabledSections.InsertBefore(s => s.Collation > section.Collation, section);
            }
            else if (e.PropertyName == nameof(SectionViewModel.Active) && section.Active)
            {
                if (ActiveSection != null)
                    ActiveSection.Active = false;
                ActiveSection = section;
            }
        }
        
        #endregion

        #region NavigateTo

        public void NavigateTo(string name, object parameter = null)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));

            var view = _viewService.GetView(name);
            var viewModel = _viewModelService.GetViewModel(name);
            viewModel.Parameter = parameter;
            view.BindingContext = viewModel;

            if (_navigationTarget.CurrentView != null)
                _navigationTarget.CurrentView.BackButtonPressed -= OnBackButtonPressed;
            view.BackButtonPressed += OnBackButtonPressed;
            _navigationTarget.CurrentView = view;

            if (_allSections.ContainsKey(name))
                _allSections[name].Active = true;
        }

        private void OnBackButtonPressed(object sender, BackButtonPressedEventArgs e)
        {
            if (sender is SectionMainView)
                return;

            this.NavigateToActiveSection();
            e.PreventDefault = true;
        }

        #endregion
    }
}

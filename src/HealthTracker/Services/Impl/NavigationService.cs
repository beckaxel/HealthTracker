﻿using System;
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
                .Select(CreateSectionViewModel)
                .OrderBy(s => s.Collation)
                .ToDictionary(s => s.Name);
        }

        private SectionViewModel CreateSectionViewModel(SectionMainView sectionMainView)
        {
            var sectionViewModel = _viewModelService.GetViewModel<SectionViewModel>(_viewService.GetNameOfView(sectionMainView));
            sectionViewModel.DisplayName = sectionMainView.SectionName;
            sectionViewModel.IconImageSource = sectionMainView.SectionIconImageSource;
            sectionViewModel.Collation = sectionMainView.SectionCollation;
            sectionViewModel.Filters.AddRange(sectionMainView.SectionFilters.Select(f => new FilterViewModel
            {
                Parameter = f,
                Default = f.Name == sectionMainView.SectionFilters.DefaultFilterName,
                Active = f.Name == sectionMainView.SectionFilters.DefaultFilterName
            }));
            sectionViewModel.ActiveFilter = sectionViewModel.Filters.FirstOrDefault(f => f.Default);
            return sectionViewModel;
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
                    OnSectionDisabled(section);
                else
                    OnSectionEnabled(section);
            }
            else if (e.PropertyName == nameof(SectionViewModel.Active) && section.Active)
            {
                OnSectionActivated(section);
            }
        }

        private void OnSectionEnabled(SectionViewModel section)
        {
            if (!EnabledSections.InsertBefore(s => s.Collation > section.Collation, section))
                EnabledSections.Append(section);

            if (EnabledSections.Count == 1)
                section.Active = true;
        }

        private void OnSectionDisabled(SectionViewModel section)
        {
            var disabledIndex = EnabledSections.IndexOf(section);
            if (section.Active)
            {
                var activeIndex = Math.Abs(disabledIndex - 1);
                if (activeIndex < EnabledSections.Count)
                {
                    EnabledSections[activeIndex].Active = true;
                }
                else
                {
                    section.Active = false;
                    ActiveSection = null;
                }
            }

            EnabledSections.RemoveAt(disabledIndex);
        }

        private void OnSectionActivated(SectionViewModel section)
        {
            if (ActiveSection != null)
                ActiveSection.Active = false;
            ActiveSection = section;
        }

        public SectionViewModel FindSectionViewModel(SectionMainViewModel sectionMainViewModel)
        {
            var sectionName = _viewModelService.GetNameOfViewModel(sectionMainViewModel);
            return AllSections.FirstOrDefault(s => s.Name == sectionName);
        }

        #endregion

        #region NavigateTo

        public void NavigateTo(string name, object parameter)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));

            var view = _viewService.GetView(name);
            var viewModel = _viewModelService.GetViewModel(name, parameter);
            view.BindingContext = viewModel;

            if (_navigationTarget.CurrentView != null)
                _navigationTarget.CurrentView.BackButtonPressed -= OnBackButtonPressed;
            view.BackButtonPressed += OnBackButtonPressed;

            _navigationTarget.CurrentView = view;

            if (_navigationTarget.CurrentViewModel != null)
                _navigationTarget.CurrentViewModel.Dispose();
            _navigationTarget.CurrentViewModel = viewModel;

            if (_allSections.ContainsKey(name))
                _allSections[name].Active = true;
        }

        private void OnBackButtonPressed(object sender, BackButtonPressedEventArgs e)
        {
            if (sender is SectionMainView || sender is NoEnabledSectionViewModel)
                return;

            this.NavigateToActiveSection();
            e.PreventDefault = true;
        }

        #endregion

        #region Initialize

        public void Initialize()
        {
            var defaultSection = EnabledSections.FirstOrDefault();
            if (defaultSection == null)
                return;

            defaultSection.Active = true;
            this.NavigateToActiveSection();
        }

        #endregion
    }
}

using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using HealthTracker.MVVM;
using HealthTracker.Storage;
using Xamarin.Forms;

namespace HealthTracker.ViewModels
{
    public class SectionViewModel : ViewModelBase
    {
        bool _loading = false;

        private readonly ISettingsStorage _settingsStorage;

        public SectionViewModel(ISettingsStorage settingsStorage)
        {
            _settingsStorage = settingsStorage;
            Filters.CollectionChanged += Filters_CollectionChanged;
        }

        protected override void OnParameterChanged(object oldValue, object newValue)
        {
            Name = newValue as string;
            Load();
        }

        private void Filters_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Move)
                return;

            if (e.OldItems != null)
            {
                foreach (FilterViewModel filterViewModel in e.OldItems)
                    filterViewModel.PropertyChanged -= FilterViewModel_PropertyChanged;
            }

            if (e.NewItems != null)
            {
                foreach (FilterViewModel filterViewModel in e.NewItems)
                    filterViewModel.PropertyChanged += FilterViewModel_PropertyChanged;
            }
        }

        private void FilterViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (!(sender is FilterViewModel filterViewModel))
                return;

            if (e.PropertyName != nameof(FilterViewModel.Active))
                return;

            if (filterViewModel.Active)
            {
                ActiveFilter.Active = false;
                ActiveFilter = filterViewModel;
            }
            else if (Filters.All(f => !f.Active))
            {
                var defaultFilter = Filters.FirstOrDefault(f => f.Default);
                if (defaultFilter != null)
                    defaultFilter.Active = true;
            }
        }

        private void Load()
        {
            _loading = true;

            Disabled = bool.TryParse(_settingsStorage.GetValue($"{Name}_IsDisabled"), out var disabled) && disabled;

            _loading = false;
        }

        private void Save()
        {
            if (_loading)
                return;

            _settingsStorage.SetValue($"{Name}_IsDisabled", Disabled.ToString());
        }

        #region Name

        public string Name { get; private set; }

        #endregion

        #region DisplayName

        private string _displayName;

        public string DisplayName
        {
            get => _displayName;
            set => SetProperty(ref _displayName, value);
        }

        #endregion

        #region IconImageSource

        private ImageSource _iconImageSource;

        public ImageSource IconImageSource
        {
            get => _iconImageSource;
            set => SetProperty(ref _iconImageSource, value);
        }

        #endregion

        #region Collation

        private int _collation;

        public int Collation
        {
            get => _collation;
            set => SetProperty(ref _collation, value);
        }

        #endregion

        #region Filters

        public ObservableCollection<FilterViewModel> Filters { get; } = new ObservableCollection<FilterViewModel>();

        #endregion

        #region ActiveFilter

        private FilterViewModel _activeFilter;

        public FilterViewModel ActiveFilter
        {
            get => _activeFilter;
            set => SetProperty(ref _activeFilter, value);
        }

        #endregion

        #region Active

        private bool _active;

        public bool Active
        {
            get => _active;
            set => SetProperty(ref _active, value);
        }

        #endregion

        #region Disabled

        private bool _disabled;
        
        public bool Disabled
        {
            get => _disabled;
            set
            {
                SetProperty(ref _disabled, value);
                Save();
            }
        }

        #endregion
    }
}

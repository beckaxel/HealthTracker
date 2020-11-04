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
        }

        protected override void OnParameterChanged(object oldValue, object newValue)
        {
            Name = newValue as string;
            Load();
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

using HealthTracker.MVVM;
using Xamarin.Forms;

namespace HealthTracker.ViewModels
{
    public class SectionViewModel : ViewModelBase
    {
        public SectionViewModel(string name)
        {
            Name = name;
        }

        #region Name

        public string Name { get; }
       
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
            set => SetProperty(ref _disabled, value);
        }

        #endregion
    }
}

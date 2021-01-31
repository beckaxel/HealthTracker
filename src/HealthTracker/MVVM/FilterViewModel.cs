namespace HealthTracker.MVVM
{
    public class FilterViewModel : ViewModelBase
    {
        protected override void OnParameterChanged(object oldValue, object newValue)
        {
            var filter = newValue as Filter;
            if (filter?.Name != null)
                DisplayName = filter.Name;
        }

        private string _displayName;
        public string DisplayName
        {
            get => _displayName;
            set => SetProperty(ref _displayName, value);
        }

        private bool _active;
        public bool Active
        {
            get => _active;
            set => SetProperty(ref _active, value);
        }

        private bool _default;
        public bool Default
        {
            get => _default;
            set => SetProperty(ref _default, value);
        }
    }
}

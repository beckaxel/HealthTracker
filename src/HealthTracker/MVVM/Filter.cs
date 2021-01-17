using Xamarin.Forms;

namespace HealthTracker.MVVM
{
    [ContentProperty(nameof(Name))]
    public class Filter : BindableObject
    {   
        public string Name { get; set; }

        private bool _isActive;
        public bool IsActive
        {
            get => _isActive;
            set
            {
                if (_isActive == value)
                    return;
                _isActive = value;
                OnPropertyChanged(nameof(IsActive));
            }
        }
        
    }
}
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using HealthTracker.Common;
using HealthTracker.Models;
using HealthTracker.MVVM;

namespace HealthTracker.ViewModels
{
    public class MealViewModel : ViewModelBase
    {   
        protected Meal Meal { get; private set; }

        public MealViewModel()
        {
            Photos.CollectionChanged += PhotosChanged;
        }

        protected override void Dispose(bool disposing)
        {
            Photos.CollectionChanged -= PhotosChanged;
            base.Dispose(disposing);
        }

        #region Parameter Handling

        protected override void OnParameterChanged(object oldValue, object newValue)
        {
            if (newValue == MVVM.Parameter.Empty)
            {
                Meal = new Meal
                {
                    EatingTime = DateTime.UtcNow
                };
                MapFrom(Meal);
            }
            else if (newValue is Meal meal)
            {
                Meal = meal;
                MapFrom(Meal);
            }
        }

        #endregion

        #region Id

        private int? _mealId;

        public int? MealId
        {
            get => _mealId;
            set => SetProperty(ref _mealId, value);
        }

        #endregion

        #region Name

        private string _name;

        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        #endregion

        #region Date and Time

        private DateAndTime _eatingTime;

        public DateTime EatingTime
        {
            get => _eatingTime.UtcDateTime;
            set
            {
                _eatingTime.UtcDateTime = value;
                OnPropertyChanged(nameof(DateOfEatingTime));
                OnPropertyChanged(nameof(TimeOfEatingTime));
            }
        }

        public DateTime DateOfEatingTime
        {
            get => _eatingTime.LocalDate;
            set
            {
                _eatingTime.LocalDate = value;
                OnPropertyChanged(nameof(DateOfEatingTime));
            }
        }

        public TimeSpan TimeOfEatingTime
        {
            get => _eatingTime.LocalTime;
            set
            {
                _eatingTime.LocalTime = value;
                OnPropertyChanged(nameof(TimeOfEatingTime));
            }
        }

        #endregion

        #region Photos

        public bool HasPhotos => Photos.Count > 0;

        public PhotoViewModel LatestPhoto => Photos.OrderByDescending(p => p.RecordingTime).FirstOrDefault();

        public ObservableCollection<PhotoViewModel> Photos { get; } = new ObservableCollection<PhotoViewModel>();

        private void PhotosChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(HasPhotos));
            OnPropertyChanged(nameof(LatestPhoto));
        }

        #endregion

        #region MealType

        private MealType _mealType;

        public MealType MealType
        {
            get => _mealType;
            set => SetProperty(ref _mealType, value);
        }

        #endregion

        #region Diet

        private Diet _diet;

        public Diet Diet
        {
            get => _diet;
            set => SetProperty(ref _diet, value);
        }

        #endregion
    }
}

using System.Windows.Input;
using HealthTracker.MVVM;
using HealthTracker.Services;
using HealthTracker.Storage;
using Xamarin.Forms;

namespace HealthTracker.ViewModels
{
    public class FoodSectionViewModel : SectionMainViewModel
    {
        private readonly HealthTrackerDbContext _healthTrackerDbContext;

        public FoodSectionViewModel
        (
            INavigationService navigationService,
            IDbContextFactory dbContextFactory
        )
            : base(navigationService)
        {
            _healthTrackerDbContext = dbContextFactory.CreateHealthTrackerDbContext();
            var lastMeal = _healthTrackerDbContext.Meal.LatestOrDefault();

            LastMealName = lastMeal?.Name ?? string.Empty;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        #region LastMeal

        private string _lastMealName;

        public string LastMealName
        {
            get => _lastMealName;
            set => SetProperty(ref _lastMealName, value);
        }

        #endregion

        #region AddMeal

        private ICommand _addMealCommand;

        public ICommand AddMealCommand => GetLazyProperty(ref _addMealCommand, () => new Command(AddMeal));

        public void AddMeal()
        {
            NavigationService.NavigateTo("EditMeal");
        }

        #endregion
    }
}

using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using HealthTracker.MVVM;
using HealthTracker.Services;
using HealthTracker.Storage;
using Xamarin.Forms;

namespace HealthTracker.ViewModels
{
    public class DrinkingSectionViewModel : SectionMainViewModel
    {  
        private readonly HealthTrackerDbContext _healthTrackerDbContext;

        public DrinkingSectionViewModel
        (
            INavigationService navigationService,                     
            IDbContextFactory dbContextFactory
        )
            : base(navigationService)
        {            
            _healthTrackerDbContext = dbContextFactory.CreateHealthTrackerDbContext();
            DrinkingQuantitiesPerDay.AddRange(_healthTrackerDbContext.Beverage
                .LastXDays(14)
                .SelectIntoDrinkingQuantityPerDayViewModel(_healthTrackerDbContext));

            BeveragesToday.AddRange(_healthTrackerDbContext.Beverage
                .Today()
                .Select(b => new BeverageViewModel { Parameter = b }));

            DrinkingQuantityToday = _healthTrackerDbContext.Beverage
                .Today()
                .SelectIntoDrinkingQuantityPerDayViewModel(_healthTrackerDbContext)
                .FirstOrDefault();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        public ObservableCollection<DrinkingQuantityPerDayViewModel> DrinkingQuantitiesPerDay { get; } = new ObservableCollection<DrinkingQuantityPerDayViewModel>();

        public ObservableCollection<BeverageViewModel> BeveragesToday { get; } = new ObservableCollection<BeverageViewModel>();

        private DrinkingQuantityPerDayViewModel _drinkingQuantityToday;

        public DrinkingQuantityPerDayViewModel DrinkingQuantityToday
        {
            get => _drinkingQuantityToday;
            set => SetProperty(ref _drinkingQuantityToday, value);
        }

        #region AddBeverage

        private ICommand _addBeverageCommand;

        public ICommand AddBeverageCommand => GetLazyProperty(ref _addBeverageCommand, () => new Command(AddBeverage));

        public void AddBeverage()
        {
            NavigationService.NavigateTo("EditBeverage");
        }

        #endregion

        #region OpenBeverageCommand

        private ICommand _openBeverageCommand;

        public ICommand OpenBeverageCommand => GetLazyProperty(ref _openBeverageCommand, () => new Command(id => OpenBeverage((int)id)));

        public void OpenBeverage(int beverageId)
        {
            var beverage = _healthTrackerDbContext
                .Beverage
                .FirstOrDefault(b => b.BeverageId == beverageId);

            if (beverage == null)
                return;

            NavigationService.NavigateTo("EditBeverage", beverage);
        }

        #endregion
    }
}

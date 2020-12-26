using System;
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
        private const float DailyAmount = 2000f;
        private readonly HealthTrackerDbContext _healthTrackerDbContext;

        public DrinkingSectionViewModel
        (
            INavigationService navigationService,                     
            IDbContextFactory dbContextFactory
        )
            : base(navigationService)
        {            
            _healthTrackerDbContext = dbContextFactory.CreateHealthTrackerDbContext();
            AmountToday = _healthTrackerDbContext.Beverage.AmountToday();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        private float _amountToday;
        public float AmountToday
        {
            get => _amountToday;
            set
            {
                SetProperty(ref _amountToday, value);
                OnPropertyChanged(nameof(PercentToday));
            }
        }

        public float PercentToday => AmountToday / DailyAmount;

        #region AddBeverage

        private ICommand _addBeverageCommand;

        public ICommand AddBeverageCommand => GetLazyProperty(ref _addBeverageCommand, () => new Command(AddBeverage));

        public void AddBeverage()
        {
            NavigationService.NavigateTo("EditBeverage");
        }

        #endregion
    }
}

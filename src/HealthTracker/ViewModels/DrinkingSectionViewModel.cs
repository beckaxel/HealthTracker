using System.Windows.Input;
using HealthTracker.MVVM;
using HealthTracker.Services;
using HealthTracker.Storage;
using Xamarin.Forms;

namespace HealthTracker.ViewModels
{
    public class DrinkingSectionViewModel : SectionMainViewModel
    {        
        private readonly IDrinkingStorage _drinkingStorage;

        public DrinkingSectionViewModel
        (
            INavigationService navigationService,                     
            IDrinkingStorage drinkingStorage
        )
            : base(navigationService)
        {            
            _drinkingStorage = drinkingStorage;
            AmountToday = _drinkingStorage.AmountToday();
        }

        private float _amountToday;
        public float AmountToday
        {
            get => _amountToday;
            set => SetProperty(ref _amountToday, value);
        }

        #region AddDrinking

        private ICommand _addDrinkingCommand;

        public ICommand AddDrinkingCommand => GetLazyProperty(ref _addDrinkingCommand, () => new Command(AddDrinking));

        public void AddDrinking()
        {
            NavigationService.NavigateTo("EditDrinking");
        }

        #endregion
    }
}

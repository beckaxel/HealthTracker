﻿using System;
using System.Linq;
using System.Windows.Input;
using HealthTracker.Services;
using HealthTracker.Storage;
using Xamarin.Forms;

namespace HealthTracker.ViewModels
{
    public class EditBeverageViewModel : BeverageViewModel
    {
        private const float StepSize = 50.0f;

        public EditBeverageViewModel
        (
            INavigationService navigationService,
            IDbContextFactory dbContextFactory
        )
            : base(navigationService, dbContextFactory) { }

        #region Parameter Handling

        protected override void OnParameterChanged(object oldValue, object newValue)
        {
            base.OnParameterChanged(oldValue, newValue);
            if (newValue == MVVM.Parameter.Empty)
            {
                var latest = HealthTrackerDbContext.Beverage.LatestOrDefault();
                if (latest == null)
                    return;

                Beverage.DrinkingTime = DateTime.UtcNow;
                Beverage.Amount = latest.Amount;

                MapFrom(Beverage);
            }
        }

        #endregion

        #region IncreaseAmount

        private ICommand _increaseWeightCommand;

        public ICommand IncreaseAmountCommand => GetLazyProperty(ref _increaseWeightCommand, () => new Command(IncreaseWeight));

        public void IncreaseWeight()
        {
            Amount += StepSize;
            OnPropertyChanged(nameof(Amount));
        }

        #endregion

        #region DecreaseAmount

        private ICommand _decreaseAmountCommand;

        public ICommand DecreaseAmountCommand => GetLazyProperty(ref _decreaseAmountCommand, () => new Command(DecreaseWeight));

        public void DecreaseWeight()
        {
            Amount -= StepSize;
        }

        #endregion

        #region Cancel

        private ICommand _cancelCommand;

        public ICommand CancelCommand => GetLazyProperty(ref _cancelCommand, () => new Command(Cancel));

        public void Cancel()
        {
            NavigationService.NavigateToActiveSection();
        }

        #endregion

        #region Save

        private ICommand _saveCommand;

        public ICommand SaveCommand => GetLazyProperty(ref _saveCommand, () => new Command(Save));

        public void Save()
        {
            SaveChanges();
            NavigationService.NavigateToActiveSection();
        }

        #endregion
    }
}

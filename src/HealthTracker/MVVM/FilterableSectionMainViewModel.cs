using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using HealthTracker.Services;
using HealthTracker.Storage;
using Xamarin.Forms;

namespace HealthTracker.MVVM
{
    public abstract class FilterableSectionMainViewModel<TEntity> : SectionMainViewModel
        where TEntity : class
    {
        protected HealthTrackerDbContext HealthTrackerDbContext { get; }

        protected FilterableSectionMainViewModel
        (
            IDbContextFactory dbContextFactory,
            INavigationService navigationService
        ) : base(navigationService)
        {
            HealthTrackerDbContext = dbContextFactory.CreateHealthTrackerDbContext();
            ApplyActiveFilterAsync();
        }

        protected override void Dispose(bool disposing)
        {
            HealthTrackerDbContext.Dispose();
        }

        private async void ApplyActiveFilterAsync()
        {
            try
            {
                var activeFilterName = NavigationService.FindSectionViewModel(this)?.ActiveFilter?.DisplayName;
                if (activeFilterName != null)
                    await FilterContentAsync(activeFilterName).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                Debug.Write(ex.Message);
            }
        }

        #region IsLoading

        private bool _isLoading;
        public bool IsLoading
        {
            get => _isLoading;
            set => SetProperty(ref _isLoading, value);
        }

        #endregion

        #region HasListData

        private bool _hasListData;
        public bool HasListData
        {
            get => _hasListData;
            set => SetProperty(ref _hasListData, value);
        }

        #endregion

        #region HasSummaryData

        private bool _hasSummaryData;
        public bool HasSummaryData
        {
            get => _hasSummaryData;
            set => SetProperty(ref _hasSummaryData, value);
        }

        #endregion

        #region FilterContent

        private ICommand _filterContentCommand;

        public ICommand FilterContentCommand => GetLazyProperty(ref _filterContentCommand, () => new Command(async p => await ActivateFilterAsync(p as FilterViewModel)));

        private async Task ActivateFilterAsync(FilterViewModel filterViewModel)
        {
            if (filterViewModel == null)
                return;

            filterViewModel.Active = true;
            await FilterContentAsync(filterViewModel.DisplayName);
        }

        private async Task FilterContentAsync(string filterName)
        {
            IsLoading = true;

            var filteredItems = await GetFilteredItemsAsync(HealthTrackerDbContext.Set<TEntity>(), filterName);
            var updateResults = await Task.WhenAll
            (
                UpdateListAsync(filteredItems, filterName),
                UpdateSummaryAsync(filteredItems, filterName)
            );

            HasListData = updateResults[0];
            HasSummaryData = updateResults[1];

            IsLoading = false;
        }

        protected abstract Task<List<TEntity>> GetFilteredItemsAsync(IQueryable<TEntity> query, string filterName);

        protected abstract Task<bool> UpdateListAsync(List<TEntity> filteredItems, string filterName);

        protected abstract Task<bool> UpdateSummaryAsync(List<TEntity> filteredItems, string filterName);

        #endregion

        #region AddItem

        #region AddItem

        private ICommand _addItemCommand;

        public ICommand AddItemCommand => GetLazyProperty(ref _addItemCommand, () => new Command(AddItem));

        protected abstract void AddItem();

        #endregion

        #endregion
    }
}

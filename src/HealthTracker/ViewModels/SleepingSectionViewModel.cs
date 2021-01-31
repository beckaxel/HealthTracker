using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthTracker.Models;
using HealthTracker.MVVM;
using HealthTracker.Services;
using HealthTracker.Storage;

namespace HealthTracker.ViewModels
{
    public class SleepingSectionViewModel : FilterableSectionMainViewModel<SleepLog>
    {
        public SleepingSectionViewModel(
            IDbContextFactory dbContextFactory,
            INavigationService navigationService)
            : base(dbContextFactory, navigationService)
        {
        }

        #region FilterContent

        protected override Task<List<SleepLog>> GetFilteredItemsAsync(IQueryable<SleepLog> query, string filterName)
        {
            return Task.Run(() => Enumerable.Empty<SleepLog>().ToList());
        }

        protected override Task<bool> UpdateListAsync(List<SleepLog> filteredItems, string filterName)
        {
            return Task.FromResult(false);
        }

        protected override Task<bool> UpdateSummaryAsync(List<SleepLog> filteredItems, string filterName)
        {
            return Task.FromResult(false);
        }

        #endregion

        #region AddItem

        protected override void AddItem()
        {

        }

        #endregion
    }
}

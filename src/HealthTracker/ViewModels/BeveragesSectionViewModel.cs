using HealthTracker.MVVM;
using HealthTracker.Services;
using HealthTracker.Storage;

namespace HealthTracker.ViewModels
{
    public class BeveragesSectionViewModel : SectionMainViewModel
    {
        private readonly IBeverageTypeStorage _beverageTypeStorage;
        private readonly IBeverageStorage _beverageStorage;

        public BeveragesSectionViewModel
        (
            INavigationService navigationService,
            IBeverageTypeStorage beverageTypeStorage,
            IBeverageStorage beverageStorage
        )
            : base(navigationService)
        {
            _beverageTypeStorage = beverageTypeStorage;
            _beverageStorage = beverageStorage;

            var types = _beverageTypeStorage.All();
            var bs = _beverageStorage.All();
        }
    }
}

using HealthTracker.MVVM;

namespace HealthTracker.Services
{
    public static class ViewModelServiceExtensions
    {
        public static ViewModelBase GetViewModel(this IViewModelService viewModelService, string name, object parameter)
        {
            var viewModel = viewModelService.GetViewModel(name);
            viewModel.Parameter = parameter;
            return viewModel;
        }

        public static TViewModel GetViewModel<TViewModel>(this IViewModelService viewModelService)
            where TViewModel : ViewModelBase
        {
            var viewModelTypeName = typeof(TViewModel).Name;
            var viewModelName = viewModelTypeName.Substring(0, viewModelTypeName.Length - viewModelService.ViewModelNameSuffix.Length);
            return (TViewModel)viewModelService.GetViewModel(viewModelName);
        }

        public static TViewModel GetViewModel<TViewModel>(this IViewModelService viewModelService, object parameter)
            where TViewModel : ViewModelBase
        {
            var viewModel = GetViewModel<TViewModel>(viewModelService);
            viewModel.Parameter = parameter;
            return viewModel;
        }
    }
}

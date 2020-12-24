using System;
using System.Collections.Generic;
using System.Linq;
using HealthTracker.MVVM;

namespace HealthTracker.Services.Impl
{
    public class ViewModelService : IViewModelService
    {
        public string ViewModelNameSuffix { get; } = "ViewModel";

        private readonly IServiceLocator _serviceLocator;
        private readonly Dictionary<string, Type> _viewModelTypes;

        public ViewModelService(ITypeResolver typeResolver, IServiceLocator serviceLocator)
        {
            _serviceLocator = serviceLocator;
            _viewModelTypes = typeResolver.ResolveTypes<ViewModelBase>
                (
                    "HealthTracker.MVVM",
                    suffix: ViewModelNameSuffix,
                    options: TypeResolvingOptions.IgnoreCase | TypeResolvingOptions.ClassesOnly
                )
                .Concat(typeResolver.ResolveTypes<ViewModelBase>
                (
                    "HealthTracker.ViewModels",
                    suffix: ViewModelNameSuffix,
                    options: TypeResolvingOptions.IgnoreCase | TypeResolvingOptions.ClassesOnly
                ))
                .ToDictionary(t => t.Name.Substring(0, t.Name.Length - ViewModelNameSuffix.Length), StringComparer.InvariantCultureIgnoreCase);

            foreach (var viewModelType in _viewModelTypes.Values)
                _serviceLocator.Register(viewModelType);
        }

        public ViewModelBase GetViewModel(string name)
        {
            if (!_viewModelTypes.ContainsKey(name))
                throw new ViewModelNotFoundException($"Ein ViewModel mit dem Namen {name} konnte nicht gefunden werden");

            var type = _viewModelTypes[name];
            ViewModelBase viewModel;
            try
            {
                viewModel = GetInstance<ViewModelBase>(type);
            }
            catch (Exception ex)
            {
                throw new ViewModelNotFoundException($"Ein ViewModel mit dem Namen {name} konnte nicht gefunden werden", ex);
            }

            if (viewModel == null)
                throw new ViewModelNotFoundException($"Ein ViewModel mit dem Namen {name} konnte nicht gefunden werden");

            return viewModel;
        }

        private T GetInstance<T>(Type type)
            where T : class
        {
            return type != null
                ? _serviceLocator.Resolve(type) as T
                : null;
        }
    }
}

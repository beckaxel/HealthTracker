using System;
using System.Collections.Generic;
using System.Linq;
using HealthTracker.MVVM;
using Xamarin.Forms;

namespace HealthTracker.Services.Impl
{
    public class ViewService : IViewService
    {
        public string ViewNameSuffix { get; } = "View";

        private readonly IServiceLocator _serviceLocator;
        private readonly Dictionary<string, Type> _viewTypes;

        public ViewService(ITypeResolver typeResolver, IServiceLocator serviceLocator)
        {
            _serviceLocator = serviceLocator;
            _viewTypes = typeResolver.ResolveTypes<ViewBase>
                (
                    "HealthTracker.Views",
                    suffix: ViewNameSuffix,
                    options: TypeResolvingOptions.IgnoreCase | TypeResolvingOptions.ClassesOnly
                )
                .ToDictionary(t => t.Name.Substring(0, t.Name.Length - ViewNameSuffix.Length), StringComparer.InvariantCultureIgnoreCase);

            foreach (var viewType in _viewTypes.Values)
                _serviceLocator.RegisterSingleton(viewType);
        }

        public string GetNameOfView(ViewBase view)
        {
            return GetNameOfView(view.GetType());
        }

        public string GetNameOfView(Type viewType)
        {
            var name = _viewTypes
                .Where(kvp => kvp.Value == viewType)
                .Select(kvp => kvp.Key)
                .FirstOrDefault();

            if (name == null)
                throw new ViewNotFoundException($"Der Typ {viewType.FullName} gehört zu keiner View");

            return name;
        }

        public ViewBase GetView(string name)
        {
            if (!_viewTypes.ContainsKey(name))
                throw new ViewNotFoundException($"Eine View mit dem Namen {name} konnte nicht gefunden werden");

            var type = _viewTypes[name];
            ViewBase view;
            try
            {
                view = GetInstance<ViewBase>(type);
            }
            catch (Exception ex)
            {
                throw new ViewNotFoundException($"Eine View mit dem Namen {name} konnte nicht gefunden werden", ex);
            }
             
            if (view == null)
                throw new ViewNotFoundException($"Eine View mit dem Namen {name} konnte nicht gefunden werden");

            return view;
        }

        public IEnumerable<TBaseType> GetViews<TBaseType>()
            where TBaseType : ViewBase
        {
            return _viewTypes.Values
                .Where(t => t.IsSubclassOf(typeof(TBaseType)))
                .Select(GetInstance<TBaseType>);
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

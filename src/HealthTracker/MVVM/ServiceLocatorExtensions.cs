using System;

namespace HealthTracker.MVVM
{
    public static class ServiceLocatorExtensions
    {
        #region Register

        public static void Register<T>(this IServiceLocator serviceLocator)
            where T : class
        {
            serviceLocator.Register(typeof(T));
        }

        public static void Register<T>(this IServiceLocator serviceLocator, Func<IServiceLocator, T> factory)
            where T : class
        {
            serviceLocator.Register(typeof(T), factory);
        }

        public static void Register<T>(this IServiceLocator serviceLocator, string name)
            where T : class
        {
            serviceLocator.Register(typeof(T), name);
        }

        public static void Register<T>(this IServiceLocator serviceLocator, Func<IServiceLocator, T> factory, string name)
            where T : class
        {
            serviceLocator.Register(typeof(T), factory, name);
        }

        public static void Register<TInterface, TImplementation>(this IServiceLocator serviceLocator)
            where TInterface : class
            where TImplementation : class, TInterface
        {
            serviceLocator.Register(typeof(TInterface), typeof(TImplementation));
        }

        public static void Register<TInterface, TImplementation>(this IServiceLocator serviceLocator, string name)
            where TInterface : class
            where TImplementation : class, TInterface
        {
            serviceLocator.Register(typeof(TInterface), typeof(TImplementation), name);
        }

        #endregion

        #region Register Singleton

        public static void RegisterSingleton<T>(this IServiceLocator serviceLocator)
            where T : class
        {
            serviceLocator.RegisterSingleton(typeof(T));
        }

        public static void RegisterSingleton<T>(this IServiceLocator serviceLocator, T instance)
            where T : class
        {
            serviceLocator.RegisterSingleton(typeof(T), instance.GetType(), instance);
        }

        public static void RegisterSingleton<T>(this IServiceLocator serviceLocator, Func<IServiceLocator, T> factory)
            where T : class
        {
            serviceLocator.RegisterSingleton(typeof(T), factory);
        }

        public static void RegisterSingleton<T>(this IServiceLocator serviceLocator, string name)
            where T : class
        {
            serviceLocator.RegisterSingleton(typeof(T), name);
        }

        public static void RegisterSingleton<T>(this IServiceLocator serviceLocator, T instance, string name)
            where T : class
        {
            serviceLocator.RegisterSingleton(typeof(T), instance.GetType(), instance, name);
        }

        public static void RegisterSingleton<T>(this IServiceLocator serviceLocator, Func<IServiceLocator, T> factory, string name)
            where T : class
        {
            serviceLocator.RegisterSingleton(typeof(T), factory, name);
        }

        public static void RegisterSingleton<TInterface, TImplementation>(this IServiceLocator serviceLocator)
            where TInterface : class
            where TImplementation : class, TInterface
        {
            serviceLocator.RegisterSingleton(typeof(TInterface), typeof(TImplementation));
        }

        public static void RegisterSingleton<TInterface, TImplementation>(this IServiceLocator serviceLocator, TImplementation instance)
            where TInterface : class
            where TImplementation : class, TInterface
        {
            serviceLocator.RegisterSingleton(typeof(TInterface), typeof(TImplementation), instance);
        }

        public static void RegisterSingleton<TInterface, TImplementation>(this IServiceLocator serviceLocator, string name)
            where TInterface : class
            where TImplementation : class, TInterface
        {
            serviceLocator.RegisterSingleton(typeof(TInterface), typeof(TImplementation), name);
        }

        public static void RegisterSingleton<TInterface, TImplementation>(this IServiceLocator serviceLocator, TImplementation instance, string name)
            where TInterface : class
            where TImplementation : class, TInterface
        {
            serviceLocator.RegisterSingleton(typeof(TInterface), typeof(TImplementation), instance, name);
        }

        #endregion

        #region Resolve

        public static T Resolve<T>(this IServiceLocator serviceLocator)
            where T : class
        {
            return (T)serviceLocator.Resolve(typeof(T));
        }

        public static T Resolve<T>(this IServiceLocator serviceLocator, string name)
            where T : class
        {
            return (T)serviceLocator.Resolve(typeof(T), name);
        }

        #endregion
    }
}

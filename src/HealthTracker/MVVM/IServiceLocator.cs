using System;

namespace HealthTracker.MVVM
{
    public interface IServiceLocator
    {
        #region Register

        void Register(Type type);
        void Register(Type type, string name);
        void Register(Type type, Func<IServiceLocator, object> factory);
        void Register(Type type, Func<IServiceLocator, object> factory, string name);
        void Register(Type interfaceType, Type implementationType);
        void Register(Type interfaceType, Type implementationType, string name);

        #endregion

        #region Register Singleton

        void RegisterSingleton(Type type);
        void RegisterSingleton(Type type, object instance);
        void RegisterSingleton(Type type, Func<IServiceLocator, object> factory);
        void RegisterSingleton(Type type, string name);
        void RegisterSingleton(Type type, object instance, string name);
        void RegisterSingleton(Type type, Func<IServiceLocator, object> factory, string name);
        void RegisterSingleton(Type interfaceType, Type implementationType);
        void RegisterSingleton(Type interfaceType, Type implementationType, object instance);
        void RegisterSingleton(Type interfaceType, Type implementationType, string name);
        void RegisterSingleton(Type interfaceType, Type implementationType, object instance, string name);

        #endregion

        #region Resolve

        object Resolve(Type type);
        object Resolve(Type type, string name);

        #endregion
    }
}

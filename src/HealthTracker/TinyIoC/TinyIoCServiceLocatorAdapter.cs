using System;
using HealthTracker.MVVM;
using TinyIoC;

namespace HealthTracker.TinyIoC
{
    public class TinyIoCServiceLocatorAdapter : IServiceLocator
    {
        private readonly TinyIoCContainer _container;

        public TinyIoCServiceLocatorAdapter(TinyIoCContainer container)
        {
            _container = container;
        }

        #region Register

        public void Register(Type type)
        {
            _container.Register(type);
        }

        public void Register(Type type, Func<IServiceLocator, object> factory)
        {
            _container.Register(type, (c, p) => factory(this));
        }

        public void Register(Type type, string name)
        {
            _container.Register(type, name);
        }

        public void Register(Type type, Func<IServiceLocator, object> factory, string name)
        {
            _container.Register(type, (c, p) => factory(this), name);
        }

        public void Register(Type interfaceType, Type implementationType)
        {
            _container.Register(interfaceType, implementationType);
        }

        public void Register(Type interfaceType, Type implementationType, string name)
        {
            _container.Register(interfaceType, implementationType, name);
        }

        #endregion

        #region Register Singleton

        public void RegisterSingleton(Type type)
        {
            _container.Register(type).AsSingleton();
        }

        public void RegisterSingleton(Type type, object instance)
        {
            _container.Register(type, instance.GetType(), instance);
        }

        public void RegisterSingleton(Type type, Func<IServiceLocator, object> factory)
        {
            _container.Register(type, (c, p) => factory(this)).AsSingleton();
        }

        public void RegisterSingleton(Type type, string name)
        {
            _container.Register(type, name).AsSingleton();
        }

        public void RegisterSingleton(Type type, object instance, string name)
        {
            _container.Register(type, instance.GetType(), instance, name);
        }

        public void RegisterSingleton(Type type, Func<IServiceLocator, object> factory, string name)
        {
            _container.Register(type, (c, p) => factory(this), name).AsSingleton();
        }

        public void RegisterSingleton(Type interfaceType, Type implementationType)
        {
            _container.Register(interfaceType, implementationType).AsSingleton();
        }

        public void RegisterSingleton(Type interfaceType, Type implementationType, object instance)
        {
            _container.Register(interfaceType, implementationType, instance);
        }

        public void RegisterSingleton(Type interfaceType, Type implementationType, string name)
        {
            _container.Register(interfaceType, implementationType, name).AsSingleton();
        }

        public void RegisterSingleton(Type interfaceType, Type implementationType, object instance, string name)
        {
            _container.Register(interfaceType, implementationType, instance, name);
        }

        #endregion

        #region Resolve

        public object Resolve(Type type)
        {
            return _container.Resolve(type);
        }

        public object Resolve(Type type, string name)
        {
            return _container.Resolve(type);
        }

        #endregion
    }
}

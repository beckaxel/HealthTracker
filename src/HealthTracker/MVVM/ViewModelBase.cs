using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace HealthTracker.MVVM
{
    public abstract class ViewModelBase : BindableObject, IDisposable
    {
        #region Parameter

        private object _parameter;
        public object Parameter
        {
            get => _parameter;
            set
            {
                if (Equals(_parameter, value))
                    return;
                var oldParameter = _parameter;
                _parameter = value;
                OnParameterChanged(oldParameter, _parameter);
            }
        }

        protected virtual void OnParameterChanged(object oldValue, object newValue) { }

        #endregion

        #region PropertyHelper

        protected virtual bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(storage, value))
                return false;

            storage = value;
            OnPropertyChanged(propertyName);

            return true;
        }

        protected virtual T GetLazyProperty<T>(ref T storage, Func<T> factory)
            where T : class
        {
            if (storage == null)
                storage = factory();
            return storage;
        }

        #endregion

        #region AutoMapping

        protected void MapFrom<T>(T source)
        {
            Map(source, this);
        }

        protected void MapTo<T>(T target)
            where T : new()
        {
            Map(this, target);
        }

        private static void Map(object source, object target)
        {
            var targetProperties = target.GetType()
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Where(p => p.CanWrite);

            var sourceProperties = source.GetType()
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Where(p => p.CanRead)
                .ToDictionary(p => p.Name, StringComparer.InvariantCultureIgnoreCase);

            foreach (var targetProperty in targetProperties)
            {
                if (!sourceProperties.ContainsKey(targetProperty.Name))
                    continue;

                var sourceProperty = sourceProperties[targetProperty.Name];
                if (!targetProperty.PropertyType.IsAssignableFrom(sourceProperty.PropertyType))
                    continue;

                targetProperty.SetValue(target, sourceProperty.GetValue(source));
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing) { }

        #endregion
    }
}

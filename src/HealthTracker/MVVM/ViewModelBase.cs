using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace HealthTracker.MVVM
{
    public abstract class ViewModelBase : BindableObject
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
    }
}

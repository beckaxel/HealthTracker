using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace HealthTracker.MVVM
{
    public class FilterCollection : IList
    {
        private readonly IList _list = new List<Filter>();

        public string DefaultFilterName { get; set; }

        protected int ActiveFiltersCount => _list.Cast<Filter>().Count(f => f.IsActive);

        protected Filter DefaultFilter => _list.Cast<Filter>().FirstOrDefault(f => f.Name == DefaultFilterName);
        
        public object this[int index]
        {
            get => _list[index];
            set => _list[index] = value;
        }

        public bool IsFixedSize => _list.IsFixedSize;

        public bool IsReadOnly => _list.IsReadOnly;

        public int Count => _list.Count;

        public bool IsSynchronized => _list.IsSynchronized;

        public object SyncRoot => _list.SyncRoot;

        public int Add(object value)
        {
            if (value is Filter filter)
            {
                filter.IsActive = filter.Name == DefaultFilterName;
                filter.PropertyChanged += Filter_PropertyChanged;
            }

            return _list.Add(value);
        }

        private void Filter_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName != nameof(Filter.IsActive))
                return;

            if (!(sender is Filter filter))
                return;

            if (filter.IsActive)
            {
                foreach (Filter current in _list)
                    if (current != filter)
                        current.IsActive = false;
            }
            else if (ActiveFiltersCount == 0)
            {
                var defaultFilter = DefaultFilter;
                if (defaultFilter != null)
                    defaultFilter.IsActive = true;
            }
        }

        public void Clear()
        {
            _list.Clear();
        }

        public bool Contains(object value)
        {
            return _list.Contains(value);
        }

        public void CopyTo(Array array, int index)
        {
            _list.CopyTo(array, index);
        }

        public IEnumerator GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        public int IndexOf(object value)
        {
            return _list.IndexOf(value);
        }

        public void Insert(int index, object value)
        {
            _list.Insert(index, value);
        }

        public void Remove(object value)
        {
            _list.Remove(value);
        }

        public void RemoveAt(int index)
        {
            RemoveAt(index);
        }
    }
}

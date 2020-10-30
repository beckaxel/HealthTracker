using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace HealthTracker.MVVM
{
    public static class ObservableCollectionExtensions
    {
        public static void AddRange<T>(this ObservableCollection<T> source, IEnumerable<T> values)
        {
            foreach (var value in values)
                source.Add(value);
        }

        public static void Prepend<T>(this ObservableCollection<T> source, T item)
        {
            source.Insert(0, item);
        }

        public static void Append<T>(this ObservableCollection<T> source, T item)
        {
            source.Add(item);
        }

        public static void InsertBefore<T>(this ObservableCollection<T> source, Func<T, bool> predicate, T item)
        {
            for (var i = 0; i < source.Count; i++)
            {
                if (predicate(source[i]))
                {
                    source.Insert(i, item);
                    break;
                }
            }
        }

        public static void InsertBefore<T>(this ObservableCollection<T> source, T itemToFind, T itemToInsert)
        {
            InsertBefore(source, i => ReferenceEquals(i, itemToFind), itemToInsert);
        }

        public static void InsertAfter<T>(this ObservableCollection<T> source, Func<T, bool> predicate, T item)
        {
            for (var i = 0; i < source.Count; i++)
            {
                if (predicate(source[i]))
                {
                    source.Insert(i + 1, item);
                    break;
                }
            }
        }

        public static void InsertAfter<T>(this ObservableCollection<T> source, T itemToFind, T itemToInsert)
        {
            InsertAfter(source, i => ReferenceEquals(i, itemToFind), itemToInsert);
        }
    }
}

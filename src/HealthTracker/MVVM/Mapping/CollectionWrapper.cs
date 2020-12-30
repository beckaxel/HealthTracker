using System;
using System.Collections;
using System.Reflection;

namespace HealthTracker.MVVM.Mapping
{
    public class CollectionWrapper : IEnumerable
    {
        private MethodInfo _addMethod;
        private MethodInfo _clearMethod;

        private object _collection;

        public CollectionWrapper(object collection)
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));

            var collectionType = collection.GetType();
            if (!collectionType.IsCollection())
                throw new ArgumentException($"Das Interface ICollection<> muss implementert werden", nameof(collection));

            _addMethod = collectionType.GetMethod("Add");
            _clearMethod = collectionType.GetMethod("Clear");
            _collection = collection;
        }

        public void Add(object item)
        {
            _addMethod.Invoke(_collection, new[] { item });
        }

        public void Clear()
        {
            _clearMethod.Invoke(_collection, new object[] { });
        }

        public IEnumerator GetEnumerator()
        {
            return ((IEnumerable)_collection).GetEnumerator();
        }
    }
}

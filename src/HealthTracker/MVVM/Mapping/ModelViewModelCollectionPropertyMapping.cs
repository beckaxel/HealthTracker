using System;
using System.Reflection;

namespace HealthTracker.MVVM.Mapping
{
    public sealed class ModelViewModelCollectionPropertyMapping : ModelViewModelPropertyMapping
    {
        private static readonly ModelViewModelMapper Mapper = new ModelViewModelMapper();

        public ModelViewModelCollectionPropertyMapping
        (
            ModelViewModelMapping parent,
            PropertyInfo sourceProperty,
            PropertyInfo targetProperty
        )
            : base(parent, sourceProperty, targetProperty)
        {
        }

        protected override void PerformMapping(object source, object target)
        {
            var targetItemType = TargetProperty.GetCollectionItemType();
            var sourceCollection = new CollectionWrapper(SourceProperty.GetValue(source));
            var targetCollection = new CollectionWrapper(TargetProperty.GetValue(target));

            targetCollection.Clear();
            foreach (var sourceItem in sourceCollection)
            {
                var targetItem = Activator.CreateInstance(targetItemType);
                Mapper.Map(sourceItem, targetItem);
                targetCollection.Add(targetItem);
            }
        }
    }
}

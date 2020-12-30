using System;
using System.Reflection;

namespace HealthTracker.MVVM.Mapping
{
    public abstract class ModelViewModelPropertyMapping
    {
        public static ModelViewModelPropertyMapping Create(ModelViewModelMapping parent, PropertyInfo sourceProperty, PropertyInfo targetProperty)
        {
            if (IsValidScalarMapping(sourceProperty, targetProperty))
                return new ModelViewModelScalarPropertyMapping(parent, sourceProperty, targetProperty);
            if (IsValidCollectionMapping(parent.Direction, sourceProperty, targetProperty))
                return new ModelViewModelCollectionPropertyMapping(parent, sourceProperty, targetProperty);
            return null;
        }

        private static bool IsValidCollectionMapping(MappingDirection mappingDirection, PropertyInfo sourceProperty, PropertyInfo targetProperty)
        {
            return
                sourceProperty.IsCollection() &&
                targetProperty.IsCollection() &&
                (
                    (
                        mappingDirection == MappingDirection.ModelToViewModel &&
                        !sourceProperty.GetCollectionItemType().IsViewModel() &&
                        targetProperty.GetCollectionItemType().IsViewModel()
                    ) ||
                    (
                        mappingDirection == MappingDirection.ViewModelToModel &&
                        sourceProperty.GetCollectionItemType().IsViewModel() &&
                        !targetProperty.GetCollectionItemType().IsViewModel()
                    )
                );
        }

        private static bool IsValidScalarMapping(PropertyInfo sourceProperty, PropertyInfo targetProperty)
        {
            return
                sourceProperty.IsScalar() &&
                sourceProperty.CanRead &&
                targetProperty.IsScalar() &&
                targetProperty.CanWrite &&
                targetProperty.IsAssignableFrom(sourceProperty);
        }

        public ModelViewModelMapping Parent { get; }
        public PropertyInfo SourceProperty { get; }
        public PropertyInfo TargetProperty { get; }

        protected ModelViewModelPropertyMapping
        (
            ModelViewModelMapping parent,
            PropertyInfo sourceProperty,
            PropertyInfo targetProperty
        )
        {
            Parent = parent;
            SourceProperty = sourceProperty;
            TargetProperty = targetProperty;
        }

        public void Map(object source, object target)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (source.GetType() != Parent.SourceType)
                throw new ArgumentException($"Erwartet wurde Typ {Parent.SourceType.FullName}, gefunden wurde {source.GetType()}", nameof(source));

            if (target == null)
                throw new ArgumentNullException(nameof(target));

            if (target.GetType() != Parent.TargetType)
                throw new ArgumentException($"Erwartet wurde Typ {Parent.TargetType.FullName}, gefunden wurde {target.GetType()}", nameof(target));

            PerformMapping(source, target);
        }

        protected abstract void PerformMapping(object source, object target);
    }
}

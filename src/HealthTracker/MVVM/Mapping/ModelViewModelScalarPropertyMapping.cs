using System.Reflection;

namespace HealthTracker.MVVM.Mapping
{
    public sealed class ModelViewModelScalarPropertyMapping : ModelViewModelPropertyMapping
    {
        public ModelViewModelScalarPropertyMapping
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
            TargetProperty.SetValue(target, SourceProperty.GetValue(source));
        }
    }
}

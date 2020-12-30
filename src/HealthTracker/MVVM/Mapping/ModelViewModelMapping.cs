using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace HealthTracker.MVVM.Mapping
{
    public class ModelViewModelMapping
    {
        public Type SourceType { get; }
        public Type TargetType { get; }
        public MappingDirection Direction { get; }
        public List<ModelViewModelPropertyMapping> PropertyMappings { get; } = new List<ModelViewModelPropertyMapping>();

        public ModelViewModelMapping(Type sourceType, Type targetType)
        {
            SourceType = sourceType;
            TargetType = targetType;
            Direction = DetectDirection();
            DetectPropertyMappings();
        }

        private MappingDirection DetectDirection()
        {
            if (SourceType.IsViewModel())
            {
                if (TargetType.IsViewModel())
                    throw new NotImplementedException("Can't Map from ViewModel to ViewModel");

                return MappingDirection.ViewModelToModel;
            }
            else if (TargetType.IsViewModel())
            {
                return MappingDirection.ModelToViewModel;
            }
            else
            {
                throw new NotImplementedException("Can't Map from Model to Model");
            }
        }

        private void DetectPropertyMappings()
        {
            var sourceProperties = SourceType
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .ToDictionary(p => p.Name, StringComparer.InvariantCultureIgnoreCase);

            foreach (var targetProperty in TargetType.GetProperties(BindingFlags.Instance | BindingFlags.Public))
            {
                if (!sourceProperties.ContainsKey(targetProperty.Name))
                    continue;

                var propertyMapping = ModelViewModelPropertyMapping.Create(this, sourceProperties[targetProperty.Name], targetProperty);
                if (propertyMapping == null)
                    continue;

                PropertyMappings.Add(propertyMapping);
            }
        }
    }
}

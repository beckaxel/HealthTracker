using System;
using System.Collections;
using System.Collections.Generic;

namespace HealthTracker.MVVM.Mapping
{
    public class ModelViewModelMapper
    {
        private static Dictionary<string, ModelViewModelMapping> Mappings = new Dictionary<string, ModelViewModelMapping>(StringComparer.InvariantCultureIgnoreCase);

        private static string GetCacheKey(object source, object target)
        {
            return $"::{source.GetType().FullName}::=>::{target.GetType().FullName}::";
        }

        private static ModelViewModelMapping GetOrAddMapping(object source, object target)
        {
            var key = GetCacheKey(source, target);
            if (Mappings.ContainsKey(key))
                return Mappings[key];

            var mapping = new ModelViewModelMapping(source.GetType(), target.GetType());
            Mappings.Add(key, mapping);
            return mapping;
        }

        public void Map(object source, object target)
        {
            var mapping = GetOrAddMapping(source, target);
            foreach(var propertyMapping in mapping.PropertyMappings)
                propertyMapping.Map(source, target);
        }
    }
}

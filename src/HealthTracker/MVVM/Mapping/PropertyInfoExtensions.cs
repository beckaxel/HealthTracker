using System;
using System.Reflection;

namespace HealthTracker.MVVM.Mapping
{
    public static class PropertyInfoExtensions
    {
        public static Type GetCollectionItemType(this PropertyInfo propertyInfo)
        {
            return propertyInfo.PropertyType.GetCollectionItemType();
        }

        public static bool IsCollection(this PropertyInfo propertyInfo)
        {
            return propertyInfo.PropertyType.IsCollection();
        }

        public static bool IsScalar(this PropertyInfo propertyInfo)
        {
            return !propertyInfo.PropertyType.IsCollection();
        }

        public static bool IsAssignableFrom(this PropertyInfo propertyInfoVariable, PropertyInfo propertyInfoInstance)
        {
            return propertyInfoVariable.PropertyType.IsAssignableFrom(propertyInfoInstance.PropertyType);
        }
    }
}

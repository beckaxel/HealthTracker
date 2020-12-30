using System;
using System.Collections.Generic;
using System.Linq;

namespace HealthTracker.MVVM.Mapping
{
    public static class TypeExtensions
    {
        public static bool IsViewModel(this Type type)
        {
            return
                type.IsSubclassOf(typeof(ViewModelBase)) ||
                type == typeof(ViewModelBase);
        }

        public static bool IsCollection(this Type type)
        {
            return !type.IsArray && (IsGenericICollection(type) || type.GetInterfaces().Any(IsGenericICollection));
        }

        private static bool IsGenericICollection(Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(ICollection<>);
        }

        public static Type GetCollectionItemType(this Type type)
        {
            return type.GetGenericArguments().FirstOrDefault();
        }
    }
}

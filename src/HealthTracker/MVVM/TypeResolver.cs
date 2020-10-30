using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace HealthTracker.MVVM
{
    public class TypeResolver : ITypeResolver
    {
        public IEnumerable<Type> ResolveTypes(
            string @namespace,
            string prefix = null,
            string suffix = null,
            TypeResolvingOptions options = TypeResolvingOptions.None)
        {
            var comparison = options.HasFlag(TypeResolvingOptions.IgnoreCase)
                ? StringComparison.InvariantCultureIgnoreCase
                : StringComparison.InvariantCulture;

            IEnumerable<Type> result = Assembly
                .GetCallingAssembly()
                .GetTypes();

            result = options.HasFlag(TypeResolvingOptions.IncludeChildNamespaces)
                ? result.Where(t => t.Namespace != null && t.Namespace.StartsWith(@namespace, comparison))
                : result.Where(t => t.Namespace != null && t.Namespace.Equals(@namespace, comparison));

            if (!options.HasFlag(TypeResolvingOptions.IncludeAbstractTypes))
                result = result.Where(t => !t.IsAbstract);

            if (options.HasFlag(TypeResolvingOptions.ClassesOnly))
                result = result.Where(t => t.IsClass);

            if (prefix != null)
                result = result.Where(t => t.Name.StartsWith(prefix, comparison));

            if (suffix != null)
                result = result.Where(t => t.Name.EndsWith(suffix, comparison));

            return result;
        }
    }
}

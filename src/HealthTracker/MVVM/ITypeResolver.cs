using System;
using System.Collections.Generic;

namespace HealthTracker.MVVM
{
    public interface ITypeResolver
    {
        IEnumerable<Type> ResolveTypes(string @namespace, string prefix = null, string suffix = null, TypeResolvingOptions options = TypeResolvingOptions.None);
    }
}
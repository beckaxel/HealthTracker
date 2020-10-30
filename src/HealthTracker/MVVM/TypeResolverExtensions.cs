using System;
using System.Collections.Generic;
using System.Linq;
using HealthTracker.MVVM;

public static class TypeResolverExtensions
{
    public static IEnumerable<Type> ResolveTypes<TBaseClass>
    (
        this ITypeResolver typeResolver,
        string @namespace,
        string prefix = null,
        string suffix = null,
        TypeResolvingOptions options = TypeResolvingOptions.None
    )
    {
        return typeResolver
            .ResolveTypes(@namespace, prefix, suffix, options)
            .Where(t => t.IsSubclassOf(typeof(TBaseClass)));
    }
}
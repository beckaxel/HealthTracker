using System;
namespace HealthTracker.MVVM
{
    [Flags]
    public enum TypeResolvingOptions
    {
        None = 0,
        IgnoreCase = 1,
        IncludeChildNamespaces = 2,
        IncludeAbstractTypes = 4,
        ClassesOnly = 8
    }
}

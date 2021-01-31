using System.Collections.Generic;

namespace HealthTracker.MVVM
{
    public class FilterCollection : List<Filter>
    {
        public string DefaultFilterName { get; set; }
    }
}

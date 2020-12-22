using System.Collections.Generic;

namespace HealthTracker.Models
{
    internal interface IPhotographable
    {
        public ICollection<Photo> Photos { get; set; }
    }
}
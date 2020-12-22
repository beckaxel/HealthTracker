using System.Collections.Generic;

namespace HealthTracker.Models
{
    public interface ITagable
    {
        public ICollection<Tag> Tags { get; set; }
    }
}
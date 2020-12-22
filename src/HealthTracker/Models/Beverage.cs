﻿using System;
using System.Collections.Generic;

namespace HealthTracker.Models
{
    public class Beverage : INameable, IDatable, IPhotographable, ITagable
    {
        public int BeverageId { get; set; }

        public string Name { get; set; }

        public DateTime Date { get; set; }

        public virtual ICollection<Photo> Photos { get; set; } = new HashSet<Photo>();

        public virtual ICollection<Tag> Tags { get; set; } = new HashSet<Tag>();

        public float Amount { get; set; }
    }
}

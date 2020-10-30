using System;
using HealthTracker.ViewModels;

namespace HealthTracker.Services
{
    public class ActiveSectionChangedEventArgs : EventArgs
    {
        public SectionViewModel OldActiveSection { get; }
        public SectionViewModel NewActiveSection { get; }

        public ActiveSectionChangedEventArgs(SectionViewModel oldActiveSection, SectionViewModel newActiveSection)
        {
            OldActiveSection = oldActiveSection;
            NewActiveSection = newActiveSection;
        }
    }
}

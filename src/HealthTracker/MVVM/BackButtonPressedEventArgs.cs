using System;

namespace HealthTracker.MVVM
{
    public class BackButtonPressedEventArgs : EventArgs
    {
        public bool PreventDefault { get; set; }
    }
}

using System;
using Xamarin.Forms;

namespace HealthTracker.MVVM
{
    public class ViewBase : ContentPage
    {
        public event EventHandler<BackButtonPressedEventArgs> BackButtonPressed;

        protected override bool OnBackButtonPressed()
        {
            var args = new BackButtonPressedEventArgs();
            BackButtonPressed?.Invoke(this, args);
            return args.PreventDefault || base.OnBackButtonPressed();
        }
    }
}

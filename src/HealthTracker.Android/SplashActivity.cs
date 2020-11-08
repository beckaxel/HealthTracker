using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;

namespace HealthTracker.Droid
{
    [Activity(Theme = "@style/MainTheme.Splash", MainLauncher = true, NoHistory = true)]
    public class SplashActivity : AppCompatActivity
    {
        public override void OnCreate(Bundle savedInstanceState, PersistableBundle persistentState)
        {
            base.OnCreate(savedInstanceState, persistentState);
        }

        protected override void OnResume()
        {
            base.OnResume();
            Task.Run(() => { LaunchAppAsync(); });
        }
        
        public override void OnBackPressed() { }

        private async void LaunchAppAsync()
        {   
            StartActivity(new Intent(Application.Context, typeof (MainActivity)));
        }
    }
}
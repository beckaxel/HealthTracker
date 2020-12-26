using System.Threading.Tasks;
using HealthTracker.MVVM;
using HealthTracker.Services;
using HealthTracker.Services.Impl;
using HealthTracker.Storage;
using HealthTracker.Storage.Impl;
using HealthTracker.TinyIoC;
using TinyIoC;
using Xamarin.Forms;

namespace HealthTracker
{
    public partial class App : Application, INavigationTarget
    {
        public App()
        {
            InitializeComponent();
        }

        ViewBase INavigationTarget.CurrentView
        {
            get => MainPage as ViewBase;
            set => MainPage = value;
        }

        ViewModelBase INavigationTarget.CurrentViewModel { get; set; }

        protected override void OnStart()
        {
            var container = new TinyIoCContainer();
            IServiceLocator serviceLocator = new TinyIoCServiceLocatorAdapter(container);
            serviceLocator.RegisterSingleton(serviceLocator);
            serviceLocator.RegisterSingleton<IDbContextFactory, DbContextFactory>();
            serviceLocator.RegisterSingleton<INavigationTarget>(this);
            serviceLocator.RegisterSingleton<ITypeResolver, TypeResolver>();
            serviceLocator.RegisterSingleton<IViewService, ViewService>();
            serviceLocator.RegisterSingleton<IViewModelService, ViewModelService>();
            serviceLocator.RegisterSingleton<INavigationService, NavigationService>();
            serviceLocator.RegisterSingleton<ICameraService, CameraService>();
            serviceLocator.RegisterSingleton<ISettingsStorage, EFSettingsStorage>();
            serviceLocator.RegisterSingleton<IUserStorage, EFUserStorage>();
            serviceLocator.RegisterSingleton<IBodyMeasurementStorage, EFBodyMeasurementStorage>();

            //ToDo: Theming (inkl. Darkmode)
            //ToDo: I18n und L10n

            var navigationService = serviceLocator.Resolve<INavigationService>();
            navigationService.Initialize();
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}

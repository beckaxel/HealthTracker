using System;
using System.IO;
using HealthTracker.MVVM;
using HealthTracker.Services;
using HealthTracker.Services.Impl;
using HealthTracker.Storage;
using HealthTracker.Storage.Impl;
using HealthTracker.TinyIoC;
using HealthTracker.ViewModels;
using SQLite;
using TinyIoC;
using Xamarin.Forms;

namespace HealthTracker
{
    public partial class App : Application, INavigationTarget
    {
        public App()
        {
            InitializeComponent();
            
            var container = new TinyIoCContainer();
            IServiceLocator serviceLocator = new TinyIoCServiceLocatorAdapter(container);
            serviceLocator.RegisterSingleton(serviceLocator);
            serviceLocator.RegisterSingleton<INavigationTarget>(this);
            serviceLocator.RegisterSingleton<ITypeResolver, TypeResolver>();
            serviceLocator.RegisterSingleton<IViewService, ViewService>();
            serviceLocator.RegisterSingleton<IViewModelService, ViewModelService>();
            serviceLocator.RegisterSingleton<INavigationService, NavigationService>();
            serviceLocator.RegisterSingleton(new SQLiteConnection
            (
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "HealthTracker.db3"),
                openFlags: SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create | SQLiteOpenFlags.SharedCache
            ));
            serviceLocator.RegisterSingleton<IWeightStorage, SQLiteWeightStorage>();
            //serviceLocator.RegisterSingleton<ISectionViewModel, BeveragesViewModel>("Beverages");
            //serviceLocator.RegisterSingleton<ISectionViewModel, MealsViewModel>("Meals");
            //serviceLocator.RegisterSingleton<ISectionViewModel, WeightViewModel>("Weight");
            //serviceLocator.RegisterSingleton<ISectionViewModel, SleepViewModel>("Sleep");
            //serviceLocator.RegisterSingleton<MainViewModel>();

            //var mainViewModel = serviceLocator.Resolve<MainViewModel>();

            //var weightModel = new Weight { Id = 1000, Date = DateTime.Now, Amount = 100.1m };
            //var editWeightViewModel = new EditWeightViewModel(serviceLocator.Resolve<IWeightStorage>()) { Model = weightModel };
            //var editWeightView = new EditWeightView { ViewModel = editWeightViewModel };
            //mainViewModel.Content = editWeightView;

            //MainPage = new MainView { BindingContext = mainViewModel };

            //ToDo: Alle Sections auf neue Variante umstellen
            //ToDo: Globale Config hinzufügen, inkl. Section Definition
            //ToDo: Theming (Darkmode) nicht vergessen
            //ToDo: HeaderToolbar mit User- und SettingsSymbol für Benutzerbezogene angaben, wie Größe und Alter, Settings z.B. für das Deaktivieren von Sections
            //ToDo: Ins GIT einchecken
            //ToDo: PlayStore freispielen und Autodeploy einrichten

            var navigationService = serviceLocator.Resolve<INavigationService>();
            navigationService.NavigateTo("BeveragesSection");
        }

        ViewBase INavigationTarget.CurrentView
        {
            get => MainPage as ViewBase;
            set => MainPage = value;
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}

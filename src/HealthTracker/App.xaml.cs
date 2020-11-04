﻿using System;
using System.IO;
using HealthTracker.MVVM;
using HealthTracker.Services;
using HealthTracker.Services.Impl;
using HealthTracker.Storage;
using HealthTracker.Storage.Impl;
using HealthTracker.TinyIoC;
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
            serviceLocator.RegisterSingleton<ISettingsStorage, SQLiteSettingsStorage>();
            serviceLocator.RegisterSingleton<IUserStorage, SQLiteUserStorage>();
            serviceLocator.RegisterSingleton<IWeightStorage, SQLiteWeightStorage>();
            
            //ToDo: Theming (inkl. Darkmode)
            //ToDo: I18n und L10n
            
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
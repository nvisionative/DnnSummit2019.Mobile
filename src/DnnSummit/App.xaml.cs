﻿using DnnSummit.Views;
using Prism;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Unity;
using System;
using System.Globalization;
using System.Reflection;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace DnnSummit
{
    public partial class App : PrismApplication
    {
        public const string EntryPoint = "/" + Constants.Navigation.NavigationPage + "/" + Constants.Navigation.TabbedPage;

        public App(IPlatformInitializer initializer = null) : base(initializer)
        {
        }


        protected async override void OnInitialized()
        {
            InitializeComponent();
            ViewModelLocationProvider.SetDefaultViewTypeToViewModelTypeResolver(FindViewModel);
            await NavigationService.NavigateAsync(EntryPoint);
        }


        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            RegisterNavigation(containerRegistry);
            RegisterDependencies(containerRegistry);
        }

        private void RegisterNavigation(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<DnnSummitNavigationPage>(Constants.Navigation.NavigationPage);
            containerRegistry.RegisterForNavigation<DnnSummitTabbedPage>(Constants.Navigation.TabbedPage);
            containerRegistry.RegisterForNavigation<LocationPage>(Constants.Navigation.LocationPage);
            containerRegistry.RegisterForNavigation<ScheduleDetailsPage>(Constants.Navigation.ScheduleDetailsPage);
            containerRegistry.RegisterForNavigation<SessionDetailsPage>(Constants.Navigation.SessionDetailsPage);
            Data.Startup.RegisterDependencies(containerRegistry);
        }

        private void RegisterDependencies(IContainerRegistry containerRegistry)
        {
        }

        // Page/ViewModel Wireup Logic
        //                Page - FooPage
        //                ViewModel - FooViewModel
        private Type FindViewModel(Type viewType)
        {
            var viewName = viewType.FullName
                .Replace("Page", string.Empty)
                .Replace("Views", "ViewModels");

            var viewAssemblyName = viewType.GetTypeInfo().Assembly.FullName;
            var viewModelName = string.Format(CultureInfo.InvariantCulture, "{0}ViewModel, {1}", viewName, viewAssemblyName);

            return Type.GetType(viewModelName);
        }
    }
}
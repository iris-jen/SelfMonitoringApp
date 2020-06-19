using SelfMonitoringApp.Navigation;
using SelfMonitoringApp.Services;
using SelfMonitoringApp.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace SelfMonitoringApp
{
    public partial class App : Application, IHaveMainPage
    {
        public App()
        {
            InitializeComponent();

            //Load existing user data
            DataStore.Initalize();

            var navigator = new NavigationService(this, new ViewLocator());
            var rootViewModel = new MainViewModel(navigator);
            navigator.PresentAsNavigatableMainPage(rootViewModel);
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
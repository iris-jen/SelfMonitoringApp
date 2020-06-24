using SelfMonitoringApp.Models;
using SelfMonitoringApp.Navigation;
using SelfMonitoringApp.Services;
using SelfMonitoringApp.ViewModels;
using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace SelfMonitoringApp
{
    public partial class App : Application, IHaveMainPage
    {
        private static Database _database;

        public static Database Database
        {
            get
            {
                if (_database is null)
                {
                    _database = new Database();
                }

                return _database;
            }
        }

        public App()
        {
            InitializeComponent();

            foreach (MealModel meal in LogSamples.GetMealSamples())
                Database.AddOrModifyModelAsync(meal).Wait();

            foreach (SubstanceModel substance in LogSamples.GetSubstanceSamples())
                Database.AddOrModifyModelAsync(substance).Wait();

            foreach (ActivityModel activity in LogSamples.GetActivitySamples())
                Database.AddOrModifyModelAsync(activity).Wait();

            foreach (MoodModel mood in LogSamples.GetMoodSamples())
                Database.AddOrModifyModelAsync(mood).Wait();

            foreach (SleepModel sleep in LogSamples.GetSleepSamples())
                Database.AddOrModifyModelAsync(sleep).Wait();

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
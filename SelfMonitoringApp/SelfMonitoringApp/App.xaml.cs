using Newtonsoft.Json;
using SelfMonitoringApp.Services;
using SelfMonitoringApp.Services.Navigation;
using SelfMonitoringApp.ViewModels;
using Splat;
using System.IO;
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
            ConfigureContainer();

            // Present the root view
            Locator.Current.GetService<INavigationService>().
               PresentAsNavigatableMainPage(new MainViewModel());
        }

        public void ConfigureContainer()
        {
            Locator.CurrentMutable.RegisterConstant( new SuggestionService(), typeof(ISuggestionService));
            Locator.CurrentMutable.RegisterConstant( new LocalSqlDatabaseService(), typeof(IDatabaseService));
            Locator.CurrentMutable.RegisterConstant( new NavigationService(this, new ViewLocator()), typeof(INavigationService));

            var Logger = new DebugLogger()
            {
                Level = LogLevel.Debug
            };

            Locator.CurrentMutable.RegisterConstant(Logger, typeof(ILogger));
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

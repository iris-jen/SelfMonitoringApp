using SelfMonitoringApp.Navigation;
using SelfMonitoringApp.Services;
using SelfMonitoringApp.ViewModels;
using Splat;

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
            Locator.CurrentMutable.Register(() => new LocalSqlDatabaseService(), typeof(IDatabaseService));
            Locator.CurrentMutable.Register(() => new NavigationService(this, new ViewLocator()), typeof(INavigationService));
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

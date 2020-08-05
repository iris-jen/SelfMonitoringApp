using SelfMonitoringApp.ViewModels.Base;
using SelfMonitoringApp.ViewModels.Logs;
using System;
using System.Globalization;
using System.IO;

using Xamarin.Forms;
using Acr.UserDialogs;
using SelfMonitoringApp.Pages;
using System.Threading.Tasks;


namespace SelfMonitoringApp.ViewModels
{
    public class MainViewModel : ViewModelBase, INavigationViewModel
    {
        public Command<PageNames> NavigateCommand { get; set; }

        public MainViewModel()   
        {
            NavigateCommand = new Command<PageNames>((page) => Navigate(page));
        }

        private Task GetNavigatorTask(PageNames page) => page switch
        {
            PageNames.MoodEditor          => _navigator.NavigateTo(new MoodViewModel()),
            PageNames.SleepEditor         => _navigator.NavigateTo(new SleepViewModel()),
            PageNames.ActivityEditor      => _navigator.NavigateTo(new ActivityViewModel()),
            PageNames.MealEditor          => _navigator.NavigateTo(new MealViewModel()),
            PageNames.SocializationEditor => _navigator.NavigateTo(new SocializationViewModel()),
            PageNames.SubstanceEditor     => _navigator.NavigateTo(new SubstanceViewModel()),
            PageNames.RawLogViewer        => _navigator.NavigateTo(new DataExplorerViewModel()),
            PageNames.NotificationsViewer => _navigator.NavigateTo(new NotificationsViewModel()),
            PageNames.GoalsViewer         => _navigator.NavigateTo(new GoalsViewModel()), 
            PageNames.Settings            => _navigator.NavigateTo(new SettingsViewModel()),
            _ => throw new DirectoryNotFoundException($"Cant find directory -- {page}")
        };

        public void Navigate(PageNames page)
        {
            try
            {
                GetNavigatorTask(page).SafeFireAndForget(false);
            }
            catch(Exception ex)
            {
                UserDialogs.Instance.Alert(ex.ToString(), "Oh no i could not navigate :(");
            }
        }
    }
}

using SelfMonitoringApp.ViewModels.Base;
using System.Globalization;
using System.IO;
using Xamarin.Forms;

namespace SelfMonitoringApp.ViewModels
{
    public class MainViewModel : ViewModelBase, INavigationViewModel
    {
        public Command<string> NavigateCommand { get; set; }

        public string NavigationNodeName => "main";

        public MainViewModel()   
        {
            NavigateCommand = new Command<string>((param) => Navigate(param));
        }

        public void Navigate(string page)
        {
            switch(page.ToLower(CultureInfo.CurrentCulture))
            {
                case MoodViewModel.NavigationNodeName:
                    _navigator.NavigateTo(new MoodViewModel()).SafeFireAndForget(false);
                    break;
                case SleepViewModel.NavigationNodeName:
                    _navigator.NavigateTo(new SleepViewModel()).SafeFireAndForget(false);
                    break;
                case SubstanceViewModel.NavigationNodeName:
                    _navigator.NavigateTo(new SubstanceViewModel()).SafeFireAndForget(false);
                    break;
                case MealViewModel.NavigationNodeName:
                    _navigator.NavigateTo(new MealViewModel()).SafeFireAndForget(false);
                    break;
                case ActivityViewModel.NavigationNodeName:
                    _navigator.NavigateTo(new ActivityViewModel()).SafeFireAndForget(false);
                    break;
                case SettingsViewModel.NavigationNodeName:
                    _navigator.NavigateTo(new SettingsViewModel()).SafeFireAndForget(false);
                    break;
                case DataExplorerViewModel.NavigationNodeName:
                    _navigator.NavigateTo(new DataExplorerViewModel()).SafeFireAndForget(false);
                    break;
                case HelpViewModel.NavigationNodeName:
                    _navigator.NavigateTo(new HelpViewModel()).SafeFireAndForget(false);
                    break;
                case NotificationsViewModel.NavigationNodeName:
                    _navigator.NavigateTo(new NotificationsViewModel()).SafeFireAndForget(false);
                    break;
                default:
                    throw new DirectoryNotFoundException($"Cant find nav directory {page}");
            }
        }
    }
}

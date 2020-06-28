
using SelfMonitoringApp.Navigation;
using SelfMonitoringApp.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SelfMonitoringApp.ViewModels
{
    public class MainViewModel : NavigatableViewModelBase, INavigationViewModel
    {
        public Command<string> NavigateCommand { get; set; }

        public string NavigationNodeName => "main";

        public MainViewModel(INavigationService navService) : base(navService)
        {
            NavigateCommand = new Command<string>( (param) => Navigate(param));
        }

        public void Navigate(string page)
        {
            switch(page.ToLower(CultureInfo.CurrentCulture))
            {
                case MoodViewModel.NavigationNodeName:
                    _navigator.NavigateTo(new MoodViewModel(_navigator)).SafeFireAndForget(false);
                    break;
                case SleepViewModel.NavigationNodeName:
                    _navigator.NavigateTo(new SleepViewModel(_navigator)).SafeFireAndForget(false);
                    break;
                case SubstanceViewModel.NavigationNodeName:
                    _navigator.NavigateTo(new SubstanceViewModel(_navigator)).SafeFireAndForget(false);
                    break;
                case MealViewModel.NavigationNodeName:
                    _navigator.NavigateTo(new MealViewModel(_navigator)).SafeFireAndForget(false);
                    break;
                case ActivityViewModel.NavigationNodeName:
                    _navigator.NavigateTo(new ActivityViewModel(_navigator)).SafeFireAndForget(false);
                    break;
                case SettingsViewModel.NavigationNodeName:
                    _navigator.NavigateTo(new SettingsViewModel(_navigator)).SafeFireAndForget(false);
                    break;
                case DataExplorerViewModel.NavigationNodeName:
                    _navigator.NavigateTo(new DataExplorerViewModel(_navigator)).SafeFireAndForget(false);
                    break;
                case HelpViewModel.NavigationNodeName:
                    _navigator.NavigateTo(new HelpViewModel(_navigator)).SafeFireAndForget(false);
                    break;
                case NotificationsViewModel.NavigationNodeName:
                    _navigator.NavigateTo(new NotificationsViewModel(_navigator)).SafeFireAndForget(false);
                    break;
                default:
                    throw new DirectoryNotFoundException($"Cant find nav directory {page}");
            }
        }
    }
}

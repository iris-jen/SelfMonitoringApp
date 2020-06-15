using Meziantou.Framework;
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
                    _navigator.NavigateTo(new MoodViewModel(_navigator)).Forget();
                    break;
                case SleepViewModel.NavigationNodeName:
                    _navigator.NavigateTo(new SleepViewModel(_navigator)).Forget();
                    break;
                case SubstanceViewModel.NavigationNodeName:
                    _navigator.NavigateTo(new SubstanceViewModel(_navigator)).Forget();
                    break;
                case MealViewModel.NavigationNodeName:
                    _navigator.NavigateTo(new MealViewModel(_navigator)).Forget();
                    break;
                case SettingsViewModel.NavigationNodeName:
                    _navigator.NavigateTo(new SettingsViewModel(_navigator)).Forget();
                    break;
                case DataExplorerViewModel.NavigationNodeName:
                    _navigator.NavigateTo(new DataExplorerViewModel(_navigator)).Forget();
                    break;
                default:
                    throw new DirectoryNotFoundException($"Cant find nav directory {page}");
            }
        }
    }
}

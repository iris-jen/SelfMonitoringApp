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
            NavigateCommand = new Command<string>(async (param) => await Navigate(param));
        }

        public async Task Navigate(string page)
        {
            switch(page.ToLower(CultureInfo.CurrentCulture))
            {
                case MoodViewModel.NavigationNodeName:
                    await _navigator.NavigateTo(new MoodViewModel(_navigator));
                    break;
                case SleepViewModel.NavigationNodeName:
                    await _navigator.NavigateTo(new SleepViewModel(_navigator));
                    break;
                case SubstanceViewModel.NavigationNodeName:
                    await _navigator.NavigateTo(new SubstanceViewModel(_navigator));
                    break;
                case MealViewModel.NavigationNodeName:
                    await _navigator.NavigateTo(new MealViewModel(_navigator));
                    break;
                case SettingsViewModel.NavigationNodeName:
                    await _navigator.NavigateTo(new SettingsViewModel(_navigator));
                    break;
                case DataExplorerViewModel.NavigationNodeName:
                    await _navigator.NavigateTo(new DataExplorerViewModel(_navigator));
                    break;
                default:
                    throw new DirectoryNotFoundException($"Cant find nav directory {page}");
            }
        }
    }
}

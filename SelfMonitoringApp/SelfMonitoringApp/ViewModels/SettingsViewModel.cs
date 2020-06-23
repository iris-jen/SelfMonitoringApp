using SelfMonitoringApp.Models;
using SelfMonitoringApp.Navigation;
using SelfMonitoringApp.Services;
using SelfMonitoringApp.ViewModels.Base;

using Xamarin.Forms;

namespace SelfMonitoringApp.ViewModels
{
    public class SettingsViewModel : NavigatableViewModelBase, INavigationViewModel
    {
        public const string NavigationNodeName = "settings";

        public Command<string> DeleteLogCommand { get; private set; }

        public SettingsViewModel(INavigationService navService)
            :base(navService)
        {
            DeleteLogCommand = new Command<string>(DeleteModelFile);
        }

        public void DeleteModelFile(string modelType)
        {
            switch(modelType)
            {
  
            }
        }
    }
}

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
                case "sleep":
                    DataStore.DeleteStore(ModelType.Sleep);
                    break;
                case "meal":
                    DataStore.DeleteStore(ModelType.Meal);
                    break;
                case "substance":
                    DataStore.DeleteStore(ModelType.Substance);
                    break;
                case "mood":
                    DataStore.DeleteStore(ModelType.Mood);
                    break;
                case "activity":
                    DataStore.DeleteStore(ModelType.Activity);
                    break;
                case "all":
                    DataStore.DeleteAll();
                    break;
            }
        }
    }
}

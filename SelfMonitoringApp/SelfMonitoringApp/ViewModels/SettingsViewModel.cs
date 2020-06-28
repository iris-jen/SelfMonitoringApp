using SelfMonitoringApp.Models;
using SelfMonitoringApp.Navigation;
using SelfMonitoringApp.Services;
using SelfMonitoringApp.ViewModels.Base;
using System.Globalization;
using System.Threading.Tasks;
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
            DeleteLogCommand = new Command<string>(async(s)=> await DeleteModelFile(s));
        }

        public async Task DeleteModelFile(string modelType)
        {
            switch(modelType.ToLower(CultureInfo.CurrentCulture))
            {
                case SleepViewModel.NavigationNodeName:
                    await App.Database.ClearSpecificDatabase(ModelType.Sleep);
                    break;
                case MoodViewModel.NavigationNodeName:
                    await App.Database.ClearSpecificDatabase(ModelType.Mood);
                    break;
                case ActivityViewModel.NavigationNodeName:
                    await App.Database.ClearSpecificDatabase(ModelType.Activity);
                    break;
                case MealViewModel.NavigationNodeName:
                    await App.Database.ClearSpecificDatabase(ModelType.Meal);
                    break;
                case SubstanceViewModel.NavigationNodeName:
                    await App.Database.ClearSpecificDatabase(ModelType.Substance);
                    break;
                case "all":
                    await App.Database.ClearSpecificDatabase(ModelType.Substance);
                    await App.Database.ClearSpecificDatabase(ModelType.Meal);
                    await App.Database.ClearSpecificDatabase(ModelType.Activity);
                    await App.Database.ClearSpecificDatabase(ModelType.Mood);
                    await App.Database.ClearSpecificDatabase(ModelType.Sleep);         
                    break;
            }
        }
    }
}

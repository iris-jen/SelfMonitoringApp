using SelfMonitoringApp.Models;
using SelfMonitoringApp.Navigation;
using SelfMonitoringApp.Services;
using SelfMonitoringApp.ViewModels.Base;

using System.Globalization;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SelfMonitoringApp.ViewModels
{
    public class SettingsViewModel : ViewModelBase, INavigationViewModel
    {
        public const string NavigationNodeName = "settings";
        public Command<string> DeleteLogCommand { get; private set; }

        public SettingsViewModel()
        {
            DeleteLogCommand = new Command<string>(async(s)=> await DeleteModelFile(s));
        }

        public async Task DeleteModelFile(string modelType)
        {
            switch(modelType.ToLower(CultureInfo.CurrentCulture))
            {
                case SleepViewModel.NavigationNodeName:
                     await _database.ClearSpecificDatabase(ModelType.Sleep);
                    break;
                case MoodViewModel.NavigationNodeName:
                    await _database.ClearSpecificDatabase(ModelType.Mood);
                    break;
                case ActivityViewModel.NavigationNodeName:
                    await _database.ClearSpecificDatabase(ModelType.Activity);
                    break;
                case MealViewModel.NavigationNodeName:
                    await _database.ClearSpecificDatabase(ModelType.Meal);
                    break;
                case SubstanceViewModel.NavigationNodeName:
                    await _database.ClearSpecificDatabase(ModelType.Substance);
                    break;
                case "all":
                    await Task.WhenAll(_database.ClearSpecificDatabase(ModelType.Substance),
                    _database.ClearSpecificDatabase(ModelType.Meal),
                    _database.ClearSpecificDatabase(ModelType.Activity),
                    _database.ClearSpecificDatabase(ModelType.Mood),
                    _database.ClearSpecificDatabase(ModelType.Sleep));
                    break;
            }
        }
    }
}

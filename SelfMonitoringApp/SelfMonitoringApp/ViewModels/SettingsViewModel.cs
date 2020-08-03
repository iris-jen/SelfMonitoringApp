using SelfMonitoringApp.Models.Base;
using SelfMonitoringApp.ViewModels.Base;
using SelfMonitoringApp.ViewModels.Logs;
using System.Globalization;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SelfMonitoringApp.ViewModels
{
    public class SettingsViewModel : ViewModelBase, INavigationViewModel
    {
        public const string NavigationNodeName = "settings";
        public Command<ModelType> DeleteLogCommand { get; private set; }

        public SettingsViewModel()
        {
            DeleteLogCommand = new Command<ModelType>(async(s)=> await DeleteModelFile(s));
        }

        public async Task DeleteModelFile(ModelType modelType)
        {
            if (modelType == ModelType.All)
            {
                await Task.WhenAll
                (   
                    _database.ClearSpecificDatabase(ModelType.Substance),
                    _database.ClearSpecificDatabase(ModelType.Meal),
                    _database.ClearSpecificDatabase(ModelType.Activity),
                    _database.ClearSpecificDatabase(ModelType.Mood),
                    _database.ClearSpecificDatabase(ModelType.Sleep)
                );
            }
            else
                await _database.ClearSpecificDatabase(modelType);
            
        }
    }
}

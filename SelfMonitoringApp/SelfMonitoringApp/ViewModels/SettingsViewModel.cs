using SelfMonitoringApp.Models;
using SelfMonitoringApp.Models.Base;
using SelfMonitoringApp.ViewModels.Base;
using SelfMonitoringApp.ViewModels.Logs;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SelfMonitoringApp.ViewModels
{
    public class SettingsViewModel : ViewModelBase, INavigationViewModel
    {
        public const string NavigationNodeName = "settings";
        public Command<ModelType> DeleteLogCommand { get; private set; }
        public Command AddSampleLogsCommand { get; private set; }
        public SettingsViewModel()
        {
            DeleteLogCommand = new Command<ModelType>(async(s)=> await DeleteModelFile(s));
            AddSampleLogsCommand = new Command(async()=> await AddExamples());
        }

        public async Task AddExamples()
        {
            List<Task> taskList = new List<Task>();

            foreach (ActivityModel model in LogSamples.GetActivitySamples())
                taskList.Add(_database.AddOrModifyModelAsync(model));

            foreach (MealModel model in LogSamples.GetMealSamples())
                taskList.Add(_database.AddOrModifyModelAsync(model));

            foreach (MoodModel model in LogSamples.GetMoodSamples())
                taskList.Add( _database.AddOrModifyModelAsync(model));

            foreach (SleepModel model in LogSamples.GetSleepSamples())
                taskList.Add(_database.AddOrModifyModelAsync(model));

            foreach (SubstanceModel model in LogSamples.GetSubstanceSamples())
                taskList.Add(_database.AddOrModifyModelAsync(model));

            await Task.WhenAll(taskList);
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

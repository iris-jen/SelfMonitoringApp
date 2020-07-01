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

        public Command AddRandomLogsCommand { get; private set; }


        public SettingsViewModel(INavigationService navService)
            :base(navService)
        {
            DeleteLogCommand = new Command<string>(async(s)=> await DeleteModelFile(s));
            AddRandomLogsCommand = new Command(AddExamples);
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
                    await Task.WhenAll(App.Database.ClearSpecificDatabase(ModelType.Substance),
                    App.Database.ClearSpecificDatabase(ModelType.Meal),
                    App.Database.ClearSpecificDatabase(ModelType.Activity),
                    App.Database.ClearSpecificDatabase(ModelType.Mood),
                    App.Database.ClearSpecificDatabase(ModelType.Sleep));
                    break;
            }
        }

        public void AddExamples()
        {  
            foreach(ActivityModel model in LogSamples.GetActivitySamples())
                App.Database.AddOrModifyModelAsync(model).Wait();

            foreach (MealModel model in LogSamples.GetMealSamples())
                App.Database.AddOrModifyModelAsync(model).Wait();

            foreach (MoodModel model in LogSamples.GetMoodSamples())
                App.Database.AddOrModifyModelAsync(model).Wait();

            foreach (SleepModel model in LogSamples.GetSleepSamples())
                App.Database.AddOrModifyModelAsync(model).Wait();

            foreach (SubstanceModel model in LogSamples.GetSubstanceSamples())
                App.Database.AddOrModifyModelAsync(model).Wait();
        }
    }
}

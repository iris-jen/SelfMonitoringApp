using SelfMonitoringApp.Models;
using SelfMonitoringApp.Navigation;
using SelfMonitoringApp.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;
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

        public string MoodJson=> DataStore.MoodJson;
        public string SleepJson => DataStore.SleepJson;
        public string SubstanceJson =>DataStore.SubstanceJson;
        public string MealJson => DataStore.MealJson;


        public int TotalMoods => DataStore.TotalMoods;
        public int TotalSleeps => DataStore.TotalSleeps;
        public int TotalSubstances => DataStore.TotalSubstances;
        public int TotalMeals => DataStore.TotalMeals;

        public void DeleteModelFile(string modelType)
        {
            switch(modelType)
            {
                case "sleep":
                    DataStore.DeleteStore(ModelType.Sleep);
                    NotifyPropertyChanged(nameof(SleepJson));
                    NotifyPropertyChanged(nameof(TotalSleeps));
                    break;
                case "meal":
                    DataStore.DeleteStore(ModelType.Meal);
                    NotifyPropertyChanged(nameof(MealJson));
                    NotifyPropertyChanged(nameof(TotalMeals));
                    break;
                case "substance":
                    DataStore.DeleteStore(ModelType.Substance);
                    NotifyPropertyChanged(nameof(SubstanceJson));
                    NotifyPropertyChanged(nameof(TotalSubstances));
                    break;
                case "mood":
                    DataStore.DeleteStore(ModelType.Mood);
                    NotifyPropertyChanged(nameof(MoodJson));
                    NotifyPropertyChanged(nameof(TotalMoods));
                    break;
                case "all":
                    DataStore.DeleteAll();
                    break;
            }
        }
    }
}

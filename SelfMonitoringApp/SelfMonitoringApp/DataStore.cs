using Newtonsoft.Json;
using SelfMonitoringApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using System.Text;
using System.Runtime.Serialization;
using System.Data;

namespace SelfMonitoringApp
{
    public static class DataStore
    {
        private static List<MealModel> _meals = new List<MealModel>();
        private static List<SubstanceModel> _substances = new List<SubstanceModel>();
        private static List<MoodModel> _moods = new List<MoodModel>();
        private static List<SleepModel> _sleeps = new List<SleepModel>();

        public static SettingsModel UserSettings { get; set; }
 
        private static readonly string _storeDirectory = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        
        //todo.. this is pooop, need to serialize through IModel Interface eventually
        private static readonly string _mealFileName = "meal.json";
        private static readonly string _moodFileName = "mood.json";
        private static readonly string _substanceFileName = "substance.json";
        private static readonly string _settingsFileName = "settings.json";
        private static readonly string _sleepFileName = "sleep.json";

        private static readonly string _mealFilePath = Path.Combine(_storeDirectory, _mealFileName);
        private static readonly string _moodFilePath = Path.Combine(_storeDirectory, _moodFileName);
        private static readonly string _substanceFilePath = Path.Combine(_storeDirectory, _substanceFileName);
        private static readonly string _sleepFilePath = Path.Combine(_storeDirectory, _sleepFileName);
        private static readonly string _settingsFilPath = Path.Combine(_storeDirectory, _settingsFileName);

        public static void Initalize()
        {
            if (File.Exists(_mealFilePath))
            {
                var contents = File.ReadAllText(_mealFilePath);
                _meals = JsonConvert.DeserializeObject<List<MealModel>>(contents);
            }

            if (File.Exists(_moodFilePath))
            {
                var contents = File.ReadAllText(_moodFilePath);
                _moods = JsonConvert.DeserializeObject<List<MoodModel>>(contents);
            }

            if (File.Exists(_substanceFilePath))
            {
                var contents = File.ReadAllText(_substanceFilePath);
                _substances = JsonConvert.DeserializeObject<List<SubstanceModel>>(contents);
            }

            if (File.Exists(_sleepFilePath))
            {
                var contents = File.ReadAllText(_sleepFilePath);
                _sleeps = JsonConvert.DeserializeObject<List<SleepModel>>(contents);
            }

            if (File.Exists(_settingsFilPath))
            {
                var contents = File.ReadAllText(_settingsFilPath);
                UserSettings = JsonConvert.DeserializeObject<SettingsModel>(contents);
            }
        }

   
        public static void AddModel(IModel NewModel)
        {
#if DEBUG
            Debug.WriteLine($"Adding new model to list... LogType = {NewModel.LogType}");

            Debug.WriteLine("Model JSON... " +
               $"{JsonConvert.SerializeObject(NewModel)}");
#endif

            switch (NewModel.LogType)
            {
                case ModelType.Meal:
                    _meals.Add(NewModel as MealModel);
                    break;
                case ModelType.Mood:
                    _moods.Add(NewModel as MoodModel);
                    break;
                case ModelType.Settings:
                    UserSettings = NewModel as SettingsModel;
                    break;
                case ModelType.Sleep:
                    _sleeps.Add(NewModel as SleepModel);
                    break;
                case ModelType.Substance:
                    _substances.Add(NewModel as SubstanceModel);
                    break;
            }

            SaveStore(NewModel.LogType);
        }

        public static void SaveStore(ModelType modelType)
        {
            var listJson = string.Empty;
            var path = string.Empty;
            
            switch (modelType)
            {
                case ModelType.Meal:
                    listJson = JsonConvert.SerializeObject(_meals, Formatting.Indented);
                    path = _mealFilePath;
                    break;
                case ModelType.Mood:
                    listJson = JsonConvert.SerializeObject(_moods, Formatting.Indented);
                    path = _moodFilePath;
                    break;
                case ModelType.Settings:
                    listJson = JsonConvert.SerializeObject(_settingsFileName);
                    path = _settingsFilPath;
                    break;
                case ModelType.Sleep:
                    listJson = JsonConvert.SerializeObject(_sleeps, Formatting.Indented);
                    path = _sleepFilePath;
                    break;
                case ModelType.Substance:
                    listJson = JsonConvert.SerializeObject(_sleeps, Formatting.Indented);
                    path = _substanceFilePath;
                    break;
            }
#if DEBUG
            Debug.WriteLine($"Writing {modelType} data to : {path} - {listJson}");
#endif
            File.WriteAllText(path, listJson); 
        }

        public static List<IModel> GetModels()
        {
            throw new NotImplementedException();
        }
    }
}
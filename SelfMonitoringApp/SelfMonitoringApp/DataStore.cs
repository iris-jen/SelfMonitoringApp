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

        public static string MealFilePath
        {
            get => Path.Combine(_storeDirectory, _mealFileName);
        }

        public static string MoodFilePath
        {
            get => Path.Combine(_storeDirectory, _moodFileName);
        }

        public static string SubstanceFilePath
        {
            get => Path.Combine(_storeDirectory, _substanceFileName);
        }

        public static string SleepFilePath
        {
            get => Path.Combine(_storeDirectory, _sleepFileName);
        }

        private static readonly string _settingsFilPath = Path.Combine(_storeDirectory, _settingsFileName);

        public static string MoodJson { get; set; }
        public static string SleepJson { get; set; }
        public static string SubstanceJson { get; set; }
        public static string MealJson { get; set; }


        public static int TotalMoods => _moods.Count;
        public static int TotalSleeps => _sleeps.Count;
        public static int TotalSubstances => _substances.Count;
        public static int TotalMeals => _meals.Count;

        public static void Initalize()
        {
            if (File.Exists(MealFilePath))
            {
                var contents = File.ReadAllText(MealFilePath);
                MealJson = contents;
                _meals = JsonConvert.DeserializeObject<List<MealModel>>(contents);
            }

            if (File.Exists(MoodFilePath))
            {
                var contents = File.ReadAllText(MoodFilePath);
                MoodJson = contents;
                _moods = JsonConvert.DeserializeObject<List<MoodModel>>(contents);
            }

            if (File.Exists(SubstanceFilePath))
            {
                var contents = File.ReadAllText(SubstanceFilePath);
                SubstanceJson = contents;
                _substances = JsonConvert.DeserializeObject<List<SubstanceModel>>(contents);
            }

            if (File.Exists(SleepFilePath))
            {
                var contents = File.ReadAllText(SleepFilePath);
                SleepJson = contents;
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
                    MealJson = listJson;
                    path = MealFilePath;
                    break;
                case ModelType.Mood:
                    listJson = JsonConvert.SerializeObject(_moods, Formatting.Indented);
                    MoodJson = listJson;
                    path = MoodFilePath;
                    break;
                case ModelType.Settings:
                    listJson = JsonConvert.SerializeObject(_settingsFileName);
                    path = _settingsFilPath;
                    break;
                case ModelType.Sleep:
                    listJson = JsonConvert.SerializeObject(_sleeps, Formatting.Indented);
                    SleepJson = listJson;
                    path = SleepFilePath;
                    break;
                case ModelType.Substance:
                    listJson = JsonConvert.SerializeObject(_sleeps, Formatting.Indented);
                    SubstanceJson = listJson;
                    path = SubstanceFilePath;
                    break;
            }
            File.WriteAllText(path, listJson); 
        }

        public static void DeleteStore(ModelType modelType)
        {
            switch (modelType)
            {
                case ModelType.Meal:
                    if (File.Exists(MealFilePath))
                        File.Delete(MealFilePath);

                    MealJson = string.Empty;
                    _meals.Clear();
                    break;
                case ModelType.Mood:
                    if (File.Exists(MoodFilePath))
                        File.Delete(MoodFilePath);

                    MoodJson = string.Empty;
                    _moods.Clear();
                    break;
                case ModelType.Sleep:
                    if (File.Exists(SleepFilePath))
                        File.Delete(SleepFilePath);

                    SleepJson = string.Empty;
                    _sleeps.Clear();
                    break;
                case ModelType.Substance:
                    if (File.Exists(SubstanceFilePath))
                        File.Delete(SubstanceFilePath);

                    SubstanceJson = string.Empty;
                    _substances.Clear();
                    break;
            }
        }

        public static void DeleteAll()
        {
            DeleteStore(ModelType.Meal);
            DeleteStore(ModelType.Mood);
            DeleteStore(ModelType.Sleep);
            DeleteStore(ModelType.Substance);
        }

        public static List<IModel> GetModels()
        {
            throw new NotImplementedException();
        }
    }
}
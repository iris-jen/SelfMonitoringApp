using Newtonsoft.Json;
using SelfMonitoringApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using System.Text;
using System.Runtime.Serialization;
using System.Data;
using System.Threading.Tasks;
using Xamarin.Forms.Internals;

namespace SelfMonitoringApp.Services
{
    public static class DataStore
    {
        public static List<MealModel> Meals           { get; private set; } = new List<MealModel>();
        public static List<SubstanceModel> Substances { get; private set; } = new List<SubstanceModel>();
        public static List<MoodModel> Moods           { get; private set; } = new List<MoodModel>();
        public static List<SleepModel> Sleeps         { get; private set; } = new List<SleepModel>();
        public static List<ActivityModel> Activities  { get; private set; } = new List<ActivityModel>();

        public static SettingsModel UserSettings { get; set; }

        private static readonly string _storeDirectory = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

        //todo.. this is pooop, need to serialize through IModel Interface eventually
        private static readonly string _mealFileName = "meal.json";
        private static readonly string _moodFileName = "mood.json";
        private static readonly string _substanceFileName = "substance.json";
        private static readonly string _settingsFileName = "settings.json";
        private static readonly string _sleepFileName = "sleep.json";
        private static readonly string _activityFileName = "activity.json";

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

        public static string ActivityFilePath
        {
            get => Path.Combine(_storeDirectory, _activityFileName);
        }

        private static readonly string _settingsFilPath = Path.Combine(_storeDirectory, _settingsFileName);

        public static void Initalize()
        {
            if (File.Exists(MealFilePath))
            {
                var contents = File.ReadAllText(MealFilePath);
                Meals = JsonConvert.DeserializeObject<List<MealModel>>(contents);
            }

            if (File.Exists(MoodFilePath))
            {
                var contents = File.ReadAllText(MoodFilePath);
                Moods = JsonConvert.DeserializeObject<List<MoodModel>>(contents);
            }

            if (File.Exists(SubstanceFilePath))
            {
                var contents = File.ReadAllText(SubstanceFilePath);
                Substances = JsonConvert.DeserializeObject<List<SubstanceModel>>(contents);
            }

            if (File.Exists(SleepFilePath))
            {
                var contents = File.ReadAllText(SleepFilePath);
                Sleeps = JsonConvert.DeserializeObject<List<SleepModel>>(contents);
            }

            if (File.Exists(ActivityFilePath))
            {
                var contents = File.ReadAllText(ActivityFilePath);
                Activities = JsonConvert.DeserializeObject<List<ActivityModel>>(contents);
            }

            if (File.Exists(_settingsFilPath))
            {
                var contents = File.ReadAllText(_settingsFilPath);
                UserSettings = JsonConvert.DeserializeObject<SettingsModel>(contents);
            }

            AddTestModels();
        }

   
        public static void AddModel(IModel NewModel)
        {
            switch (NewModel.LogType)
            {
                case ModelType.Meal:
                    Meals.Add(NewModel as MealModel);
                    break;
                case ModelType.Mood:
                    Moods.Add(NewModel as MoodModel);
                    break;
                case ModelType.Settings:
                    UserSettings = NewModel as SettingsModel;
                    break;
                case ModelType.Sleep:
                    Sleeps.Add(NewModel as SleepModel);
                    break;
                case ModelType.Substance:
                    Substances.Add(NewModel as SubstanceModel);
                    break;
                case ModelType.Activity:
                    Activities.Add(NewModel as ActivityModel);
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
                    listJson = JsonConvert.SerializeObject(Meals, Formatting.Indented);

                    path = MealFilePath;
                    break;
                case ModelType.Mood:
                    listJson = JsonConvert.SerializeObject(Moods, Formatting.Indented);

                    path = MoodFilePath;
                    break;
                case ModelType.Settings:
                    listJson = JsonConvert.SerializeObject(_settingsFileName);
                    path = _settingsFilPath;
                    break;
                case ModelType.Sleep:
                    listJson = JsonConvert.SerializeObject(Sleeps, Formatting.Indented);

                    path = SleepFilePath;
                    break;
                case ModelType.Substance:
                    listJson = JsonConvert.SerializeObject(Substances, Formatting.Indented);

                    path = SubstanceFilePath;
                    break;
                case ModelType.Activity:
                    listJson = JsonConvert.SerializeObject(Activities, Formatting.Indented);

                    path = ActivityFilePath;
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

                    Meals.Clear();
                    break;
                case ModelType.Mood:
                    if (File.Exists(MoodFilePath))
                        File.Delete(MoodFilePath);


                    Moods.Clear();
                    break;
                case ModelType.Sleep:
                    if (File.Exists(SleepFilePath))
                        File.Delete(SleepFilePath);

 
                    Sleeps.Clear();
                    break;
                case ModelType.Substance:
                    if (File.Exists(SubstanceFilePath))
                        File.Delete(SubstanceFilePath);


                    Substances.Clear();
                    break;
                case ModelType.Activity:
                    if (File.Exists(ActivityFilePath))
                        File.Delete(ActivityFilePath);

                    Activities.Clear();
                    break;
            }
        }

        public static void DeleteAll()
        {
            DeleteStore(ModelType.Meal);
            DeleteStore(ModelType.Mood);
            DeleteStore(ModelType.Sleep);
            DeleteStore(ModelType.Substance);
            DeleteStore(ModelType.Activity);
        }

        public static void ModifyModel(IModel model)
        {

        }

        public static void DeleteModel(IModel model)
        {
  
        }


        public static void AddTestModels()
        {
            var year = 2020;
            var month = 06;
            var day1 = 2;
            var day2 = 3;
            var day3 = 4;

            Meals = new List<MealModel>()
            {
                // Day 1
                new MealModel
                {
                    MealSize = "Small",
                    MealType = "Breakfast",
                    Description = "Cereal",
                    Satisfaction = 5.4,
                    RegisteredTime = new DateTime(year,month,day1, hour: 8, minute:30, second:0)
                },
                new MealModel
                {
                    MealSize = "Regular",
                    MealType = "Lunch",
                    Description = "Sandwich",
                    Satisfaction = 9,
                    RegisteredTime = new DateTime(year,month,day1, hour: 12, minute:45, second:0)
                },
                new MealModel
                {
                    MealSize = "Small",
                    MealType = "Snack",
                    Description = "Chips",
                    Satisfaction = 2,
                    RegisteredTime = new DateTime(year,month,day1, hour: 14, minute: 3, second:0)
                },
                new MealModel
                {
                    MealSize = "Large",
                    MealType = "Dinner",
                    Description = "Cake?",
                    Satisfaction = 10,
                    RegisteredTime = new DateTime(year,month,day1, hour: 20, minute: 48, second:0)
                },

                // Day 2
                new MealModel
                {
                    MealSize = "Regular",
                    MealType = "Lunch",
                    Description = "Sandwich",
                    Satisfaction = 7,
                    RegisteredTime = new DateTime(year,month,day2, hour: 12, minute:45, second:0)
                },
                new MealModel
                {
                    MealSize = "Large",
                    MealType = "Dinner",
                    Description = "Pasta",
                    Satisfaction = 4,
                    RegisteredTime = new DateTime(year,month,day2, hour: 22, minute: 13, second:21)
                },

                // Day 3
                new MealModel
                {
                    MealSize = "Small",
                    MealType = "Breakfast",
                    Description = "Fruit",
                    Satisfaction = 5.3,
                    RegisteredTime = new DateTime(year,month,day3, hour: 8, minute:32, second:0)
                },
                new MealModel
                {
                    MealSize = "Large",
                    MealType = "Lunch",
                    Description = "Curry",
                    Satisfaction = 9.2,
                    RegisteredTime = new DateTime(year,month,day3, hour: 13, minute: 29, second:39)
                },
                new MealModel
                {
                    MealSize = "Large",
                    MealType = "Lunch",
                    Description = "BIG SOOUP",
                    Satisfaction = 9.2,
                    RegisteredTime = new DateTime(year,month,day3, hour: 18, minute: 19, second:39)
                },
            };
            
            Substances = new List<SubstanceModel>()
            {
                //Day 2
                new SubstanceModel()
                {
                    ConsumptionMethod= "Smoked",
                    SubstanceName = "Weed",
                    Amount = 0.1,
                    Unit = "g",
                    Satisfaction = 7.1,
                    RegisteredTime = new DateTime(year,month,day2, hour: 16, minute:20, second:0)
                }
            };

            Moods = new List<MoodModel>()
            {
                //Day 1
                new MoodModel()
                {
                    Description = "Some sad thing",
                    Emotions = new List<string>()
                    {
                        "Sad",
                        "Super sad",
                    },
                    OverallMood = 3.1,
                    RegisteredTime = new DateTime(year,month,day1, hour: 8, minute:20, second:0)
                },
                new MoodModel()
                {
                    Description = "Some mediocre thing",
                    Emotions = new List<string>()
                    {
                        "Meh",
                    },
                    OverallMood = 5.8,
                    RegisteredTime = new DateTime(year,month,day1, hour: 10, minute:15, second:0)
                },
                new MoodModel()
                {
                    OverallMood = 2,
                    RegisteredTime = new DateTime(year,month,day1, hour: 11, minute:41, second:0)
                },
                new MoodModel()
                {
                    Description = "Some good thing",
                    Emotions = new List<string>()
                    {
                        "Happy",
                    },
                    OverallMood = 8,
                    RegisteredTime = new DateTime(year,month,day1, hour: 14 , minute:11, second:0)
                },
                new MoodModel()
                {
                    OverallMood = 1.2,
                    RegisteredTime = new DateTime(year,month,day1, hour: 17 , minute:13, second:0)
                },

                //Day 2
                new MoodModel()
                {
                    Description = "Saw 5 cats",
                    Emotions = new List<string>()
                    {
                        "Ecstasy",
                    },
                    OverallMood = 9.9,
                    RegisteredTime = new DateTime(year,month,day2, hour: 7, minute:22, second:0)
                },
                new MoodModel()
                {
                    OverallMood = 2.1,
                    RegisteredTime = new DateTime(year,month,day2, hour: 13, minute:21, second:01)
                },
                new MoodModel()
                {
                    OverallMood = 4.8,
                    RegisteredTime = new DateTime(year,month,day1, hour: 15, minute:22, second:0)
                },
                new MoodModel()
                {
                    Description = "Saw 9 more cats",
                    Emotions = new List<string>()
                    {
                        "eeeeeeeeeeeeeeeee",
                    },
                    OverallMood = 10,
                    RegisteredTime = new DateTime(year,month,day1, hour: 17, minute:11, second:0)
                },
                new MoodModel()
                {
                    OverallMood = 0.08,
                    Description = "Was told ill never see cats again :(",
                    RegisteredTime = new DateTime(year,month,day1, hour: 18 , minute:13, second:0)
                }
            };

            Sleeps = new List<SleepModel>()
            {
                new SleepModel()
                {
                    SleepStart = new TimeSpan(23,0,0),
                    SleepStartDate = new DateTime(year,month, day1-1,23,0,0),
                    RegisteredTime = new DateTime(year,month,day1),

                    SleepEnd = new TimeSpan(8,0,0),
                    SleepEndDate = new DateTime(year, month,day1,8,0,0),
                    TotalSleep = 9,

                    Nightmare =true,
                    DreamLog="someone took all my cats away",
                    RestRating = 1.2
                },
                new SleepModel()
                {
                    SleepStart = new TimeSpan(23,59,59),
                    SleepStartDate = new DateTime(year,month, day1,23,59,59),
                    RegisteredTime = new DateTime(year,month,day2),

                    SleepEnd = new TimeSpan(8,0,0),
                    SleepEndDate = new DateTime(year, month,day2,8,0,0),
                    TotalSleep = 8,


                    DreamLog="someone gave my cats bak",
                    RestRating = 9.9
                }
            };

            Activities = new List<ActivityModel>()
            {
                new ActivityModel()
                {
                    Description = "2 many jumping jax",
                    StartTime = new TimeSpan(13,30,0 ),
                    EndTime = new TimeSpan(14,0,0),
                    Duration = 0.5,
                    Enjoyment = 2.0,
                    Exersice = true
                }
            };
        }

    }
}
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
    public static class LogSamples
    {
        private const int year = 2020;
        private static int month = DateTime.Now.Month;
        private static int day1 = DateTime.Now.Day-3;
        private static int day2 = day1 + 1;
        private static int day3 = day1 + 2;

        public static List<MealModel> GetMealSamples() => new List<MealModel>()
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

        public static List<SubstanceModel> GetSubstanceSamples() => new List<SubstanceModel>()
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

        public static List<MoodModel> GetMoodSamples() => new List<MoodModel>()
        {
            //Day 1
            new MoodModel()
            {
                Description = "Some sad thing",
                OverallMood = 3.1,
                StrongestEmotion = "super s ad sad",
                RegisteredTime = new DateTime(year,month,day1, hour: 8, minute:20, second:0)
            },
            new MoodModel()
            {
                Description = "Some mediocre thing",
                OverallMood = 5.8,
                StrongestEmotion = "anger",
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
                OverallMood = 8,
                RegisteredTime = new DateTime(year,month,day1, hour: 14 , minute:11, second:0)
            },
            new MoodModel()
            {
                Description = "asda",
                OverallMood = 1.2,
                StrongestEmotion = "sadness :)",
                RegisteredTime = new DateTime(year,month,day1, hour: 17 , minute:13, second:0)
            },

            //Day 2
            new MoodModel()
            {
                Description = "Saw 5 cats",
                OverallMood = 9.9,
                StrongestEmotion="bleeech",
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

        public static List<SleepModel> GetSleepSamples() => new List<SleepModel>()
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

        public static List<ActivityModel> GetActivitySamples() => new List<ActivityModel>()
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
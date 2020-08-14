using System.Collections.Generic;
using System;

namespace SelfMonitoringApp.Models
{
    public static class LogSamples
    {
        private const int year = 2020;
        private static readonly int month = DateTime.Now.Month;
        private static readonly int _day1 = DateTime.Now.Day;
        private static readonly int _day2 = _day1 + 1;
        private static readonly int _day3 = _day1 + 2;

        public static ActivityModel TestActivity => new ActivityModel()
        {
            ActivityName = "Hopscotch",
            StartTime = DateTime.Now,
            Duration = 1,
            Enjoyment = 8.2,
            Exersice = true
        };

        public static SubstanceModel TestSubstance => new SubstanceModel()
        {
            ConsumptionMethod = "Snorted",
            SubstanceName = "Coffeve",
            Comment = "tooo much coffeee",
            Amount = 2,
            Unit = "Pots",
            Satisfaction = 9.9,
        };

        public static MealModel TestMeal => new MealModel()
        {
            Description = "Cereal",
            MealType = "Breakfast",
            RegisteredTime = DateTime.Now,
            MealSize = "Normal",
            Satisfaction = 1.7,
        };

        public static MoodModel TestMood => new MoodModel()
        {
            Description = "Saw 5 dogoos in a row",
            OverallMood = 8.2,
            StrongestEmotion = "Joy",
            Location = "Home",
            RegisteredTime = DateTime.Now
        };

        public static SleepModel TestSleep => new SleepModel()
        {
            SleepStartDate = new DateTime(year, month, _day1, 23, 0, 0),
            RegisteredTime = new DateTime(year, month, _day2),

            SleepEndDate = new DateTime(year, month, _day2, 8, 0, 0),
            TotalSleep = 9,

            Nightmare = true,
            DreamLog = "someone took all my cats away",
            RestRating = 1.2
        };

        public static List<MealModel> GetMealSamples() => new List<MealModel>()
        {
            // Day 1
            new MealModel
            {
                MealSize = "Small",
                MealType = "Breakfast",
                Description = "Cereal",
                Satisfaction = 5.4,
                RegisteredTime = new DateTime(year,month,_day1, hour: 8, minute:30, second:0)
            },
            new MealModel
            {
                MealSize = "Regular",
                MealType = "Lunch",
                Description = "Sandwich",
                Satisfaction = 9,
                RegisteredTime = new DateTime(year,month,_day1, hour: 12, minute:45, second:0)
            },
            new MealModel
            {
                MealSize = "Small",
                MealType = "Snack",
                Description = "Chips",
                Satisfaction = 2,
                RegisteredTime = new DateTime(year,month,_day1, hour: 14, minute: 3, second:0)
            },
            new MealModel
            {
                MealSize = "Large",
                MealType = "Dinner",
                Description = "Cake?",
                Satisfaction = 10,
                RegisteredTime = new DateTime(year,month,_day1, hour: 20, minute: 48, second:0)
            },

            // Day 2
            new MealModel
            {
                MealSize = "Regular",
                MealType = "Lunch",
                Description = "Sandwich",
                Satisfaction = 7,
                RegisteredTime = new DateTime(year,month,_day2, hour: 12, minute:45, second:0)
            },
            new MealModel
            {
                MealSize = "Large",
                MealType = "Dinner",
                Description = "Pasta",
                Satisfaction = 4,
                RegisteredTime = new DateTime(year,month,_day2, hour: 22, minute: 13, second:21)
            },

            // Day 3
            new MealModel
            {
                MealSize = "Small",
                MealType = "Breakfast",
                Description = "Fruit",
                Satisfaction = 5.3,
                RegisteredTime = new DateTime(year,month,_day3, hour: 8, minute:32, second:0)
            },
            new MealModel
            {
                MealSize = "Large",
                MealType = "Lunch",
                Description = "Curry",
                Satisfaction = 9.2,
                RegisteredTime = new DateTime(year,month,_day3, hour: 13, minute: 29, second:39)
            },
            new MealModel
            {
                MealSize = "Large",
                MealType = "Lunch",
                Description = "BIG SOOUP",
                Satisfaction = 9.2,

                RegisteredTime = new DateTime(year,month,_day3, hour: 18, minute: 19, second:39)
            },
        };

        public static List<SubstanceModel> GetSubstanceSamples() => new List<SubstanceModel>()
        {
            //Day 1
            new SubstanceModel()
            {
                ConsumptionMethod= "Drank",
                SubstanceName = "Coffee",
                Amount = 1,
                Unit = "Cup",
                Satisfaction = 7.1,
                RegisteredTime = new DateTime(year,month,_day1, hour: 8, minute:20, second:0)
            },
            new SubstanceModel()
            {
                ConsumptionMethod= "Drank",
                SubstanceName = "Coffee",
                Amount = 1.5,
                Unit = "Cup",
                Satisfaction = 4,
                RegisteredTime = new DateTime(year,month,_day1, hour: 9, minute:23, second:0)
            },
            new SubstanceModel()
            {
                ConsumptionMethod= "Drank",
                SubstanceName = "Tea",
                Amount = 10,
                Unit = "cup",
                Satisfaction = 8,
                RegisteredTime = new DateTime(year,month,_day1, hour: 12, minute:20, second:0)
            },
            new SubstanceModel()
            {
                ConsumptionMethod= "Drank",
                SubstanceName = "Coffee",
                Amount = 1,
                Unit = "Cup",
                Satisfaction = 3,
                RegisteredTime = new DateTime(year,month,_day1, hour: 16, minute:20, second:0)
            },

            //Day 1
            new SubstanceModel()
            {
                ConsumptionMethod= "Drank",
                SubstanceName = "Coffee",
                Amount = 1,
                Unit = "Cup",
                Satisfaction = 7.1,
                RegisteredTime = new DateTime(year,month,_day1, hour: 8, minute:20, second:0)
            },
            new SubstanceModel()
            {
                ConsumptionMethod= "Drank",
                SubstanceName = "Coffee",
                Amount = 1.5,
                Unit = "Cup",
                Satisfaction = 4,
                RegisteredTime = new DateTime(year,month,_day1, hour: 9, minute:23, second:0)
            },
            new SubstanceModel()
            {
                ConsumptionMethod= "Drank",
                SubstanceName = "Tea",
                Amount = 10,
                Unit = "cup",
                Satisfaction = 8,
                RegisteredTime = new DateTime(year,month,_day1, hour: 12, minute:20, second:0)
            },
            new SubstanceModel()
            {
                ConsumptionMethod= "Drank",
                SubstanceName = "Coffee",
                Amount = 1,
                Unit = "Cup",
                Satisfaction = 3,
                RegisteredTime = new DateTime(year,month,_day1, hour: 16, minute:20, second:0)
            },
        };

        public static List<MoodModel> GetMoodSamples() => new List<MoodModel>()
        {
            //Day 1
            new MoodModel()
            {
                Description = "Blah blah blah blah blah blah",
                OverallMood = 7.1,
                StrongestEmotion = "Anger",
                Location = "Home",
                RegisteredTime = new DateTime(year,month,_day1, hour: 8, minute:20, second:0)
            },

            new MoodModel()
            {
                Description = "Blah blah blah blah blah blah",
                OverallMood = 5.8,
                StrongestEmotion = "anger",
                Location = "Pie Shop",
                RegisteredTime = new DateTime(year,month,_day1, hour: 10, minute:15, second:0)
            },
            new MoodModel()
            {
                OverallMood = 2,
                StrongestEmotion="saddness",
                RegisteredTime = new DateTime(year,month,_day1, hour: 11, minute:41, second:0)
            },
            new MoodModel()
            {
                Description = "Ill never see another pie again",
                OverallMood = 8,
                RegisteredTime = new DateTime(year,month,_day1, hour: 14 , minute:11, second:0)
            },
            new MoodModel()
            {
                OverallMood = 1.2,
                Location = "Home",
                StrongestEmotion = "Devestated",
                RegisteredTime = new DateTime(year,month,_day1, hour: 17 , minute:13, second:0)
            },

            //Day 2
            new MoodModel()
            {
                Description = "Saw 5 cats",
                OverallMood = 9.9,
                StrongestEmotion="bleeech",
                RegisteredTime = new DateTime(year,month,_day2, hour: 7, minute:22, second:0)
            },
            new MoodModel()
            {
                OverallMood = 2.1,
                RegisteredTime = new DateTime(year,month,_day2, hour: 13, minute:21, second:01)
            },
            new MoodModel()
            {
                OverallMood = 4.8,
                RegisteredTime = new DateTime(year,month,_day1, hour: 15, minute:22, second:0)
            },
            new MoodModel()
            {
                Description = "Saw 9 more cats",
                OverallMood = 10,
                RegisteredTime = new DateTime(year,month,_day1, hour: 17, minute:11, second:0)
            },
            new MoodModel()
            {
                OverallMood = 0.08,
                Description = "Was told ill never see cats again :(",
                RegisteredTime = new DateTime(year,month,_day1, hour: 18 , minute:13, second:0)
            }
        };

        public static List<SleepModel> GetSleepSamples() => new List<SleepModel>()
        {
            new SleepModel()
            {
                SleepStartDate = new DateTime(year,month, _day1,23,0,0),
                RegisteredTime = new DateTime(year,month,_day2),

                SleepEndDate = new DateTime(year, month,_day2,8,0,0),
                TotalSleep = 9,

                Nightmare =true,
                DreamLog="someone took all my cats away",
                RestRating = 1.2
            },  
            new SleepModel()
            {
                SleepStartDate = new DateTime(year,month, _day2,23,59,59),
                RegisteredTime = new DateTime(year,month,_day2),

                SleepEndDate = new DateTime(year, month,_day3,8,0,0),
                TotalSleep = 8,

                DreamLog="someone gave my cats bak",
                RestRating = 9.9
            }
        };

        public static List<ActivityModel> GetActivitySamples() => new List<ActivityModel>()
        {
            new ActivityModel()
            {
                ActivityName = "Painted a picture of a pie", 
                StartTime = new DateTime(year,month,_day1,7,30,15,0),
                WantedToStart = true,
                Duration = 1.4,
                Enjoyment = 8.1
            }
        };
    }
}
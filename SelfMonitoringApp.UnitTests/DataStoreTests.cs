using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SelfMonitoringApp.Models;
using SelfMonitoringApp.Services;
using System;
using System.Collections.Generic;


namespace SelfMonitoringApp.UnitTests
{
    [TestClass]
    public class DataStoreTests
    {

        [TestInitialize]
        public void Init()
        {
            DataStore.Initalize();
        }

        [TestMethod]
        public void TestLogSaving()
        {
            var mood = new MoodModel
            {
                Description = "was v sad",
                Emotions = new List<string>
                {
                    "sadddd",
                    "angry about the sad",
                    "sad about the angry",
                    "sangryyy>??"
                },
                OverallMood = 1.21,
                RegisteredTime = DateTime.Now
            };
            DataStore.AddModel(mood);

            var sleep = new SleepModel
            {
                SleepStart = new TimeSpan(1, 30, 0),
                SleepEnd = new TimeSpan(6, 46, 1),
                TotalSleep = 1.2,
                RememberedDream = true,
                DreamLog = "was attacked by logs all night, log after log after log. 5/5 would do again",

                RestRating = 7.1,
                RegisteredTime = DateTime.Now.AddMinutes(1)
            };
            DataStore.AddModel(sleep);

            var substance = new SubstanceModel
            {
                ConsumptionMethod = "Smoked",
                SubstanceName = "Weed",
                Amount = 0.1,
                Unit = "g",
                Comment = "blaze it",
                Satisfaction = 4.20,
                RegisteredTime = DateTime.Now.AddMinutes(2)
            };
            DataStore.AddModel(substance);
        }


        [TestCleanup]
        public void Cleanup()
        {

        }
    }
}

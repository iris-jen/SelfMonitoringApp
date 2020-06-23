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
            
        }

        [TestMethod]
        public void TestLogSaving()
        {
            var mood = new MoodModel
            {
                Description = "was v sad",
                OverallMood = 1.21,
                RegisteredTime = DateTime.Now
            };

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

        }


        [TestCleanup]
        public void Cleanup()
        {

        }
    }
}

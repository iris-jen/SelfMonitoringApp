using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting.Logging;
using Moq;
using SelfMonitoringApp.Models;
using SelfMonitoringApp.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace SelfMonitoringApp.UnitTests
{
    [TestClass]
    public class DataStoreTests
    {
        [TestInitialize]
        public async Task Init()
        {
            Logger.LogMessage("Adding samples to get the lazy initializer to go");
            await AddAllSampleLogs();
        }

        private async Task AddAllSampleLogs()
        {
            foreach (ActivityModel model in LogSamples.GetActivitySamples())
                await App.Database.AddOrModifyModelAsync(model);

            foreach (MealModel model in LogSamples.GetMealSamples())
                await App.Database.AddOrModifyModelAsync(model);

            foreach (MoodModel model in LogSamples.GetMoodSamples())
                await App.Database.AddOrModifyModelAsync(model);

            foreach (SleepModel model in LogSamples.GetSleepSamples())
                await App.Database.AddOrModifyModelAsync(model);

            foreach (SubstanceModel model in LogSamples.GetSubstanceSamples())
                await App.Database.AddOrModifyModelAsync(model);
        }

        [TestMethod]
        public async Task TestDatabase()
        {
        

        }


        [TestCleanup]
        public void Cleanup()
        {
            
        }
    }
}

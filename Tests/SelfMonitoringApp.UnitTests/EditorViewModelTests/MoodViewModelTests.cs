using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SelfMonitoringApp.Models;
using SelfMonitoringApp.Services;
using SelfMonitoringApp.Services.Navigation;
using SelfMonitoringApp.ViewModels;
using SelfMonitoringApp.ViewModels.Logs;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SelfMonitoringApp.UnitTests
{
    [TestClass]
    public class MoodViewModelTests
    {
        private MoodViewModel _newMoodModel;
        private MoodViewModel _existingMoodModel;


        [TestInitialize]
        public void Init()
        {
            var navService = new Mock<INavigationService>();
            var dbService = new Mock<IDatabaseService>();

            _newMoodModel = new MoodViewModel(nav: navService.Object, db: dbService.Object);
            _existingMoodModel = new MoodViewModel(existingModel: LogSamples.TestMood,
                nav: navService.Object, db: dbService.Object);
        }

        [TestMethod]
        public async Task TestSavingLog()
        {
            _newMoodModel.Description = _existingMoodModel.Description = "aaaaaaaaaaaaahhhhhhhh";
            _newMoodModel.Location = _existingMoodModel.Location = "hell probably";
            _newMoodModel.OverallMood = _existingMoodModel.OverallMood = 1.1;
            _newMoodModel.LogTime = _existingMoodModel.LogTime = DateTime.Now;
            _newMoodModel.StartTimeSpan = _existingMoodModel.StartTimeSpan = new TimeSpan(1, 2, 3);

            await Task.WhenAll
            ( 
                _newMoodModel.SaveAndPop(),  
                _existingMoodModel.SaveAndPop()
            );
        }

        [TestCleanup]
        public void Cleanup()
        {           
            _newMoodModel = _existingMoodModel = null;
        }
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SelfMonitoringApp.Models;
using SelfMonitoringApp.Navigation;
using SelfMonitoringApp.Services;
using SelfMonitoringApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SelfMonitoringApp.UnitTests
{
    [TestClass]
    public class MoodViewModelTests
    {
        private MoodViewModel _newMoodModel;
        private MoodViewModel _existingMoodModel;
        private MoodModel _existingMood = new MoodModel
        {
            Description = "was v sad",
            OverallMood = 4.20,
        };

        private INavigationService _navService;

        [TestInitialize]
        public void Init()
        {
            _navService = new Mock<INavigationService>().Object;          
            _newMoodModel = new MoodViewModel(_navService);
            _existingMoodModel = new MoodViewModel(_navService, _existingMood);
        }


        //todo: make good-er once i settle on how I'm storing data
        [TestMethod]
        public void TestSavingLog()
        {
            _newMoodModel.SaveAndPop();
            _existingMoodModel.SaveAndPop();
        }

        [TestCleanup]
        public void Cleanup()
        {
            _navService = null;
            _newMoodModel = null;
        }
    }
}

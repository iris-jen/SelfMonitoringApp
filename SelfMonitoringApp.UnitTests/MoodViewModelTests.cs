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
            Emotions = new List<string>
            {
                "sadddd",
                "angry about the sad",
                "sad about the angry",
                "sangryyy>??"
            },
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

        [TestMethod]
        public void TestEmotionAddRemoveLogic()
        {
            string emotionName = "some emotionnnn";
            _newMoodModel.NewEmotion = _existingMoodModel.NewEmotion = emotionName;
            _newMoodModel.OnAddNewEmotion();
            _existingMoodModel.OnAddNewEmotion();

            if (!_newMoodModel.Emotions.Contains(emotionName) || 
                !_existingMoodModel.Emotions.Contains(emotionName))
                throw new Exception("Could not find emotion in list after adding");

            _newMoodModel.SelectedEmotion = _existingMoodModel.SelectedEmotion = emotionName;
            _newMoodModel.OnRemoveEmotion();
            _existingMoodModel.OnRemoveEmotion();

            if (_newMoodModel.Emotions.Contains(emotionName) || _existingMoodModel.Emotions.Contains(emotionName))
                throw new Exception("Could not find emotion in list after adding");
        }

        [TestMethod]
        public void TestSavingLog()
        {
            DataStore.Initalize();
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

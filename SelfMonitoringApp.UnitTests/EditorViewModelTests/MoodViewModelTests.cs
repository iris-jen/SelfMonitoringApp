using Microsoft.VisualStudio.TestTools.UnitTesting;
using SelfMonitoringApp.Models;
using SelfMonitoringApp.Services;
using SelfMonitoringApp.Services.Navigation;
using SelfMonitoringApp.ViewModels;

using System.Threading.Tasks;

namespace SelfMonitoringApp.UnitTests
{
    [TestClass]
    public class MoodViewModelTests
    {
        private MoodViewModel _newMoodModel;
        private MoodViewModel _existingMoodModel;

        private INavigationService _navService;
        private IDatabaseService _dbService;

        [TestInitialize]
        public void Init()
        {
            _newMoodModel = new MoodViewModel();
            _existingMoodModel = new MoodViewModel(LogSamples.TestMood);
        }

        [TestMethod]
        public async Task TestSavingLog()
        {
            await _newMoodModel.SaveAndPop();
            await _existingMoodModel.SaveAndPop();
        }

        [TestCleanup]
        public void Cleanup()
        {
            _navService = null;
            _newMoodModel = null;
            _existingMoodModel = null;
        }
    }
}

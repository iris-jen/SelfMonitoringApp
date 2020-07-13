using Microsoft.VisualStudio.TestTools.UnitTesting;
using SelfMonitoringApp.ViewModels;
using System.Threading.Tasks;

namespace SelfMonitoringApp.UnitTests
{
    [TestClass]
    public class SleepViewModelTests
    {
        private SleepViewModel _newModel;
        private SleepViewModel _existingModel;

        [TestInitialize]
        public void Init()
        {
            _newModel = new SleepViewModel();
            //todo existing model
        }

        [TestMethod]
        public async Task TestSavingLog()
        {
            await _newModel.SaveAndPop();
        }

        [TestCleanup]
        public void Cleanup()
        {
            _newModel = null;
            _existingModel = null;
        }
    }
}

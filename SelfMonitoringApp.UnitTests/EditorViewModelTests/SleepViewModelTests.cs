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
    public class SleepViewModelTests
    {
        private SleepViewModel _newModel;
        private SleepViewModel _existingModel;

        private INavigationService _navService;

        [TestInitialize]
        public void Init()
        {
            _navService = new Mock<INavigationService>().Object;
            _newModel = new SleepViewModel(_navService);

            //todo existing model
        }

        [TestMethod]
        public async Task TestSavingLog()
        {
            await App.Database.InitializeAsync();
            await _newModel.SaveAndPop();

        }

        [TestCleanup]
        public void Cleanup()
        {
            _navService = null;
            _newModel = null;
            _existingModel = null;
        }
    }
}

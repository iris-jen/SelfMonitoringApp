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

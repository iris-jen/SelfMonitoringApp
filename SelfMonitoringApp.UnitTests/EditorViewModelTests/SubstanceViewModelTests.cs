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
    public class SubstanceViewModelTests
    {
        private SubstanceViewModel _newModel;
        private SubstanceViewModel _existingModel;

        private INavigationService _navService;

        [TestInitialize]
        public void Init()
        {
            _navService = new Mock<INavigationService>().Object;
            _newModel = new SubstanceViewModel(_navService);
            _existingModel = new SubstanceViewModel(_navService, LogSamples.TestSubstance);
        }

        [TestMethod]
        public async Task TestSavingLog()
        {
            await App.Database.InitializeAsync();
            await _newModel.SaveAndPop();
            await _existingModel.SaveAndPop();
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

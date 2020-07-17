using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SelfMonitoringApp.Models;
using SelfMonitoringApp.Services;
using SelfMonitoringApp.Services.Navigation;
using SelfMonitoringApp.ViewModels;
using SelfMonitoringApp.ViewModels.Logs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SelfMonitoringApp.UnitTests
{
    [TestClass]
    public class MealViewModelTests
    {
        private MealViewModel _newModel;
        private MealViewModel _existingModel;

        private INavigationService _navService;

        [TestInitialize]
        public void Init()
        {
            _newModel = new MealViewModel();
            _existingModel = new MealViewModel(LogSamples.TestMeal);
        }

        [TestMethod]
        public async Task TestSavingLog()
        {
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

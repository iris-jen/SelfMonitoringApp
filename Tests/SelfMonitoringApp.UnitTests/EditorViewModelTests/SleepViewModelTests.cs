using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SelfMonitoringApp.Models;
using SelfMonitoringApp.Services;
using SelfMonitoringApp.Services.Navigation;
using SelfMonitoringApp.ViewModels;
using SelfMonitoringApp.ViewModels.Logs;
using System;
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
            var navService = new Mock<INavigationService>();
            var dbService = new Mock<IDatabaseService>();

            _newModel = new SleepViewModel(nav: navService.Object, db: dbService.Object);
            _existingModel = new SleepViewModel(existingModel: LogSamples.TestSleep,
                nav: navService.Object, db: dbService.Object);
        }

        [TestMethod]
        public async Task TestSavingLog()
        {
            _newModel.DreamLog = _existingModel.DreamLog = "what a fcking nightmare";
            _newModel.RememberedDream = _existingModel.RememberedDream = true;
            _newModel.RestRating = _existingModel.RestRating = 1.2;
            _newModel.Nightmare = _existingModel.Nightmare = true;

            DateTime now = DateTime.Now;

            _newModel.SleepEndDate = _existingModel.SleepEndDate = now;
            _newModel.SleepEnd = _existingModel.SleepEnd = new TimeSpan(now.Hour, now.Minute, now.Second);

            double sleepAmmount = 8.2;

            DateTime then = now.Subtract(TimeSpan.FromHours(sleepAmmount));

            _newModel.SleepStartDate = _existingModel.SleepStartDate = then;
            _newModel.SleepStart = _existingModel.SleepStart = new TimeSpan(then.Hour, then.Minute, then.Second);

            if (_newModel.TotalSleep != sleepAmmount)
                throw new Exception("Computed sleep does not match input for new model");

            if(_existingModel.TotalSleep != sleepAmmount)
                throw new Exception("Computed sleep does not match input for existing model");

            await Task.WhenAll
            (
                _newModel.SaveAndPop(), 
                _existingModel.SaveAndPop()
            );
        }

        [TestCleanup]
        public void Cleanup()
        {
            _newModel = _existingModel = null;
        }
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SelfMonitoringApp.Models;
using SelfMonitoringApp.Services;
using SelfMonitoringApp.Services.Navigation;

using SelfMonitoringApp.ViewModels.Logs;
using System;
using System.Threading.Tasks;

namespace SelfMonitoringApp.UnitTests
{
    [TestClass]
    public class SubstanceViewModelTests
    {
        private SubstanceViewModel _newModel;
        private SubstanceViewModel _existingModel;

        [TestInitialize]
        public void Init()
        {
            var navService = new Mock<INavigationService>();
            var dbService = new Mock<IDatabaseService>();

            _newModel = new SubstanceViewModel(nav: navService.Object, db: dbService.Object);
            _existingModel = new SubstanceViewModel(existingModel: LogSamples.TestSubstance, 
                nav: navService.Object, db: dbService.Object);
        }

        [TestMethod]
        public async Task TestLogFunction()
        {
            #region General Property tests
            _newModel.LogTime = _existingModel.LogTime = DateTime.Now;
            _newModel.StartTimeSpan = _existingModel.StartTimeSpan = new TimeSpan(hours:1,minutes:2,seconds:11);
            _newModel.SubstanceName = _existingModel.SubstanceName = "Cheese";
            _newModel.ConsumptionMethod = _existingModel.ConsumptionMethod = "Crackers";
            _newModel.Unit = _existingModel.Unit = "kg";
            _newModel.Satisfaction = _existingModel.Satisfaction = 1.2;
            #endregion

            #region Stepper Test
            double amt = 33.8;
            _newModel.Ammount = _existingModel.Ammount = amt;
            _newModel.StepperOffset =_existingModel.StepperOffset += 1;

            if (_newModel.Ammount != amt + 1)
                throw new Exception("Incrementing new model stepper failed");

            if (_existingModel.Ammount != amt + 1)
                throw new Exception("Incrementing existing model stepper failed");

            _newModel.StepperOffset = _existingModel.StepperOffset -= 1;

            if (_newModel.Ammount != amt )
                throw new Exception("Decrementing new model stepper failed");

            if (_existingModel.Ammount != amt)
                throw new Exception("Decrementing existing model stepper failed");
            #endregion

            await _newModel.SaveAndPop();
            await _existingModel.SaveAndPop();
        }

        [TestCleanup]
        public void Cleanup()
        {
            _existingModel =_newModel = null;
        }
    }
}

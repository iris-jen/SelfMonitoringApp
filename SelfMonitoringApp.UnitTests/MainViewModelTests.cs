using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SelfMonitoringApp.Services.Navigation;
using SelfMonitoringApp.ViewModels;
using SelfMonitoringApp.ViewModels.Logs;
using System.Collections.Generic;

namespace SelfMonitoringApp.UnitTests
{
    [TestClass]
    public class MainViewModelTests
    {
        private MainViewModel _mainViewModel;
        private INavigationService _navService;

        [TestInitialize]
        public void Init()
        {
            _navService = new Mock<INavigationService>().Object;          
            _mainViewModel = new MainViewModel();
        }


        //todo - fix
        [TestMethod]
        public void NavigateAllTest()
        {
        }

        [TestCleanup]
        public void Cleanup()
        {
            _navService = null;
            _mainViewModel = null;
        }
    }
}

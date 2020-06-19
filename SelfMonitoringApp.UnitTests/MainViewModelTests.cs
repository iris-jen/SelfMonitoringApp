using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SelfMonitoringApp.Navigation;
using SelfMonitoringApp.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

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
            _mainViewModel = new MainViewModel(_navService);
        }

        [TestMethod]
        public void NavigateAllTest()
        {
            List<string> pageNames = new List<string>()
            {
                MoodViewModel.NavigationNodeName,
                SleepViewModel.NavigationNodeName,
                SubstanceViewModel.NavigationNodeName,
                SettingsViewModel.NavigationNodeName,
                MealViewModel.NavigationNodeName,
                DataExplorerViewModel.NavigationNodeName,
            };

            foreach (string page in pageNames)
                 _mainViewModel.Navigate(page);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _navService = null;
            _mainViewModel = null;
        }
    }
}

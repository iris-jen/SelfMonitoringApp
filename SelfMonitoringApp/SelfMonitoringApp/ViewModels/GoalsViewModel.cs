using SelfMonitoringApp.ViewModels.Base;

namespace SelfMonitoringApp.ViewModels
{
    public class GoalsViewModel : ViewModelBase, INavigationViewModel
    {
        public const string NavigationNodeName = "goals";

        public GoalsViewModel()
        {
        }
    }
}

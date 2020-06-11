using SelfMonitoringApp.Navigation;
using SelfMonitoringApp.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace SelfMonitoringApp.ViewModels
{
    public class SettingsViewModel : NavigatableViewModelBase, INavigationViewModel
    {
        public const string NavigationNodeName = "settings";

        public SettingsViewModel(INavigationService navService)
            :base(navService)
        {
                
        }
    }
}

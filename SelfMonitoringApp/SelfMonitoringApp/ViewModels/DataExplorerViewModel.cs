using SelfMonitoringApp.Navigation;
using SelfMonitoringApp.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace SelfMonitoringApp.ViewModels
{
    class DataExplorerViewModel : NavigatableViewModelBase, INavigationViewModel
    {
        public const string NavigationNodeName = "data";

        public DataExplorerViewModel(INavigationService navService) 
            : base(navService)
        {
                
        }
    }
}

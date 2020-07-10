using SelfMonitoringApp.Navigation;
using SelfMonitoringApp.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace SelfMonitoringApp.ViewModels
{
    public class HelpViewModel : ViewModelBase, INavigationViewModel
    {
        public const string NavigationNodeName = "help";
        public HelpViewModel()
        {

        }
    }
}

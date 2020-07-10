using SelfMonitoringApp.Models;
using SelfMonitoringApp.Navigation;
using SelfMonitoringApp.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace SelfMonitoringApp.ViewModels
{
    class NotificationsViewModel : ViewModelBase, INavigationViewModel
    {
        public const string NavigationNodeName = "notifications";

        public NotificationsViewModel(INavigationService navService, IList<NotificationModel> existingNotifications = null)  : 
            base (navService)
        {


        }
    }
}

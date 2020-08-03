using SelfMonitoringApp.Models;
using SelfMonitoringApp.ViewModels.Base;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace SelfMonitoringApp.ViewModels
{
    class NotificationsViewModel : ViewModelBase, INavigationViewModel
    {
        public const string NavigationNodeName = "notifications";

        public ObservableCollection<NotificationModel> Notifications;


        public Command AddNewNotification { get; set; }

        public NotificationsViewModel()
        {
        }
    }
}

using SelfMonitoringApp.Models;
using SelfMonitoringApp.Services;
using SelfMonitoringApp.ViewModels.Base;
using Splat;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace SelfMonitoringApp.ViewModels
{
    class NotificationsViewModel : ViewModelBase, INavigationViewModel
    {
        INotificationManagerService _notificationService;
        public ObservableCollection<NotificationModel> Notifications;

        private NotificationModel _selectedNotification;

        public NotificationModel SelectedNotification
        {
            get => _selectedNotification;
            set
            {
                if (_selectedNotification == value)
                    return;

                _selectedNotification = value;
                NotifyPropertyChanged();
            }
        }

        public Command AddNewNotificationCommand { get; set; }

        public Command DeleteNotificationCommand { get; set; }

        public NotificationsViewModel(INotificationManagerService notificationService = null)
        {
            Notifications = new ObservableCollection<NotificationModel>();

            if (notificationService is null)
            {
                _notificationService =
                    Locator.Current.GetService(typeof(INotificationManagerService)) as INotificationManagerService;
            }
            else
                _notificationService = notificationService;

            AddNewNotificationCommand = new Command(() => _navigator.NavigateTo(new NotificationEditorViewModel()));

        }

        public void DeleteNotification()
        {
            if (SelectedNotification is null)
                return;

            _notificationService.RemoveNotification(SelectedNotification);
        }
        
    }
}

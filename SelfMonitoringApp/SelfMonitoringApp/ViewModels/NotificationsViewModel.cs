using SelfMonitoringApp.Models;
using SelfMonitoringApp.Models.Base;
using SelfMonitoringApp.Services;
using SelfMonitoringApp.ViewModels.Base;
using Splat;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace SelfMonitoringApp.ViewModels
{
    class NotificationsViewModel : ViewModelBase, INavigationViewModel
    {
        INotificationManagerService _notificationService;
        public ObservableCollection<NotificationModel> Notifications { get; set; }

        private NotificationModel _selectedNotification;

        private NotificationEditorViewModel _notificationEditor;

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

            AddNewNotificationCommand = new Command(async() => await AddNewNotification());

        }

        private async Task AddNewNotification()
        {
            _notificationEditor = new NotificationEditorViewModel();
            _notificationEditor.ModelShed += _notificationEditor_ModelShed;
            await _navigator.NavigateTo(_notificationEditor);
        }

        private void _notificationEditor_ModelShed(object sender, EventArgs e)
        {
            IModel model = (e as ModelShedEventArgs).EventModel;
            NotificationModel newNotification = model as NotificationModel;

            int oldIndex = Notifications.IndexOf(x => x.ID == newNotification.ID);

            if (oldIndex == -1)
                Notifications.Add(newNotification);
            else
            {
                Notifications.Insert(oldIndex, newNotification);
                Notifications.RemoveAt(oldIndex + 1);
            }
        }

        public void DeleteNotification()
        {
            if (SelectedNotification is null)
                return;

            _notificationService.RemoveNotification(SelectedNotification);
        }
        
    }
}

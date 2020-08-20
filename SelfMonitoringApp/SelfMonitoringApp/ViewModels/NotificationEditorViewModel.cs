using SelfMonitoringApp.Models;
using SelfMonitoringApp.Models.Base;
using SelfMonitoringApp.Services;
using SelfMonitoringApp.Services.Navigation;
using SelfMonitoringApp.ViewModels.Base;
using Splat;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SelfMonitoringApp.ViewModels
{
    class NotificationEditorViewModel : ViewModelBase, INavigationViewModel
    {
        private NotificationModel _notification;
        private INotificationManagerService _notificationManager;
        public event EventHandler ModelShed;

        public Command SaveNotificationCommand { get; set; }

        public ObservableCollection<DateTime> ReminderTimes { get; set; }

        public NotificationType NotificationType
        {
            get => _notification.NotificationType;
            set
            {
                if (_notification.NotificationType == value)
                    return;

                _notification.NotificationType = value;
                NotifyPropertyChanged();
            }
        }

        public ModelType ModelTarget
        {
            get => _notification.ModelTarget;
            set
            {
                if (_notification.ModelTarget == value)
                    return;

                _notification.ModelTarget = value;
                NotifyPropertyChanged();
            }
        }

        private int _hour;
        public int Hour
        {
            get => _hour;
            set
            {
                if (_hour == value)
                    return;

                _hour = value;
                NotifyPropertyChanged();
            }
        }

        private int _minute;
        public int Minute
        {
            get => _minute;
            set
            {
                if (_minute == value)
                    return;

                _minute = value;
                NotifyPropertyChanged();
            }
        }

        public string Message
        {
            get => _notification.Message;
            set
            {
                if (_notification.Message == value)
                    return;

                _notification.Message = value;
                NotifyPropertyChanged();
            }
        }

        public bool Vibrate
        {
            get => _notification.Vibrate;
            set
            {
                if (_notification.Vibrate == value)
                    return;

                _notification.Vibrate = value;
                NotifyPropertyChanged();
            }
        }

        public bool OpenToPage
        {
            get => _notification.OpenToPage;
            set
            {
                if (_notification.OpenToPage == value)
                    return;

                _notification.OpenToPage = value;
                NotifyPropertyChanged();
            }
        }


        public bool Silent
        {
            get => _notification.Silent;
            set
            {
                if (_notification.Silent == value)
                    return;

                _notification.Silent = value;
                NotifyPropertyChanged();
            }
        }

        public TimeSpan StartTime
        {
            get => _notification.StartTime;
            set
            {
                if (_notification.StartTime == value)
                    return;

                _notification.StartTime = value;
                NotifyPropertyChanged();
            }
        }

        public TimeSpan EndTime
        {
            get => _notification.EndTime;
            set
            {
                if (_notification.EndTime == value)
                    return;

                _notification.EndTime = value;
                NotifyPropertyChanged();
            }
        }

        public NotificationEditorViewModel(NotificationModel notification = null,  INotificationManagerService notificationManager = null)
        {
            _notificationManager = notificationManager ?? 
                (INotificationManagerService)Locator.Current.GetService(typeof(INotificationManagerService));

            if (notification == null)
            {
                _notification = new NotificationModel();
                ReminderTimes = new ObservableCollection<DateTime>();



            }
            else
            {
                _notification = notification;
                ReminderTimes = new ObservableCollection<DateTime>(_notification.ReminderTimes);
            }

            SaveNotificationCommand = new Command(async () => await SaveAndPop());
        }

        public async Task SaveAndPop()
        {
            _notification.ReminderTimes = ReminderTimes.ToList();
            _notificationManager.AddOrUpdateNotification(_notification);
            await _navigator.NavigateBack();
        }
    }
}

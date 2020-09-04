using Newtonsoft.Json.Bson;
using SelfMonitoringApp.Models;
using SelfMonitoringApp.Models.Base;
using SelfMonitoringApp.Services;
using SelfMonitoringApp.Services.Navigation;
using SelfMonitoringApp.ViewModels.Base;
using Splat;
using System;
using System.Collections.Generic;
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
        public Command AddTimeCommand { get; set; }
        public Command RemoveTimeCommand { get; set; }

        public ObservableCollection<TimeSpan> ReminderTimes { get; set; }
        public List<ModelType> ModelTypes 
        {
            get
            {
                return new List<ModelType>()
                {
                    ModelType.Activity,
                    ModelType.Mood,
                    ModelType.Meal,
                    ModelType.Sleep,
                    ModelType.Substance,
                    ModelType.Socialization
                };
            }
        }

        public bool IsPeriodic
        {
            get => _notification.IsPeriodic;
            set
            {
                if (_notification.IsPeriodic == value)
                    return;

                _notification.IsPeriodic = value;
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

        private TimeSpan _newSingleTime;
        public TimeSpan NewSingleTime
        {
            get => _newSingleTime;
            set
            {
                if (_newSingleTime == value)
                    return;

                _newSingleTime = value;
                NotifyPropertyChanged();
            }
        }

        private TimeSpan _selectedSingleTime;
        public TimeSpan SelectedSingleTime
        {
            get => _selectedSingleTime;
            set
            {
                if (_selectedSingleTime == value)
                    return;

                _selectedSingleTime = value;
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
                ReminderTimes = new ObservableCollection<TimeSpan>();
            }
            else
            {
                _notification = notification;
                ReminderTimes = new ObservableCollection<TimeSpan>(_notification.ReminderTimes);
            }

            SaveNotificationCommand = new Command(async () => await SaveAndPop());
            AddTimeCommand = new Command(AddNewTime);
            RemoveTimeCommand = new Command(DeleteReminderTime);
        }

        public void AddNewTime()
        {
            if (!ReminderTimes.Contains(NewSingleTime))
                ReminderTimes.Add(NewSingleTime);
        }

        public void DeleteReminderTime()
        {
            ReminderTimes.Remove(SelectedSingleTime);
        }

        public async Task SaveAndPop()
        {
            _notification.Interval = new TimeSpan(Hour, Minute, 0);
            _notification.ReminderTimes = ReminderTimes.ToList();
            _notificationManager.AddOrUpdateNotification(_notification);
            await _navigator.NavigateBack();
            ModelShed?.Invoke(this, new ModelShedEventArgs(_notification));
        }

    }
}

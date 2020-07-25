using Acr.UserDialogs;
using SelfMonitoringApp.Models;
using SelfMonitoringApp.Models.Base;
using SelfMonitoringApp.Services;
using SelfMonitoringApp.ViewModels.Base;

using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SelfMonitoringApp.ViewModels.Logs
{
    public class ActivityViewModel : ViewModelBase, INavigationViewModel
    {
        private readonly ActivityModel _activity;
        public const string NavigationNodeName = "activity";
        public event EventHandler ModelShed;

        #region Bindings
        //Commands
        public Command SaveLogCommand { get; private set; }

        //General Notify
        public string ActivityName
        {
            get => _activity.ActivityName;
            set
            {
                if (_activity.ActivityName == value)
                    return;

                _activity.ActivityName = value;
                NotifyPropertyChanged();
            }
        }

        private TimeSpan _startTime;
        public TimeSpan StartTime
        {
            get => _startTime;
            set
            {
                if (_startTime == value)
                    return;

                _startTime = value;
                NotifyPropertyChanged();
                Duration = GetActivityLength();
            }
        }

        private TimeSpan _endTime;
        public TimeSpan EndTime
        {
            get =>  _endTime;
            set
            {
                if (_endTime == value)
                    return;

                _endTime = value;
                NotifyPropertyChanged();
                Duration = GetActivityLength();
            }
        }

        private DateTime _startDateTime;
        public DateTime StartDateTime
        {
            get => _startDateTime;
            set
            {
                if (_startDateTime == value)
                    return;

                _startDateTime = value;
                NotifyPropertyChanged();
                Duration = GetActivityLength();
            }
        }

        private DateTime _endDateTime;
        public DateTime EndDateTime
        {
            get => _endDateTime;
            set
            {
                if (_endDateTime == value)
                    return;

                _endDateTime = value;
                NotifyPropertyChanged();
                Duration = GetActivityLength();
            }
        }

        public double Duration
        {
            get => _activity.Duration;
            set
            {
                if (_activity.Duration == value)
                    return;

                _activity.Duration = value;
                NotifyPropertyChanged();
            }
        }

        public double Enjoyment
        {
            get => _activity.Enjoyment;
            set
            {
                if (_activity.Enjoyment == value)
                    return;

                _activity.Enjoyment = value;
                NotifyPropertyChanged();
            }
        }

        public bool Exersice
        {
            get => _activity.Exersice;
            set
            {
                if (_activity.Exersice == value)
                    return;

                _activity.Exersice = value;
                NotifyPropertyChanged();
            }
        }

        public bool WantedToStart
        {
            get => _activity.WantedToStart;
            set
            {
                if (_activity.WantedToStart == value)
                    return;

                _activity.WantedToStart = value;
                NotifyPropertyChanged();
            }
        }


        public string Comments
        {
            get => _activity.Comments;
            set
            {
                if (_activity.Comments == value)
                    return;

                _activity.Comments = value;
                NotifyPropertyChanged();
            }
        }

        #endregion

        public ActivityViewModel(IModel activityModel = null)
        {
            if (activityModel is null)
            {
                _activity = new ActivityModel();

                StartDateTime = DateTime.Now.AddMinutes(-30);
                EndDateTime = DateTime.Now;
                StartTime = new TimeSpan
                (
                    hours: StartDateTime.Hour,
                    minutes: StartDateTime.Minute,
                    seconds: StartDateTime.Second
                );
                EndTime = new TimeSpan
                (
                    hours: EndDateTime.Hour,
                    minutes: EndDateTime.Minute,
                    seconds: EndDateTime.Hour
                );

            }
            else
            {
                _activity = activityModel as ActivityModel;

                StartDateTime = _activity.StartTime;
                EndDateTime = _activity.EndTime;
                StartTime = new TimeSpan
                (
                    hours: _activity.StartTime.Hour,
                    minutes: _activity.StartTime.Minute,
                    seconds: _activity.StartTime.Second
                );
                EndTime = new TimeSpan
                (
                    hours: _activity.EndTime.Hour,
                    minutes: _activity.EndTime.Minute,
                    seconds: _activity.EndTime.Hour
                );
            }
            SaveLogCommand = new Command(async()=> await SaveAndPop());
        }

        public double GetActivityLength()
        {
            var startDateTime = new DateTime
            (
                year   : StartDateTime.Year,
                month  : StartDateTime.Month,
                day    : StartDateTime.Day,
                hour   : StartTime.Hours,
                minute : StartTime.Minutes,
                second : StartTime.Seconds
            );

            var endDateTime = new DateTime
            (
                year  : EndDateTime.Year,
                month : EndDateTime.Month,
                day   : EndDateTime.Day,
                hour  : EndTime.Hours,
                minute: EndTime.Minutes,
                second: EndTime.Seconds
            );

            _activity.EndTime   = _activity.RegisteredTime = endDateTime;
            _activity.StartTime = startDateTime;

            return endDateTime.Subtract(startDateTime).TotalHours;
        }

        public async Task SaveAndPop()
        {
            await _database.AddOrModifyModelAsync(_activity);
            await _navigator.NavigateBack();
            ModelShed?.Invoke(this, new ModelShedEventArgs(_activity));
        }
    }
}
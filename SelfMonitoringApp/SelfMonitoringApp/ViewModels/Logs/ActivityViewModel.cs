using SelfMonitoringApp.Models;
using SelfMonitoringApp.Models.Base;
using SelfMonitoringApp.ViewModels.Base;

using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SelfMonitoringApp.ViewModels.Logs
{
    public class ActivityViewModel : ViewModelBase, INavigationViewModel
    {
        private readonly ActivityModel _activity;

        public const string NavigationNodeName = "activity";
        public event EventHandler ModelShed;

        public Command SaveLogCommand { get; private set; }

        public string Description
        {
            get => _activity.Description;
            set
            {
                if (_activity.Description == value)
                    return;

                _activity.Description = value;
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

        public ActivityViewModel(IModel activityModel = null)
        {
            if (activityModel is null)
                _activity = new ActivityModel();
            else
            {
                _activity = activityModel as ActivityModel;

                StartDateTime = _activity.StartTime;
                EndDateTime = _activity.EndTime;
                StartTime = new TimeSpan(_activity.StartTime.Hour, _activity.StartTime.Minute, _activity.StartTime.Second);
            }

            SaveLogCommand = new Command(async()=> await SaveAndPop());
        }

        public IModel RegisterAndGetModel()
        {
            _activity.StartTime = new DateTime(StartDateTime.Year, StartDateTime.Month, StartDateTime.Day, StartTime.Hours, StartTime.Minutes, StartTime.Seconds);
            _activity.EndTime   = new DateTime(EndDateTime.Year, EndDateTime.Month, EndDateTime.Day, EndTime.Hours, EndTime.Minutes, EndTime.Seconds);
            _activity.Duration  = _activity.EndTime.Subtract(_activity.StartTime).TotalHours;
            return _activity;
        }

        public async Task SaveAndPop()
        {
            var model = RegisterAndGetModel();
            await _database.AddOrModifyModelAsync(model);
            await _navigator.NavigateBack();
            ModelShed?.Invoke(this, new ModelShedEventArgs(model));
        }
    }
}
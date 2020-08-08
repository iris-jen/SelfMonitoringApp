using SelfMonitoringApp.Models;
using SelfMonitoringApp.Models.Base;
using SelfMonitoringApp.Services;
using SelfMonitoringApp.Services.Navigation;
using SelfMonitoringApp.ViewModels.Base;

using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SelfMonitoringApp.ViewModels.Logs
{
    public class SocializationViewModel : ViewModelBase, INavigationViewModel
    {
        private readonly SocializationModel _social;
        public Command SaveLogCommand { get; private set; }
        public event EventHandler ModelShed;

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
                Duration = GetLength();
            }
        }

        private TimeSpan _endTime;
        public TimeSpan EndTime
        {
            get => _endTime;
            set
            {
                if (_endTime == value)
                    return;

                _endTime = value;
                NotifyPropertyChanged();
                Duration = GetLength();
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
                Duration = GetLength();
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
                Duration = GetLength();
            }
        }

        public double Duration
        {
            get => _social.Duration;
            set
            {
                if (_social.Duration == value)
                    return;

                _social.Duration = value;
                NotifyPropertyChanged();
            }
        }

        public double Enjoyment
        {
            get => _social.Enjoyment;
            set
            {
                if (_social.Enjoyment == value)
                    return;

                _social.Enjoyment = value;
                NotifyPropertyChanged();
            }
        }

        public string Comments
        {
            get => _social.Comments;
            set
            {
                if (_social.Comments == value)
                    return;

                _social.Comments = value;
                NotifyPropertyChanged();
            }
        }

        public string SocializationType
        {
            get => _social.SocializationType;
            set
            {
                if (_social.SocializationType == value)
                    return;

                _social.SocializationType = value;
                NotifyPropertyChanged();
            }
        }

        public bool WantedToSocialize
        {
            get => _social.WantedToSocialize;
            set
            {
                if (_social.WantedToSocialize == value)
                    return;

                _social.WantedToSocialize = value;
                NotifyPropertyChanged();
            }
        }


        public SocializationViewModel(IModel existingModel = null, INavigationService nav = null, IDatabaseService db = null)
            : base(nav, db)
        {
            if (existingModel is null)
            {
                _social = new SocializationModel()
                {
                    Enjoyment = 5
                };

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
                _social = existingModel as SocializationModel;

                _startDateTime = _social.StartTime;
                _endDateTime = _social.EndTime;
                _startTime = new TimeSpan
                (
                    hours: _social.StartTime.Hour,
                    minutes: _social.StartTime.Minute,
                    seconds: _social.StartTime.Second
                );
                _endTime = new TimeSpan
                (
                    hours: _social.EndTime.Hour,
                    minutes: _social.EndTime.Minute,
                    seconds: _social.EndTime.Hour
                );

            }
            SaveLogCommand = new Command(async () => await SaveAndPop());
        }
        public double GetLength()
        {
            var startDateTime = new DateTime
            (
                year: StartDateTime.Year,
                month: StartDateTime.Month,
                day: StartDateTime.Day,
                hour: StartTime.Hours,
                minute: StartTime.Minutes,
                second: StartTime.Seconds
            );

            var endDateTime = new DateTime
            (
                year: EndDateTime.Year,
                month: EndDateTime.Month,
                day: EndDateTime.Day,
                hour: EndTime.Hours,
                minute: EndTime.Minutes,
                second: EndTime.Seconds
            );

            _social.EndTime = _social.RegisteredTime = endDateTime;
            _social.StartTime = startDateTime;

            return endDateTime.Subtract(startDateTime).TotalHours;
        }
        
        public async Task SaveAndPop()
        {
            Duration = GetLength();

            await _database.AddOrModifyModelAsync(_social);
            await _navigator.NavigateBack();
            ModelShed?.Invoke(this, new ModelShedEventArgs(_social));
        }
    }
}

using SelfMonitoringApp.Models;
using SelfMonitoringApp.Models.Base;
using SelfMonitoringApp.ViewModels.Base;

using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SelfMonitoringApp.ViewModels.Logs
{
    public class MoodViewModel : ViewModelBase, INavigationViewModel
    {
        private readonly MoodModel _mood;
        public const string NavigationNodeName = "mood";
        public event EventHandler ModelShed;

        #region Bindings
        //Commands
        public Command SaveLogCommand { get; private set; }

        //General Notify
        private DateTime _logTime;
        public DateTime LogTime
        {
            get => _logTime;
            set
            {
                if (_logTime == value)
                    return;

                _logTime = value;
                NotifyPropertyChanged();
            }
        }

        private TimeSpan _startTimeSpan;
        public TimeSpan StartTimeSpan
        {
            get => _startTimeSpan;
            set
            {
                if (_startTimeSpan == value)
                    return;

                _startTimeSpan = value;
                NotifyPropertyChanged();
            }
        }

        public string Description
        {
            get => _mood.Description;
            set
            {
                if (_mood.Description == value)
                    return;

                _mood.Description = value;
                NotifyPropertyChanged();
            }
        }

        public double OverallMood
        {
            get => _mood.OverallMood;
            set
            {
                if (_mood.OverallMood == value)
                    return;

                _mood.OverallMood = value;
                NotifyPropertyChanged();
            }
        }

        public string Location
        {
            get => _mood.Location;
            set
            {
                if (_mood.Location ==value)
                    return;

                _mood.Location = value;
                NotifyPropertyChanged();
            }
        }

        public string StrongestEmotion
        {
            get => _mood.StrongestEmotion;
            set
            {
                if (_mood.StrongestEmotion == value)
                    return;

                _mood.StrongestEmotion = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        public MoodViewModel(IModel existingModel = null)
        {
            if (existingModel is null) // New Log
            {
                _mood = new MoodModel() { OverallMood = 5.0 };
                LogTime = DateTime.Now;
                StartTimeSpan = new TimeSpan
                (
                    hours  : LogTime.Hour,
                    minutes: LogTime.Minute,
                    seconds: LogTime.Second
                );
            }
            else // Editing existing log
            {
                _mood = existingModel as MoodModel;
                LogTime = _mood.RegisteredTime;
                StartTimeSpan = new TimeSpan
                (
                    hours  : _mood.RegisteredTime.Hour,
                    minutes: _mood.RegisteredTime.Minute,
                    seconds:_mood.RegisteredTime.Second
                );
            }
            SaveLogCommand = new Command(async () => await SaveAndPop());
        }

        public async Task SaveAndPop()
        {
            _mood.RegisteredTime = new DateTime
            (
               year   : LogTime.Year,
               month  : LogTime.Month,
               day    : LogTime.Day,
               hour   : StartTimeSpan.Hours,
               minute : StartTimeSpan.Minutes,
               second : StartTimeSpan.Seconds
            );

            await _database.AddOrModifyModelAsync(_mood);
            await _navigator.NavigateBack();
            ModelShed?.Invoke(this, new ModelShedEventArgs(_mood));
        }
    }
}
using SelfMonitoringApp.Models;
using SelfMonitoringApp.Models.Base;
using SelfMonitoringApp.ViewModels.Base;

using System;
using System.Threading.Tasks;
using Xamarin.Forms;


namespace SelfMonitoringApp.ViewModels
{
    public class MoodViewModel : ViewModelBase, INavigationViewModel
    {
        private readonly MoodModel _mood;
        public const string NavigationNodeName = "mood";
        public Command SaveLogCommand { get; private set; }
        public event EventHandler ModelShed;

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
                if (_mood.Location == value)
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

        public MoodViewModel(IModel existingModel = null)
        {
            if (existingModel is null)
            {
                _mood = new MoodModel();
                LogTime = DateTime.Now;
                StartTimeSpan = new TimeSpan(LogTime.Hour, LogTime.Hour, LogTime.Second);
            }
            else
            {
                _mood = existingModel as MoodModel;
                LogTime = new DateTime(_mood.RegisteredTime.Year, _mood.RegisteredTime.Month, _mood.RegisteredTime.Day);
                StartTimeSpan = new TimeSpan(_mood.RegisteredTime.Hour, _mood.RegisteredTime.Minute, _mood.RegisteredTime.Second);
            }

            SaveLogCommand = new Command(async () => await SaveAndPop());
        }

        public IModel RegisterAndGetModel()
        {
            _mood.RegisteredTime = new DateTime(LogTime.Year, LogTime.Month, LogTime.Day,
                StartTimeSpan.Hours, StartTimeSpan.Minutes, StartTimeSpan.Seconds);

            return _mood;
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
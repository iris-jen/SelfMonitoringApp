using SelfMonitoringApp.Models;
using SelfMonitoringApp.Models.Base;
using SelfMonitoringApp.Services;
using SelfMonitoringApp.ViewModels.Base;

using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Xamarin.Forms;
using System.Collections.Generic;

namespace SelfMonitoringApp.ViewModels.Logs
{
    public class MoodViewModel : ViewModelBase, INavigationViewModel
    {
        private readonly MoodModel _mood;
        public const string NavigationNodeName = "mood";

        public event EventHandler ModelShed;

        #region Bindings
        public Command SaveLogCommand { get; private set; }
        public Command<SuggestionTypes> AddSuggestionCommand { get; private set; }

        private List<string> _emotions;

        public ObservableCollection<SuggestionModel> Emotions { get; private set; }

        public ObservableCollection<string> Locations { get; private set; }

        private DateTime _logTime;
        /// <summary>
        /// User selected date this occurred
        /// </summary>
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
        /// <summary>
        /// User selected hour this occurred
        /// </summary>
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

        private SuggestionModel _strongestEmotion;

        public SuggestionModel StrongestEmotion
        {
            get => _strongestEmotion;
            set
            {
                if (_strongestEmotion == value)
                    return;

                _strongestEmotion = value;
                NotifyPropertyChanged();
            }
        }


        #endregion

        public MoodViewModel(IModel existingModel = null)
        {
            Emotions = _suggestions.GetSuggestionCollection(SuggestionTypes.Emotions);

            if (existingModel is null)
            {
                // This is a fresh log, create one and set some defaults
                _mood = new MoodModel();
                LogTime = DateTime.Now;
                StartTimeSpan = new TimeSpan
                (
                    hours: LogTime.Hour,
                    minutes: LogTime.Minute,
                    seconds: LogTime.Second
                );
            }
            else
            {
                _mood = existingModel as MoodModel;
                LogTime = _mood.RegisteredTime;
                StartTimeSpan = new TimeSpan
                (
                    _mood.RegisteredTime.Hour,
                    _mood.RegisteredTime.Minute,
                    _mood.RegisteredTime.Second
                );
            }

            AddSuggestionCommand = new Command<SuggestionTypes>(async(type)=> await AddSuggestion(type));
            SaveLogCommand = new Command(async () => await SaveAndPop());
        }

        public IModel RegisterAndGetModel()
        {
            _mood.RegisteredTime = new DateTime
            (
                year: LogTime.Year,
                month: LogTime.Month,
                day: LogTime.Day,
                hour: StartTimeSpan.Hours,
                minute: StartTimeSpan.Minutes,
                second: StartTimeSpan.Seconds
            );
            _mood.StrongestEmotion = StrongestEmotion.SuggestionText;
            return _mood;
        }

        public async Task SaveAndPop()
        {
            var model = RegisterAndGetModel();
            await _database.AddOrModifyModelAsync(model);
            await _navigator.NavigateBack();
            ModelShed?.Invoke(this, new ModelShedEventArgs(model));
        }

        public async Task AddSuggestion(SuggestionTypes type)
        {
            var promptResult = await UserDialogs.Instance.PromptAsync("Enter a picker value");

            if (!promptResult.Ok)
                return;

            _suggestions.AddSuggestion(type,  promptResult.Text);
            
            switch(type)
            {
                case SuggestionTypes.Emotions:
                    var newSug = new SuggestionModel() { SuggestionText = promptResult.Text };

                    Emotions.Add(newSug);

                    await Task.Delay(50);

                    StrongestEmotion = newSug;
                    break;
                case SuggestionTypes.Locations:
                    Locations.Add(promptResult.Text);
                    break;
            }
        }
    }
}
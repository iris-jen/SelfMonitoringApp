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
using Plugin.Media.Abstractions;

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
        public Command<SuggestionTypes> AddSuggestionCommand { get; private set; }
        public Command<SuggestionTypes> RemoveSuggestionCommand { get; private set; }


        //Collections
        public ObservableCollection<string> Emotions { get; private set; }
        public ObservableCollection<string> Locations { get; private set; }

        //General Notify
        private bool _removeSuggestionEnabled;
        public bool RemoveSuggestionEnabled
        {
            get =>  _removeSuggestionEnabled;
            set
            {
                if ( _removeSuggestionEnabled == value)
                    return;

                 _removeSuggestionEnabled = value;
                NotifyPropertyChanged();
            }
        }

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

        private int _selectedLocation;
        public int SelectedLocation
        {
            get => _selectedLocation;
            set
            {
                if (value == -1)
                    return;

                _selectedLocation = value;
                _mood.Location = Locations[value];
                NotifyPropertyChanged();
            }
        }

        private int _strongestEmotion;
        public int StrongestEmotion
        {
            get => _strongestEmotion;
            set
            {
                if (value == -1)
                    return;

                _strongestEmotion = value;
               _mood.StrongestEmotion = Emotions[value];
                NotifyPropertyChanged();
            }
        }
        #endregion

        public MoodViewModel(IModel existingModel = null)
        {
            Emotions = _suggestions.GetSuggestionCollection(SuggestionTypes.Emotions);
            Locations = _suggestions.GetSuggestionCollection(SuggestionTypes.Locations);

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

                SelectedLocation = Locations.IndexOf(_mood.Location);
                StrongestEmotion = Emotions.IndexOf(_mood.StrongestEmotion);
            }

            AddSuggestionCommand = new Command<SuggestionTypes>(async(type)=> await AddSuggestion(type));
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

        public async Task AddSuggestion(SuggestionTypes type)
        {
            var promptResult = await UserDialogs.Instance.PromptAsync("Enter a picker value");

            if (!promptResult.Ok)
                return;

            _suggestions.AddSuggestion(type,  promptResult.Text);
            switch(type)
            {
                case SuggestionTypes.Emotions:
                    var newSug = promptResult.Text;
                    Emotions.Add(newSug);
                    StrongestEmotion = Emotions.IndexOf(newSug);
                    break;
                case SuggestionTypes.Locations:
                    newSug = promptResult.Text;
                    Locations.Add(newSug);
                    SelectedLocation = Locations.IndexOf(newSug);
                    break;
            }
        }


        public async Task RemoveSuggestion(SuggestionTypes type)
        {
            var promptResult = await UserDialogs.Instance.PromptAsync("Enter a picker value");

            if (!promptResult.Ok)
                return;

            _suggestions.AddSuggestion(type, promptResult.Text);
            switch (type)
            {
                case SuggestionTypes.Emotions:
                    var newSug = promptResult.Text;
                    Emotions.Add(newSug);
                    StrongestEmotion = Emotions.IndexOf(newSug);
                    break;
                case SuggestionTypes.Locations:
                    newSug = promptResult.Text;
                    Locations.Add(newSug);
                    SelectedLocation = Locations.IndexOf(newSug);
                    break;
            }
        }
    }
}
using SelfMonitoringApp.Models;
using SelfMonitoringApp.Navigation;
using SelfMonitoringApp.ViewModels.Base;

using System;
using System.Collections.ObjectModel;
using System.Linq;

using Xamarin.Forms;


namespace SelfMonitoringApp.ViewModels
{
    public class MoodViewModel : NavigatableViewModelBase, INavigationViewModel
    {
        private readonly MoodModel _mood;
        public const string NavigationNodeName = "mood";

        public ObservableCollection<string> Emotions { get; private set; }

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

        private string _newEmotion;
        public string NewEmotion
        {
            get => _newEmotion;
            set
            {
                if (_newEmotion == value)
                    return;

                _newEmotion = value;
                NotifyPropertyChanged();
            }
        }

        private string _selectedEmotion;
        public string SelectedEmotion
        {
            get => _selectedEmotion;
            set
            {
                if (_selectedEmotion == value)
                    return;

                _selectedEmotion = value;
                NotifyPropertyChanged();
            }
        }

        public double OveralMood
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

        public Command AddNewEmotion { get; private set; }
        public Command RemoveSelectedCommand { get; private set; }
        public Command SaveLogCommand { get; private set; }

        /// <summary>
        /// Constructor for loading an existing mood log;
        /// </summary>
        /// <param name="existingModel">a log created in the past</param>
        public MoodViewModel(INavigationService navService, IModel existingModel = null) : base(navService)
        {
            if (existingModel is null)
            {
                _mood = new MoodModel();
                Emotions = new ObservableCollection<string>();
            }
            else
            {
                _mood = existingModel as MoodModel;
                Emotions = new ObservableCollection<string>(_mood.Emotions);
            }

            SaveLogCommand = new Command(SaveAndPop);
            AddNewEmotion = new Command(OnAddNewEmotion);
            RemoveSelectedCommand = new Command(OnRemoveEmotion);
        }

        public void OnAddNewEmotion()
        {
            if (NewEmotion != string.Empty)
            {
                Emotions.Add(NewEmotion);
                NewEmotion = string.Empty;
            }
        }

        public void OnRemoveEmotion()
        {
            if (!string.IsNullOrEmpty(SelectedEmotion))
            {
                var emotion = Emotions.FirstOrDefault(x => x.Equals(SelectedEmotion));
                if(emotion != null)
                {
                    Emotions.Remove(emotion);
                }
            }
        }

        /// <summary>
        /// Get the view models model
        /// </summary>
        /// <returns></returns>
        public IModel RegisterAndGetModel()
        {
            _mood.RegisteredTime = DateTime.Now;
            _mood.Emotions = new System.Collections.Generic.List<string>(Emotions);
            return _mood;
        }

        public void SaveAndPop()
        {
            DataStore.AddModel(RegisterAndGetModel());
            _navigator.NavigateBack();
        }
    }
}
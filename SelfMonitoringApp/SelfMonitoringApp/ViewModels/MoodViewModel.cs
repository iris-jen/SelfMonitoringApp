using SelfMonitoringApp.Models;
using SelfMonitoringApp.Navigation;
using SelfMonitoringApp.ViewModels.Base;

using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.PlatformConfiguration;

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

            AddNewEmotion = new Command(OnAddNewEmotion);
            RemoveSelectedCommand = new Command(OnRemoveEmotion);
        }

        private void OnAddNewEmotion()
        {
            if (NewEmotion != string.Empty)
            {
                Emotions.Add(NewEmotion);
            }
        }

        private void OnRemoveEmotion()
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
            return _mood;
        }
    }
}
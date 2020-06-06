using SelfMonitoringApp.Models;
using SelfMonitoringApp.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SelfMonitoringApp.ViewModels
{
    public class MoodDetailViewModel : ExtendedBindableObject , ILogViewModel
    {
        private readonly MoodModel _mood;
        
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

        /// <summary>
        /// Constructor for creating a new mood log
        /// </summary>
        public MoodDetailViewModel()
        {
            _mood = new MoodModel();
            Emotions = new ObservableCollection<string>();
        }

        /// <summary>
        /// Constructor for loading an existing mood log;
        /// </summary>
        /// <param name="existingModel">a log created in the past</param>
        public MoodDetailViewModel(IModel existingModel)
        {
            _mood = existingModel as MoodModel;
            Emotions = new ObservableCollection<string>(_mood.Emotions);
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

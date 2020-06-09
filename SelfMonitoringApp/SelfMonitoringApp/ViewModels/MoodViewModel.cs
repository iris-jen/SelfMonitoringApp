using SelfMonitoringApp.Models;
using SelfMonitoringApp.Navigation;
using SelfMonitoringApp.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

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

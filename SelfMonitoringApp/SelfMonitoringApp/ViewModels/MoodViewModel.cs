using SelfMonitoringApp.Models;
using SelfMonitoringApp.Navigation;
using SelfMonitoringApp.Services;
using SelfMonitoringApp.ViewModels.Base;

using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
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
            }
            else
            {
                _mood = existingModel as MoodModel;
            }

            SaveLogCommand = new Command(async () => await SaveAndPop());
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

        public async Task SaveAndPop()
        {
            await App.Database.AddOrModifyModelAsync(RegisterAndGetModel());
            await _navigator.NavigateBack();
        }
    }
}
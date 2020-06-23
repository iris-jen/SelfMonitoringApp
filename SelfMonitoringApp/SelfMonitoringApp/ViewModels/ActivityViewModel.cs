using SelfMonitoringApp.Models;
using SelfMonitoringApp.Navigation;
using SelfMonitoringApp.Services;
using SelfMonitoringApp.ViewModels.Base;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SelfMonitoringApp.ViewModels
{
    class ActivityViewModel : NavigatableViewModelBase, INavigationViewModel
    {
        private readonly ActivityModel _activity;

        public const string NavigationNodeName = "activity";
        public Command SaveLogCommand { get; private set; }

        public string Description
        {
            get => _activity.Description;
            set
            {
                if (_activity.Description == value)
                    return;

                _activity.Description = value;
                NotifyPropertyChanged();
            }
        }

        public TimeSpan StartTime
        {
            get => _activity.StartTime;
            set
            {
                if (_activity.StartTime == value)
                    return;

                _activity.StartTime = value;
                NotifyPropertyChanged();
            }
        }


        public TimeSpan EndTime
        {
            get => _activity.EndTime;
            set
            {
                if (_activity.EndTime == value)
                    return;

                _activity.EndTime = value;
                NotifyPropertyChanged();
            }
        }

        public double Duration
        {
            get => _activity.Duration;
            set
            {
                if (_activity.Duration == value)
                    return;

                _activity.Duration = value;
                NotifyPropertyChanged();
            }
        }

        public double Enjoyment
        {
            get => _activity.Enjoyment;
            set
            {
                if (_activity.Enjoyment == value)
                    return;

                _activity.Enjoyment = value;
                NotifyPropertyChanged();
            }
        }

        public bool Exersice
        {
            get => _activity.Exersice;
            set
            {
                if (_activity.Exersice == value)
                    return;

                _activity.Exersice = value;
                NotifyPropertyChanged();
            }
        }

        public ActivityViewModel(INavigationService navService, IModel activityModel = null) : base(navService)
        {
            if (activityModel is null)
                _activity = new ActivityModel();
            else
                _activity = activityModel as ActivityModel;

            SaveLogCommand = new Command(async()=> await SaveAndPop());
        }

        public IModel RegisterAndGetModel()
        {
            _activity.RegisteredTime = DateTime.Now;
            return _activity;
        }

        public async Task SaveAndPop()
        {
            await App.Database.AddOrModifyActivityAsync(_activity);
            await _navigator.NavigateBack();
        }
    }
}

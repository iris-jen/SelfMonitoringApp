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
        private readonly bool _editing;

        public const string NavigationNodeName = "activity";
        public event EventHandler ModelShed;

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

        private TimeSpan _startTime;
        public TimeSpan StartTime
        {
            get => _startTime;
            set
            {
                if (_startTime == value)
                    return;

                _startTime = value;
                NotifyPropertyChanged();
            }
        }

        private TimeSpan _endTime;
        public TimeSpan EndTime
        {
            get =>  _endTime;
            set
            {
                if (_endTime == value)
                    return;

                _endTime = value;
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


        public bool WantedToStart
        {
            get => _activity.WantedToStart;
            set
            {
                if (_activity.WantedToStart == value)
                    return;

                _activity.WantedToStart = value;
                NotifyPropertyChanged();
            }
        }


        public ActivityViewModel(INavigationService navService, IModel activityModel = null) : base(navService)
        {
            if (activityModel is null)
                _activity = new ActivityModel();
            else
            {
                _activity = activityModel as ActivityModel;
                _editing = true;
            }

            SaveLogCommand = new Command(async()=> await SaveAndPop());
        }

        public IModel RegisterAndGetModel()
        {
            var now = DateTime.Now;
            
            if(!_editing)
                _activity.RegisteredTime = DateTime.Now;

            _activity.StartTime = new DateTime(now.Year, now.Month, now.Day, StartTime.Hours, StartTime.Minutes, StartTime.Seconds);
            var endDateTime = new DateTime(now.Year, now.Month, now.Day, EndTime.Hours, EndTime.Minutes, EndTime.Seconds);
            _activity.Duration = endDateTime.Subtract(_activity.StartTime).TotalHours;
            return _activity;
        }

        public async Task SaveAndPop()
        {
            var model = RegisterAndGetModel();
            await App.Database.AddOrModifyModelAsync(model);
            await _navigator.NavigateBack();
            ModelShed?.Invoke(this, new ModelShedEventArgs(model));
        }
    }
}

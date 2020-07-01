using SelfMonitoringApp.Models;
using SelfMonitoringApp.Navigation;
using SelfMonitoringApp.Services;
using SelfMonitoringApp.ViewModels.Base;

using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SelfMonitoringApp.ViewModels
{
    public class SleepViewModel : NavigatableViewModelBase, INavigationViewModel
    {
        private readonly SleepModel _sleepModel;
        public const string NavigationNodeName = "sleep";
        public Command SaveLogCommand { get; private set; }

        public TimeSpan SleepStart
        {
            get => _sleepModel.SleepStart;
            set
            {
                if (_sleepModel.SleepStart == value)
                    return;

                _sleepModel.SleepStart = value;
                TotalSleep = GetSleep();
                NotifyPropertyChanged();
            }
        }

        public double TotalSleep
        {
            get => _sleepModel.TotalSleep;
            set
            {
                if (_sleepModel.TotalSleep == value)
                    return;

                _sleepModel.TotalSleep = value;
                NotifyPropertyChanged();
            }
        }

        public TimeSpan SleepEnd
        {
            get => _sleepModel.SleepEnd;
            set
            {
                if (_sleepModel.SleepEnd == value)
                    return;

                _sleepModel.SleepEnd = value;
                TotalSleep = GetSleep();
                NotifyPropertyChanged();
            }
        }

        private DateTime _sleepEndDate = DateTime.Now;
        public DateTime SleepEndDate
        {
            get => _sleepEndDate;
            set
            {
                if (_sleepEndDate == value)
                    return;

                _sleepEndDate = value;
                TotalSleep = GetSleep();
                NotifyPropertyChanged();
            }
        }

        private DateTime _sleepStartDate = DateTime.Now.AddDays(-1);
        public DateTime SleepStartDate
        {
            get => _sleepStartDate;
            set
            {
                if (_sleepStartDate == value)
                    return;

                _sleepStartDate = value;
                TotalSleep = GetSleep();
                NotifyPropertyChanged();
            }
        }

        public bool RememberedDream
        {
            get => _sleepModel.RememberedDream;
            set
            {
                if (_sleepModel.RememberedDream == value)
                    return;

                _sleepModel.RememberedDream = value;
                NotifyPropertyChanged();
            }
        }

        public bool Nightmare
        {
            get => _sleepModel.Nightmare;
            set
            {
                if (_sleepModel.Nightmare == value)
                    return;

                _sleepModel.Nightmare = value;
                NotifyPropertyChanged();
            }
        }

        public string DreamLog
        {
            get => _sleepModel.DreamLog;
            set
            {
                if (_sleepModel.DreamLog == value)
                    return;

                _sleepModel.DreamLog = value;
                NotifyPropertyChanged();
            }
        }

        public double RestRating
        {
            get => _sleepModel.RestRating;
            set
            {
                if (_sleepModel.RestRating == value)
                    return;

                _sleepModel.RestRating = value;
                NotifyPropertyChanged();
            }
        }

        public SleepViewModel(INavigationService navService, IModel existingModel = null) : 
            base(navService)
        {
            if (existingModel is null)
                _sleepModel = new SleepModel();
            else
                _sleepModel = existingModel as SleepModel;

            SaveLogCommand = new Command(async () => await SaveAndPop());
        }

        public IModel RegisterAndGetModel()
        {
            _sleepModel.RegisteredTime = DateTime.Now;
            return _sleepModel;
        }

        public double GetSleep()
        {
            var startDateTime = new DateTime(
                year: SleepStartDate.Year, 
                month: SleepStartDate.Month, 
                day: SleepStartDate.Day,
                hour: SleepStart.Hours,
                minute: SleepStart.Minutes,
                second: SleepStart.Seconds
                );

            var endDateTime = new DateTime(
                year: SleepEndDate.Year,
                month: SleepEndDate.Month,
                day: SleepEndDate.Day,
                hour: SleepEnd.Hours,
                minute: SleepEnd.Minutes,
                second: SleepEnd.Seconds
                );

            _sleepModel.SleepEndDate = endDateTime;
            _sleepModel.SleepStartDate = startDateTime;

            return endDateTime.Subtract(startDateTime).TotalHours;
        }

        public async Task SaveAndPop()
        {
            await App.Database.AddOrModifyModelAsync(RegisterAndGetModel());
            await _navigator.NavigateBack();
        }
    }
}
using SelfMonitoringApp.Models;
using SelfMonitoringApp.Navigation;
using SelfMonitoringApp.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace SelfMonitoringApp.ViewModels
{
    public class SleepViewModel : NavigatableViewModelBase, INavigationViewModel
    {
        private readonly SleepModel _sleepModel;
        public const string NavigationNodeName = "sleep";

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

        public double GetSleep()
        {
            var sleep = SleepEnd.Subtract(SleepStart);
            return sleep.TotalHours-1;
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

        public bool VividDream
        {
            get => _sleepModel.VividDream;
            set
            {
                if (_sleepModel.VividDream == value)
                    return;

                _sleepModel.VividDream = value;
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
        }

        public IModel RegisterAndGetModel()
        {
            _sleepModel.RegisteredTime = DateTime.Now;
            return _sleepModel;
        }
    }
}

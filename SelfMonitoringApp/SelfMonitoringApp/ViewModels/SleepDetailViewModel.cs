using SelfMonitoringApp.Models;
using SelfMonitoringApp.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace SelfMonitoringApp.ViewModels
{
    public class SleepDetailViewModel : ExtendedBindableObject, ILogViewModel
    {
        private readonly SleepModel _sleepModel;

        public TimeSpan SleepStart
        {
            get => _sleepModel.SleepStart;
            set
            {
                if (_sleepModel.SleepStart == value)
                    return;

                _sleepModel.SleepStart = value;
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

        public SleepDetailViewModel()
        {
            _sleepModel = new SleepModel();
        }

        public SleepDetailViewModel(IModel existingModel)
        {
            _sleepModel = existingModel as SleepModel;
        }

        public IModel RegisterAndGetModel()
        {
            _sleepModel.RegisteredTime = DateTime.Now;
            return _sleepModel;
        }
    }
}

using SelfMonitoringApp.Models;
using SelfMonitoringApp.Models.Base;
using SelfMonitoringApp.ViewModels.Base;

using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SelfMonitoringApp.ViewModels.Logs
{
    public class SubstanceViewModel: ViewModelBase, INavigationViewModel
    {
        private readonly SubstanceModel _substance;
        public const string NavigationNodeName = "substance";

        public Command SaveLogCommand { get; private set; }
        public event EventHandler ModelShed;

        private DateTime _logTime;
        public DateTime LogTime
        {
            get => _logTime;
            set
            {
                if (_logTime == value)
                    return;

                _logTime = value;
                NotifyPropertyChanged();
            }
        }

        private TimeSpan _startTimeSpan;
        public TimeSpan StartTimeSpan
        {
            get => _startTimeSpan;
            set
            {
                if (_startTimeSpan == value)
                    return;

                _startTimeSpan = value;
                NotifyPropertyChanged();
            }
        }

        public string ConsumptionMethod
        {
            get => _substance.ConsumptionMethod;
            set
            {
                if (_substance.ConsumptionMethod == value)
                    return;

                _substance.ConsumptionMethod = value;
                NotifyPropertyChanged();
            }
        }

        public string SubstanceName
        {
            get => _substance.SubstanceName;
            set
            {
                if (_substance.SubstanceName == value)
                    return;

                _substance.SubstanceName = value;
                NotifyPropertyChanged();
            }
        }

        public string Comment
        {
            get => _substance.Comment;
            set
            {
                if (_substance.Comment == value)
                    return;

                _substance.Comment = value;
                NotifyPropertyChanged();
            }
        }

        public double Ammount
        {
            get => _substance.Amount;
            set
            {
                if (_substance.Amount == value)
                    return;

                _substance.Amount = value;
                NotifyPropertyChanged();
            }
        }

        public string Unit
        {
            get => _substance.Unit;
            set
            {
                if (_substance.Unit == value)
                    return;

                _substance.Unit = value;
                NotifyPropertyChanged();
            }
        }

        public double Satisfaction
        {
            get => _substance.Satisfaction;
            set
            {
                if (_substance.Satisfaction == value)
                    return;

                _substance.Satisfaction = value;
                NotifyPropertyChanged();
            }
        }

        public SubstanceViewModel(IModel existingModel = null) 
        {
            if (existingModel is null)
            {
                _substance = new SubstanceModel();
                LogTime = DateTime.Now;
                StartTimeSpan = new TimeSpan(LogTime.Hour, LogTime.Hour, LogTime.Second);
            }
            else
            {
                _substance = existingModel as SubstanceModel;
                LogTime = new DateTime(_substance.RegisteredTime.Year, _substance.RegisteredTime.Month, _substance.RegisteredTime.Day);
                StartTimeSpan = new TimeSpan(_substance.RegisteredTime.Hour, _substance.RegisteredTime.Minute, _substance.RegisteredTime.Second);
            }
   
            SaveLogCommand = new Command(async ()=> await SaveAndPop());
        }

        public IModel RegisterAndGetModel()
        {
            _substance.RegisteredTime = new DateTime(LogTime.Year, LogTime.Month, LogTime.Day,
                StartTimeSpan.Hours, StartTimeSpan.Minutes, StartTimeSpan.Seconds);
            return _substance;
        }

        public async Task SaveAndPop()
        {
            var model = RegisterAndGetModel();

            await _database.AddOrModifyModelAsync(model);
            await _navigator.NavigateBack();
            ModelShed?.Invoke(this, new ModelShedEventArgs(model));
        }
    }
}

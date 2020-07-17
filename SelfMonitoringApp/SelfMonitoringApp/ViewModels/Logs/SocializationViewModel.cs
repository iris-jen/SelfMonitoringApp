using SelfMonitoringApp.Models;
using SelfMonitoringApp.Models.Base;
using SelfMonitoringApp.ViewModels.Base;

using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SelfMonitoringApp.ViewModels.Logs
{
    public class SocalizationViewModel : ViewModelBase, INavigationViewModel
    {
        private readonly SocializationModel _social;
        public const string NavigationNodeName = "social";

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

        public SocalizationViewModel(IModel existingModel = null) 
        {
            if (existingModel is null)
            {
                _social = new SocializationModel();
                LogTime = DateTime.Now;
                StartTimeSpan = new TimeSpan(LogTime.Hour, LogTime.Hour, LogTime.Second);
            }
            else
            {
                _social = existingModel as SocializationModel;
                LogTime = new DateTime(_social.RegisteredTime.Year, _social.RegisteredTime.Month, _social.RegisteredTime.Day);
                StartTimeSpan = new TimeSpan(_social.RegisteredTime.Hour, _social.RegisteredTime.Minute, _social.RegisteredTime.Second);
            }
   
            SaveLogCommand = new Command(async ()=> await SaveAndPop());
        }

        public IModel RegisterAndGetModel()
        {
            _social.RegisteredTime = new DateTime(LogTime.Year, LogTime.Month, LogTime.Day,
                StartTimeSpan.Hours, StartTimeSpan.Minutes, StartTimeSpan.Seconds);
            return _social;
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

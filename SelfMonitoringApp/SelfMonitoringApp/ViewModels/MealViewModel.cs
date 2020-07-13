using SelfMonitoringApp.Models;
using SelfMonitoringApp.Models.Base;
using SelfMonitoringApp.ViewModels.Base;

using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SelfMonitoringApp.ViewModels
{
    public class MealViewModel: ViewModelBase, INavigationViewModel
    {
        private readonly MealModel _mealModel;

        public const string NavigationNodeName = "meal";
        public event EventHandler ModelShed;

        public Command SaveLogCommand { get; private set; }

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

        public string MealSize
        {
            get => _mealModel.MealSize;
            set
            {
                if (_mealModel.MealSize == value)
                    return;

                _mealModel.MealSize = value;
                NotifyPropertyChanged();
            }
        }

        public string MealType
        {
            get => _mealModel.MealType;
            set
            {
                if (_mealModel.MealType == value)
                    return;

                _mealModel.MealType = value;
                NotifyPropertyChanged();
            }
        }

        public string Description
        {
            get => _mealModel.Description;
            set
            {
                if (_mealModel.Description == value)
                    return;

                _mealModel.Description = value;
                NotifyPropertyChanged();
            }
        }

        public double Satisfaction
        {
            get => _mealModel.Satisfaction;
            set
            {
                if (_mealModel.Satisfaction == value)
                    return;

                _mealModel.Satisfaction = value;
                NotifyPropertyChanged();
            }
        }

        public MealViewModel(IModel existingMeal = null)
        {
            if (existingMeal is null)
                _mealModel = new MealModel();
            else
            {
                _mealModel = existingMeal as MealModel;
                LogTime = new DateTime(_mealModel.RegisteredTime.Year, _mealModel.RegisteredTime.Month, _mealModel.RegisteredTime.Day);
                StartTimeSpan = new TimeSpan(_mealModel.RegisteredTime.Hour, _mealModel.RegisteredTime.Minute, _mealModel.RegisteredTime.Second);
            }

            SaveLogCommand = new Command(async () => await SaveAndPop());
        }

        public IModel RegisterAndGetModel()
        {
            _mealModel.RegisteredTime = new DateTime(LogTime.Year, LogTime.Month, LogTime.Day,
                StartTimeSpan.Hours, StartTimeSpan.Minutes, StartTimeSpan.Seconds);

            return _mealModel;
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

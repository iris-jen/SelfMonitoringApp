using Acr.UserDialogs;
using SelfMonitoringApp.Models;
using SelfMonitoringApp.Models.Base;
using SelfMonitoringApp.Services;
using SelfMonitoringApp.ViewModels.Base;
    
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SelfMonitoringApp.ViewModels.Logs
{
    public class MealViewModel : ViewModelBase, INavigationViewModel
    {
        private readonly MealModel _mealModel;
        public const string NavigationNodeName = "meal";
        public event EventHandler ModelShed;

        #region Bindings
        
        //Commands
        public Command SaveLogCommand { get; private set; }
        public Command<SuggestionTypes> AddSuggestionCommand { get; private set; }

        //Collections
        public ObservableCollection<string> MealTypes { get; private set; }
        public ObservableCollection<string> MealSizes { get; private set; }
        public ObservableCollection<string> MealNames { get; private set; }

        //General Notify
        private DateTime _logDateTime;
        public DateTime LogDateTime
        {
            get => _logDateTime;
            set
            {
                if (_logDateTime == value)
                    return;

                _logDateTime = value;
                NotifyPropertyChanged();
            }
        }

        private TimeSpan _logTimeSpan;
        public TimeSpan LogTimeSpan
        {
            get => _logTimeSpan;
            set
            {
                if (_logTimeSpan == value)
                    return;

                _logTimeSpan = value;
                NotifyPropertyChanged();
            }
        }

        private int _mealSizeSelection;
        public int MealSizeSelection
        {
            get => _mealSizeSelection;
            set
            {
                if (value == -1)
                    return;

                _mealSizeSelection = value;
                _mealModel.MealSize = MealSizes[value];
                NotifyPropertyChanged();
            }
        }

        private int _mealTypeSelection;
        public int MealTypeSelection
        {
            get => _mealTypeSelection;
            set
            {
                if (value == -1)
                    return;

                _mealTypeSelection = value;
                _mealModel.MealType = MealTypes[value];
                NotifyPropertyChanged();
            }
        }

        private int _mealNameSelection;
        public int MealNameSelection
        {
            get => _mealNameSelection;
            set
            {
                if (value ==-1)
                    return;

                _mealNameSelection = value;
                _mealModel.Description = MealNames[value];
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
        #endregion

        public MealViewModel(IModel existingMeal = null)
        {
            MealTypes = _suggestions.GetSuggestionCollection(SuggestionTypes.MealTypes);
            MealSizes = _suggestions.GetSuggestionCollection(SuggestionTypes.MealSizes);
            MealNames = _suggestions.GetSuggestionCollection(SuggestionTypes.MealNames);

            if (existingMeal is null)
            {
                _mealModel = new MealModel();
                LogDateTime = DateTime.Now;
                LogTimeSpan = new TimeSpan
                (
                    hours   : LogDateTime.Hour,
                    minutes : LogDateTime.Minute,
                    seconds : LogDateTime.Second
                );
            }
            else
            {
                _mealModel = existingMeal as MealModel;
                LogDateTime = _mealModel.RegisteredTime;
                LogTimeSpan = new TimeSpan
                (
                    hours    : _mealModel.RegisteredTime.Hour,
                    minutes  : _mealModel.RegisteredTime.Minute,
                    seconds  : _mealModel.RegisteredTime.Second
                );

                MealNameSelection = MealNames.IndexOf(_mealModel.Description);
                MealSizeSelection = MealSizes.IndexOf(_mealModel.MealSize);
                MealTypeSelection = MealTypes.IndexOf(_mealModel.MealType);
            }

            AddSuggestionCommand = new Command<SuggestionTypes>(async (type) => await AddSuggestion(type));
            SaveLogCommand = new Command(async () => await SaveAndPop());
        }

        public async Task SaveAndPop()
        {
            _mealModel.RegisteredTime = new DateTime
            (
                year   : LogDateTime.Year,
                month  : LogDateTime.Month,
                day    : LogDateTime.Day,
                hour   : LogTimeSpan.Hours,
                minute : LogTimeSpan.Minutes,  
                second : LogTimeSpan.Seconds
            ) ;

            await _database.AddOrModifyModelAsync(_mealModel);
            await _navigator.NavigateBack();
            ModelShed?.Invoke(this, new ModelShedEventArgs(_mealModel));
        }

        public async Task AddSuggestion(SuggestionTypes type)
        {
            var promptResult = await UserDialogs.Instance.PromptAsync("Enter a value");

            if (!promptResult.Ok)
                return;

            _suggestions.AddSuggestion(type, promptResult.Text);
            switch (type)
            {
                case SuggestionTypes.MealTypes:
                    var newSug = promptResult.Text;
                    MealTypes.Add(newSug);
                    MealTypeSelection = MealTypes.IndexOf(newSug);
                    break;
                case SuggestionTypes.MealNames:
                    newSug = promptResult.Text;
                    MealNames.Add(newSug);
                    MealNameSelection = MealNames.IndexOf(newSug);
                    break;
                case SuggestionTypes.MealSizes:
                    newSug = promptResult.Text;
                    MealSizes.Add(newSug);
                    MealSizeSelection = MealSizes.IndexOf(newSug);
                    break;
            }
        }
    }
}

using Acr.UserDialogs;
using SelfMonitoringApp.Models;
using SelfMonitoringApp.Models.Base;
using SelfMonitoringApp.Services;
using SelfMonitoringApp.ViewModels.Base;

using System;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SelfMonitoringApp.ViewModels.Logs
{
    public class SubstanceViewModel: ViewModelBase, INavigationViewModel
    {
        private readonly SubstanceModel _substance;
        public const string NavigationNodeName = "substance";
        public event EventHandler ModelShed;

        #region Bindings

        //Commands
        public Command SaveLogCommand { get; private set; }
        public Command<SuggestionTypes> AddSuggestionCommand { get; private set; }
        public Command<SuggestionTypes> RemoveSuggestionCommand { get; private set; }

        //Collections
        public ObservableCollection<string> SubstanceNames              { get; private set; }
        public ObservableCollection<string> SubstanceConsumptionMethods { get; private set; }
        public ObservableCollection<string> Units                       { get; private set; }
        public ObservableCollection<string> Locations                   { get; private set; }

        //General Notify
        private bool _removeSuggestionEnabled;
        public bool RemoveSuggestionEnabled
        {
            get => _removeSuggestionEnabled;
            set
            {
                if (_removeSuggestionEnabled == value)
                    return;

                _removeSuggestionEnabled = value;
                NotifyPropertyChanged();
            }
        }

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

        private TimeSpan _logTimeSpan;
        public TimeSpan StartTimeSpan
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


        //idk why xamarin's doing this, but trying to bind the SelectedItem
        //on the picker to a string wont allow me to set the picker when I'm restoring it?
        //only setting the selected index works?

        private int _selectedConsumptionMethod;
        public int SelectedConsumptionMethod
        {
            get => _selectedConsumptionMethod;
            set
            {
                if (value == -1)
                    return;

                _selectedConsumptionMethod = value;
                _substance.ConsumptionMethod = SubstanceConsumptionMethods[value];
                NotifyPropertyChanged();
            }
        }

        private int _selectedSubstanceName;
        public int SelectedSubstanceName
        {
            get => _selectedSubstanceName;
            set
            {
                if (value == -1)
                    return;

                _selectedSubstanceName = value;
                _substance.SubstanceName = SubstanceNames[value];
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

        private int _unitSelection;
        public int UnitSelection
        {
            get => _unitSelection;
            set
            {
                if (value == -1)
                    return;

                _unitSelection = value;
                _substance.Unit = Units[value];
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
        #endregion

        public SubstanceViewModel(IModel existingModel = null) 
        {
            if (existingModel is null)
            {
                _substance = new SubstanceModel();
                LogTime = DateTime.Now;
                StartTimeSpan = new TimeSpan
                (
                    hours   : LogTime.Hour, 
                    minutes : LogTime.Hour, 
                    seconds : LogTime.Second
                );
            }
            else
            {
                _substance = existingModel as SubstanceModel;
                LogTime = _substance.RegisteredTime;
                StartTimeSpan = new TimeSpan
                (
                    hours   : _substance.RegisteredTime.Hour, 
                    minutes : _substance.RegisteredTime.Minute, 
                    seconds : _substance.RegisteredTime.Second
                );
            }   
            SaveLogCommand = new Command(async ()=> await SaveAndPop());
            AddSuggestionCommand = new Command<SuggestionTypes>(async (type) => await AddSuggestion(type));
        }

        public async Task AddSuggestion(SuggestionTypes type)
        {
            var promptResult = await UserDialogs.Instance.PromptAsync("Enter a value");

            if (!promptResult.Ok)
                return;

            _suggestions.AddSuggestion(type, promptResult.Text);
            switch (type)
            {
                case SuggestionTypes.Units:
                    var newSug = promptResult.Text;
                    Units.Add(newSug);
                    UnitSelection = Units.IndexOf(newSug);
                    break;
                case SuggestionTypes.Locations:
                    newSug = promptResult.Text;
                    Locations.Add(newSug);
                    break;
            }
        }


        public async Task SaveAndPop()
        {
            _substance.RegisteredTime = new DateTime
            (
                year   : LogTime.Year,
                month  : LogTime.Month,
                day    : LogTime.Day,
                hour   : StartTimeSpan.Hours,
                minute : StartTimeSpan.Minutes,
                second : StartTimeSpan.Seconds
            );

            await _database.AddOrModifyModelAsync(_substance);
            await _navigator.NavigateBack();
            ModelShed?.Invoke(this, new ModelShedEventArgs(_substance));
        }
    }
}
using SelfMonitoringApp.Models;
using SelfMonitoringApp.Navigation;
using SelfMonitoringApp.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace SelfMonitoringApp.ViewModels
{
    public class DaySummaryViewModel : NavigatableViewModelBase, INavigationViewModel
    {
        public ObservableCollection<MealModel> Meals { get; private set; }
        public ObservableCollection<SubstanceModel> Substances { get; private set; }
        public ObservableCollection<ActivityModel> Activities { get; private set; }
        public ObservableCollection<MoodModel> Moods { get; private set; }
        public ObservableCollection<SleepModel> Sleeps { get; private set; }

        private MoodViewModel _moodEditor;
        private MealViewModel _mealEditor;
        private SubstanceViewModel _substanceEditor;
        private SleepViewModel _sleepEditor;
        private ActivityViewModel _activityEditor;

        public string Date { get; private set; }

        private bool _mealsVisibility;
        public bool MealsVisibility
        {
            get => _mealsVisibility;
            set
            {
                if (_mealsVisibility == value)
                    return;

                _mealsVisibility = value;
                NotifyPropertyChanged();
            }
        }

        private bool _substancesVisibility;
        public bool SubstanceVisibility
        {
            get => _substancesVisibility;
            set
            {
                if (_substancesVisibility == value)
                    return;

                _substancesVisibility = value;
                NotifyPropertyChanged();
            }
        }

        private bool _activitiesVisibility;
        public bool ActivitiesVisibility
        {
            get => _activitiesVisibility;
            set
            {
                if (_activitiesVisibility == value)
                    return;

                _activitiesVisibility = value;
                NotifyPropertyChanged();
            }
        }

        private bool _moodsVisibility;
        public bool MoodsVisibility
        {
            get => _moodsVisibility;
            set
            {
                if (_moodsVisibility == value)
                    return;

                _moodsVisibility = value;
                NotifyPropertyChanged();
            }
        }

        private bool _sleepsVisibility;
        public bool SleepsVisibility
        {
            get => _sleepsVisibility;
            set
            {
                if (_sleepsVisibility == value)
                    return;

                _sleepsVisibility = value;
                NotifyPropertyChanged();
            }
        }

        public bool SleepsLogged { get; private set; }
        public bool MealsLogged { get; private set; }
        public bool ActivitiesLogged { get; private set; }
        public bool MoodsLogged { get; private set; }
        public bool SubstancesLogged { get; private set; }


        private MealModel _selectedMeal;
        public MealModel SelectedMeal
        {
            get => _selectedMeal;
            set
            {
                if (_selectedMeal == value)
                    return;

                _selectedMeal = value;
                NotifyPropertyChanged();
            }
        }

        private SubstanceModel _selectedSubstance;
        public SubstanceModel SelectedSubstance
        {
            get => _selectedSubstance;
            set
            {
                if (_selectedSubstance == value)
                    return;

                _selectedSubstance = value;
                NotifyPropertyChanged();
            }
        }

        private ActivityModel _selectedActivity;
        public ActivityModel SelectedActivity
        {
            get => _selectedActivity;
            set
            {
                if (_selectedActivity == value)
                    return;

                _selectedActivity = value;
                NotifyPropertyChanged();
            }
        }

        private MoodModel _selectedMood;
        public MoodModel SelectedMood
        {
            get => _selectedMood;
            set
            {
                if (_selectedMood == value)
                    return;

                _selectedMood = value;
                NotifyPropertyChanged();
            }
        }

        private SleepModel _selectedSleep;
        public SleepModel SelectedSleep
        {
            get => _selectedSleep;
            set
            {
                if (_selectedSleep == value)
                    return;

                _selectedSleep = value;
                NotifyPropertyChanged();
            }
        }

        // Show commands for specific categories
        public Command<ModelType> ShowCategoryComand { get; private set; }

        // Edit / Delete commands for selected objects
        public Command<ModelType> EditSelectedCommand { get; private set; }
        public Command<ModelType> DeleteSelectedCommand { get; private set; }

        public DaySummaryViewModel(List<SleepModel> sleeps, List<ActivityModel> activities,
            List<MealModel> meals, List<MoodModel> moods, List<SubstanceModel> substances, DateTime date, INavigationService navService) : base(navService)
        {
            Meals = new ObservableCollection<MealModel>(meals);
            Substances = new ObservableCollection<SubstanceModel>(substances);
            Activities = new ObservableCollection<ActivityModel>(activities);
            Moods = new ObservableCollection<MoodModel>(moods);
            Sleeps = new ObservableCollection<SleepModel>(sleeps);

            ShowCategoryComand = new Command<ModelType>((type) => SetVisibility(type));
            EditSelectedCommand = new Command<ModelType>(async(type) => await EditSelected(type));
            DeleteSelectedCommand = new Command<ModelType>((type) => DeleteSelected(type));

            MoodsVisibility = true;
            
            Date = $"{date.DayOfWeek}: {date.Year}-{date.Month}-{date.Day}";
        }


        private void SetVisibility(ModelType type)
        {
            switch (type)
            {
                case ModelType.Activity:
                    ActivitiesVisibility = true;
                    MealsVisibility = MoodsVisibility = SleepsVisibility = SubstanceVisibility = false;
                    break;
                case ModelType.Meal:
                    MealsVisibility = true;
                    ActivitiesVisibility = MoodsVisibility = SleepsVisibility = SubstanceVisibility = false;
                    break;
                case ModelType.Mood:
                    MoodsVisibility = true;
                    ActivitiesVisibility = MealsVisibility = SleepsVisibility = SubstanceVisibility = false;
                    break;
                case ModelType.Sleep: 
                    SleepsVisibility = true;
                    ActivitiesVisibility = MealsVisibility = MoodsVisibility = SubstanceVisibility = false;
                    break;
                case ModelType.Substance:
                    SubstanceVisibility = true;
                    ActivitiesVisibility = MealsVisibility = MoodsVisibility = SleepsVisibility = false;
                    break;
            }
        }

        private async Task EditSelected(ModelType type)
        {
            switch (type)
            {
                case ModelType.Activity:
                    await _navigator.NavigateTo(new ActivityViewModel(_navigator, SelectedActivity));
                    break;
                case ModelType.Meal:
                    _mealEditor = new MealViewModel(_navigator, SelectedMeal);
                    _mealEditor
                    await _navigator.NavigateTo(_mealEditor);
                    break;
                case ModelType.Mood:
                    _moodEditor = new MoodViewModel(_navigator, SelectedMood);
                    _moodEditor.ModelShed += Vm_ModelShed;
                    await _navigator.NavigateTo(_moodEditor);
                    break;
                case ModelType.Sleep:
                    await _navigator.NavigateTo(new SleepViewModel(_navigator, SelectedSleep));
                    break;
                case ModelType.Substance:
                    await _navigator.NavigateTo(new SubstanceViewModel(_navigator, SelectedSubstance));
                    break;
            }
        }

        /// <summary>
        /// Catches the view model shed by the editor.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Vm_ModelShed(object sender, EventArgs e)
        {
            var model = (e as ModelShedEventArgs).EventModel;
            switch (model.LogType)
            {
                case ModelType.Activity:
                    var newActivity = model as ActivityModel;
                    var id = newActivity.ID;

                    var oldIndex = Moods.IndexOf(x => x.ID == id);
                    Moods.Insert(oldIndex, newActivity);
                    Moods.RemoveAt(oldIndex + 1);
                    SelectedMood = newActivity;

                    MoodEditor.ModelShed -= Vm_ModelShed;
                    break;
                case ModelType.Meal:
                    var newMood = model as MoodModel;
                    var id = newMood.ID;

                    var oldIndex = Moods.IndexOf(x => x.ID == id);
                    Moods.Insert(oldIndex, newMood);
                    Moods.RemoveAt(oldIndex + 1);
                    SelectedMood = newMood;

                    MoodEditor.ModelShed -= Vm_ModelShed;
                    break;
                case ModelType.Mood:
                    var newMood = model as MoodModel;
                    var id = newMood.ID;

                    var oldIndex = Moods.IndexOf(x => x.ID == id);
                    Moods.Insert(oldIndex, newMood);
                    Moods.RemoveAt(oldIndex + 1);
                    SelectedMood = newMood;

                    MoodEditor.ModelShed -= Vm_ModelShed;
                    break;
                case ModelType.Sleep:
                    var newMood = model as MoodModel;
                    var id = newMood.ID;

                    var oldIndex = Moods.IndexOf(x => x.ID == id);
                    Moods.Insert(oldIndex, newMood);
                    Moods.RemoveAt(oldIndex + 1);
                    SelectedMood = newMood;

                    MoodEditor.ModelShed -= Vm_ModelShed;
                    break;
                case ModelType.Substance:
                    var newMood = model as MoodModel;
                    var id = newMood.ID;

                    var oldIndex = Moods.IndexOf(x => x.ID == id);
                    Moods.Insert(oldIndex, newMood);
                    Moods.RemoveAt(oldIndex + 1);
                    SelectedMood = newMood;

                    MoodEditor.ModelShed -= Vm_ModelShed;
                    break;
            }
        }

        private void DeleteSelected(ModelType type)
        {
            switch (type)
            {
                case ModelType.Activity:
                    break;
                case ModelType.Meal:
                    break;
                case ModelType.Mood:
                    break;
                case ModelType.Sleep:
                    break;
                case ModelType.Substance:
                    break;
            }
        }

    }
}

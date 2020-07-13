using SelfMonitoringApp.Models;
using SelfMonitoringApp.Models.Base;
using SelfMonitoringApp.ViewModels.Base;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace SelfMonitoringApp.ViewModels
{
    public class DaySummaryViewModel : ViewModelBase, INavigationViewModel
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

        #region Selected Log Objects
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
        #endregion

        // Show commands for specific categories
        public Command<ModelType> ShowCategoryComand { get; private set; }
        // Edit / Delete commands for selected objects
        public Command<ModelType> EditSelectedCommand { get; private set; }
        public Command<ModelType> DeleteSelectedCommand { get; private set; }

        public DaySummaryViewModel(List<SleepModel> sleeps, List<ActivityModel> activities,
            List<MealModel> meals, List<MoodModel> moods, List<SubstanceModel> substances, DateTime date) 
        {
            Meals      = new ObservableCollection<MealModel>(meals);
            Substances = new ObservableCollection<SubstanceModel>(substances);
            Activities = new ObservableCollection<ActivityModel>(activities);
            Moods      = new ObservableCollection<MoodModel>(moods);
            Sleeps     = new ObservableCollection<SleepModel>(sleeps);

            ShowCategoryComand    = new Command<ModelType>((type) => SetVisibility(type));
            EditSelectedCommand   = new Command<ModelType>(async(type) => await EditSelected(type));
            DeleteSelectedCommand = new Command<ModelType>(async(type) => await DeleteSelected(type));

            MoodsLogged = moods.Count > 0;
            SubstancesLogged = substances.Count > 0;
            MealsLogged = meals.Count > 0;
            SleepsLogged = sleeps.Count > 0;
            ActivitiesLogged = activities.Count > 0;

            if (MoodsLogged)
                MoodsVisibility = true;
            else if (MealsLogged)
                MealsVisibility = true;
            else if (SleepsLogged)
                SleepsVisibility = true;
            else if (ActivitiesLogged)
                ActivitiesVisibility = true;
            else if (SubstancesLogged)
                SubstanceVisibility = true;

            Date = $"{date.DayOfWeek}: {date.Year}-{date.Month}-{date.Day}";
        }

        /// <summary>
        /// Sets the visible collection on the day card to the type passed in.
        /// </summary>
        /// <param name="type">Type of model to show</param>
        private void SetVisibility(ModelType type)
        {
            switch (type)
            {
                case ModelType.Activity:
                    ActivitiesVisibility = true;
                    MealsVisibility = MoodsVisibility =
                    SleepsVisibility = SubstanceVisibility = false;
                    break;
                case ModelType.Meal:
                    MealsVisibility = true;
                    ActivitiesVisibility = MoodsVisibility = 
                    SleepsVisibility = SubstanceVisibility = false;
                    break;
                case ModelType.Mood:
                    MoodsVisibility = true;
                    ActivitiesVisibility = MealsVisibility = 
                    SleepsVisibility = SubstanceVisibility = false;
                    break;
                case ModelType.Sleep: 
                    SleepsVisibility = true;
                    ActivitiesVisibility = MealsVisibility = 
                    MoodsVisibility = SubstanceVisibility = false;
                    break;
                case ModelType.Substance:
                    SubstanceVisibility = true;
                    ActivitiesVisibility = MealsVisibility = 
                    MoodsVisibility = SleepsVisibility = false;
                    break;
            }
        }

        /// <summary>
        /// Pushes the editor page for the passed in type. Subscribes this object to wait for a new model
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private async Task EditSelected(ModelType type)
        {
            switch (type)
            {
                case ModelType.Activity:
                    if (SelectedActivity is null)
                        return;

                    _activityEditor = new ActivityViewModel(SelectedActivity);
                    _activityEditor.ModelShed += Vm_ModelShed;
                    await _navigator.NavigateTo(_activityEditor);
                    break;
                case ModelType.Meal:
                    if (SelectedMeal is null)
                        return;

                    _mealEditor = new MealViewModel(SelectedMeal);
                    _mealEditor.ModelShed += Vm_ModelShed;
                    await _navigator.NavigateTo(_mealEditor);
                    break;
                case ModelType.Mood:
                    if (SelectedMood is null)
                        return;

                    _moodEditor = new MoodViewModel(SelectedMood);
                    _moodEditor.ModelShed += Vm_ModelShed;
                    await _navigator.NavigateTo(_moodEditor);
                    break;
                case ModelType.Sleep:
                    if (SelectedSleep is null)
                        return;

                    _sleepEditor = new SleepViewModel(SelectedSleep);
                    _sleepEditor.ModelShed += Vm_ModelShed;
                    await _navigator.NavigateTo(_sleepEditor);
                    break;
                case ModelType.Substance:
                    if (SelectedSubstance is null)
                        return;

                    _substanceEditor = new SubstanceViewModel(SelectedSubstance);
                    _substanceEditor.ModelShed += Vm_ModelShed;
                    await _navigator.NavigateTo(_substanceEditor);
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

                    var oldIndex = Activities.IndexOf(x => x.ID == id);
                    Activities.Insert(oldIndex, newActivity);
                    Activities.RemoveAt(oldIndex + 1);
                    SelectedActivity = newActivity;

                    _activityEditor.ModelShed -= Vm_ModelShed;
                    break;
                case ModelType.Meal:
                    var newMeal = model as MealModel;
                    id = newMeal.ID;

                    oldIndex = Meals.IndexOf(x => x.ID == id);
                    Meals.Insert(oldIndex, newMeal);
                    Meals.RemoveAt(oldIndex + 1);
                    SelectedMeal = newMeal;

                    _mealEditor.ModelShed -= Vm_ModelShed;
                    break;
                case ModelType.Mood:
                    var newMood = model as MoodModel;
                    id = newMood.ID;

                    oldIndex = Moods.IndexOf(x => x.ID == id);
                    Moods.Insert(oldIndex, newMood);
                    Moods.RemoveAt(oldIndex + 1);
                    SelectedMood = newMood;

                    _moodEditor.ModelShed -= Vm_ModelShed;
                    break;
                case ModelType.Sleep:
                    var newSleep = model as SleepModel;
                    id = newSleep.ID;

                    oldIndex = Sleeps.IndexOf(x => x.ID == id);
                    Sleeps.Insert(oldIndex, newSleep);
                    Sleeps.RemoveAt(oldIndex + 1);
                    SelectedSleep = newSleep;

                    _sleepEditor.ModelShed -= Vm_ModelShed;
                    break;
                case ModelType.Substance:
                    var newSubstance = model as SubstanceModel;
                    id = newSubstance.ID;

                    oldIndex = Substances.IndexOf(x => x.ID == id);
                    Substances.Insert(oldIndex, newSubstance);
                    Substances.RemoveAt(oldIndex + 1);
                    SelectedSubstance = newSubstance;

                    _substanceEditor.ModelShed -= Vm_ModelShed;
                    break;
            }
        }

        private async Task DeleteSelected(ModelType type)
        {
            switch (type)
            {
                case ModelType.Activity:
                    if (SelectedActivity != null)
                    {
                        await _database.DeleteLog(SelectedActivity);
                        Activities.Remove(SelectedActivity);
                        SelectedActivity = null;
                    }
                    break;
                case ModelType.Meal:
                    if(SelectedMeal != null)
                    {
                        await _database.DeleteLog(SelectedMeal);
                        Meals.Remove(SelectedMeal);
                        SelectedMeal = null;
                    }
                    break;
                case ModelType.Mood:
                    if (SelectedMood != null)
                    {
                        await _database.DeleteLog(SelectedMood);
                        Moods.Remove(SelectedMood);
                        SelectedMood = null;
                    }
                    break;
                case ModelType.Sleep:
                    if(SelectedSleep != null)
                    {
                        await _database.DeleteLog(SelectedSleep);
                        Sleeps.Remove(SelectedSleep);
                        SelectedSleep = null;
                    }
                    break;
                case ModelType.Substance:
                    if(SelectedSubstance != null)
                    {
                        await _database.DeleteLog(SelectedSubstance);
                        Substances.Remove(SelectedSubstance);
                        SelectedSubstance = null;
                    }
                    break;
            }
        }
    }
}

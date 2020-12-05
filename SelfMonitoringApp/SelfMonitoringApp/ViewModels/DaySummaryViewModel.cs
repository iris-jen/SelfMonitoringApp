using SelfMonitoringApp.Models;
using SelfMonitoringApp.Models.Base;
using SelfMonitoringApp.ViewModels.Base;
using SelfMonitoringApp.ViewModels.Logs;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace SelfMonitoringApp.ViewModels
{
    public class DaySummaryViewModel : ViewModelBase, INavigationViewModel
    {
        [ModelType(ModelType.Meal)]
        public ObservableCollection<MealModel> Meals { get; private set; }

        [ModelType(ModelType.Substance)]
        public ObservableCollection<SubstanceModel> Substances { get; private set; }

        [ModelType(ModelType.Activity)]
        public ObservableCollection<ActivityModel> Activities { get; private set; }

        [ModelType(ModelType.Mood)]
        public ObservableCollection<MoodModel> Moods { get; private set; }

        [ModelType(ModelType.Sleep)]
        public ObservableCollection<SleepModel> Sleeps { get; private set; }

        [ModelType(ModelType.Socialization)]
        public ObservableCollection<SocializationModel> Socials { get; private set; }

        private MoodViewModel _moodEditor;
        private MealViewModel _mealEditor;
        private SubstanceViewModel _substanceEditor;
        private SleepViewModel _sleepEditor;
        private ActivityViewModel _activityEditor;
        private SocializationViewModel _socialEditor;

        /// <summary>
        /// Date for this objects logs 
        /// </summary>
        public string Date { get; private set; }

        /// <summary>
        /// Set to true for meal collection visibility. all others must be set false
        /// </summary>
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
        private bool _mealsVisibility;

        /// <summary>
        /// Set to true for substance collection visibility. all others must be set false
        /// </summary>
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
        private bool _substancesVisibility;

        /// <summary>
        /// Set to true for activity collection visibility. all others must be set false
        /// </summary>
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
        private bool _activitiesVisibility;

        /// <summary>
        /// Set to true for mood collection visibility. all others must be set false
        /// </summary>
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
        private bool _moodsVisibility;

        /// <summary>
        /// Set to true for sleep collection visibility. all others must be set false
        /// </summary>
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
        private bool _sleepsVisibility;

        /// <summary>
        /// Set to true for socialization collection visibility. all others must be set false
        /// </summary>
        public bool SocialsVisibility
        {
            get => _socialVisibility;
            set
            {
                if (_socialVisibility == value)
                    return;

                _socialVisibility = value;
                NotifyPropertyChanged();
            }
        }
        private bool _socialVisibility;

        /// <summary>
        /// True if the user has any sleep logs for this day. 
        /// </summary>
        public bool SleepsLogged
        {
            get => _sleepsLogged;
            set
            {
                if (_sleepsLogged == value)
                    return;

                _sleepsLogged = value;
                NotifyPropertyChanged();
            }
        }
        private bool _sleepsLogged;


        /// <summary>
        /// True if the user has any meal logs for the day
        /// </summary>
        public bool MealsLogged
        {
            get => _mealsLogged;
            set
            {
                if (_mealsLogged == value)
                    return;

                _mealsLogged = value;
                NotifyPropertyChanged();
            }
        }
        private bool _mealsLogged;

        /// <summary>
        /// True if the user has any activity logs for the day
        /// </summary>
        public bool ActivitiesLogged
        {
            get => _activitiesLogged;
            set
            {
                if (_activitiesLogged == value)
                    return;

                _activitiesLogged = value;
                NotifyPropertyChanged();
            }
        }
        private bool _activitiesLogged;

        /// <summary>
        /// True if the user has any mood logs for the day
        /// </summary>
        public bool MoodsLogged
        {
            get => _moodsLogged;
            set
            {
                if (_moodsLogged == value)
                    return;

                _moodsLogged = value;
                NotifyPropertyChanged();
            }
        }
        private bool _moodsLogged;

        /// <summary>
        /// True if the user has any substance logs for the day
        /// </summary>
        public bool SubstancesLogged
        {
            get => _substancesLogged;
            set
            {
                if (_substancesLogged == value)
                    return;

                _substancesLogged = value;
                NotifyPropertyChanged();
            }
        }
        private bool _substancesLogged;

        /// <summary>
        /// True if the user has any social logs for the day
        /// </summary>
        public bool SocialsLogged
        {
            get => _socialsLogged;
            set
            {
                if (_socialsLogged == value)
                    return;

                _socialsLogged = value;
                NotifyPropertyChanged();
            }
        }
        private bool _socialsLogged;

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

        private SocializationModel _selectedSocial;
        public SocializationModel SelectedSocial
        {
            get => _selectedSocial;
            set
            {
                if (_selectedSocial == value)
                    return;

                _selectedSocial = value;
                NotifyPropertyChanged();
            }
        }

        private ModelType _selectedModel;
        public ModelType SelectedModel
        {
            get => _selectedModel;
            set
            {
                if (_selectedModel == value)
                    return;

                _selectedModel = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        // Edit / Delete commands for selected objects
        public Command<ModelType> EditSelectedCommand   { get; private set; }
        public Command<ModelType> AddSelectedCommand    { get; private set; }
        public Command<ModelType> DeleteSelectedCommand { get; private set; }
        public Command<ModelType> SwitchViewCommand     { get; private set; }

        public DaySummaryViewModel(List<SleepModel> sleeps, List<ActivityModel> activities,
            List<MealModel> meals, List<MoodModel> moods, List<SubstanceModel> substances, List<SocializationModel> socials, DateTime date) 
        {
            Meals      = new ObservableCollection<MealModel>(meals);
            Substances = new ObservableCollection<SubstanceModel>(substances);
            Activities = new ObservableCollection<ActivityModel>(activities);
            Moods      = new ObservableCollection<MoodModel>(moods);
            Sleeps     = new ObservableCollection<SleepModel>(sleeps);
            Socials    = new ObservableCollection<SocializationModel>(socials);

            EditSelectedCommand   = new Command<ModelType>(async(type) => await EditSelected(type));
            DeleteSelectedCommand = new Command<ModelType>(async(type) => await DeleteSelected(type));
            AddSelectedCommand    = new Command<ModelType>(async (type) => await AddSelected(type));

            MoodsLogged = moods.Count > 0;
            SubstancesLogged = substances.Count > 0;
            MealsLogged = meals.Count > 0;
            SleepsLogged = sleeps.Count > 0;
            ActivitiesLogged = activities.Count > 0;
            SocialsLogged = socials.Count > 0;

            if (MoodsLogged)
                SelectedModel = ModelType.Mood;
            else if (MealsLogged)
                SelectedModel = ModelType.Meal;
            else if (SleepsLogged)
                SelectedModel = ModelType.Sleep;
            else if (ActivitiesLogged)
                SelectedModel = ModelType.Activity;
            else if (SubstancesLogged)
                SelectedModel = ModelType.Substance;
            else if (SocialsLogged)
                SelectedModel = ModelType.Socialization;

            Date = $"{date.DayOfWeek}: {date.Year}-{date.Month}-{date.Day}";
            SwitchViewCommand = new Command<ModelType>((e) => SetVisibility(e));
            SetVisibility(SelectedModel);
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
                    MealsVisibility = MoodsVisibility = SocialsVisibility =
                    SleepsVisibility = SubstanceVisibility = false;
                    break;
                case ModelType.Meal:
                    MealsVisibility = true;
                    ActivitiesVisibility = MoodsVisibility = SocialsVisibility =
                    SleepsVisibility = SubstanceVisibility = false;
                    break;
                case ModelType.Mood:
                    MoodsVisibility = true;
                    ActivitiesVisibility = MealsVisibility = SocialsVisibility =
                    SleepsVisibility = SubstanceVisibility = false;
                    break;
                case ModelType.Sleep:
                    SleepsVisibility = true;
                    ActivitiesVisibility = MealsVisibility = SocialsVisibility =
                    MoodsVisibility = SubstanceVisibility = false;
                    break;
                case ModelType.Substance:
                    SubstanceVisibility = true;
                    ActivitiesVisibility = MealsVisibility = SocialsVisibility =
                    MoodsVisibility = SleepsVisibility = false;
                    break;
                case ModelType.Socialization:
                    SocialsVisibility = true;
                    ActivitiesVisibility = MealsVisibility = SubstanceVisibility =
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
                case ModelType.Socialization:
                    if (SelectedSocial is null)
                        return;

                    _socialEditor = new SocializationViewModel(SelectedSocial);
                    _socialEditor.ModelShed += Vm_ModelShed;
                    await _navigator.NavigateTo(_socialEditor);
                    break;
            }
        }

        /// <summary>
        /// Pushes the editor page for the passed in type. Subscribes this object to wait for a new model
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private async Task AddSelected(ModelType type)
        {
            switch (type)
            {
                case ModelType.Activity:
                    _activityEditor = new ActivityViewModel();
                    _activityEditor.ModelShed += Vm_ModelShed;
                    await _navigator.NavigateTo(_activityEditor);
                    break;
                case ModelType.Meal:
                    _mealEditor = new MealViewModel();
                    _mealEditor.ModelShed += Vm_ModelShed;
                    await _navigator.NavigateTo(_mealEditor);
                    break;
                case ModelType.Mood:
                    _moodEditor = new MoodViewModel();
                    _moodEditor.ModelShed += Vm_ModelShed;
                    await _navigator.NavigateTo(_moodEditor);
                    break;
                case ModelType.Sleep:
                    _sleepEditor = new SleepViewModel();
                    _sleepEditor.ModelShed += Vm_ModelShed;
                    await _navigator.NavigateTo(_sleepEditor);
                    break;
                case ModelType.Substance:
                    _substanceEditor = new SubstanceViewModel();
                    _substanceEditor.ModelShed += Vm_ModelShed;
                    await _navigator.NavigateTo(_substanceEditor);
                    break;
                case ModelType.Socialization:
                    _socialEditor = new SocializationViewModel();
                    _socialEditor.ModelShed += Vm_ModelShed;
                    await _navigator.NavigateTo(_socialEditor);
                    break;
            }
        }

        private void Vm_ModelShed(object sender, EventArgs e)
        {
            Stopwatch ExecutionTimer = Stopwatch.StartNew();
            
            bool useReflection = false;

            if (useReflection)
                Vm_ModelShedReflection(sender, e);
            else
                Vm_ModelShedSwitch(sender, e);
        }

        // todo - all these models are part of the same interface, 
        //i should be able to get rid of this repetive junk somehow.

        /// <summary>
        /// Catches the view model shed by the editor.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Vm_ModelShedSwitch(object sender, EventArgs e)
        {
            var model = (e as ModelShedEventArgs).EventModel;
            switch (model.LogType)
            {
                case ModelType.Activity:
                    var newActivity = model as ActivityModel;
                    var id = newActivity.ID;

                    var oldIndex = Activities.IndexOf(x => x.ID == id);

                    if (oldIndex == -1)
                    {
                        Activities.Add(newActivity);
                    }
                    else
                    {
                        Activities.Insert(oldIndex, newActivity);
                        Activities.RemoveAt(oldIndex + 1);
                    }
                    SelectedActivity = newActivity;

                    _activityEditor.ModelShed -= Vm_ModelShed;
                    break;

            #region Meal
                case ModelType.Meal:
                    var newMeal = model as MealModel;
                    id = newMeal.ID;

                    oldIndex = Meals.IndexOf(x => x.ID == id);
                    if (oldIndex == -1)
                    {
                        Meals.Add(newMeal);
                    }
                    else
                    {
                        Meals.Insert(oldIndex, newMeal);
                        Meals.RemoveAt(oldIndex + 1);
                    }
                    SelectedMeal = newMeal;

                    _mealEditor.ModelShed -= Vm_ModelShed;
                    break; 
            #endregion

            #region Mood
                case ModelType.Mood:
                    var newMood = model as MoodModel;
                    id = newMood.ID;

                    oldIndex = Moods.IndexOf(x => x.ID == id);
                    if (oldIndex == -1)
                    {
                        Moods.Add(newMood);
                    }
                    else
                    {
                        Moods.Insert(oldIndex, newMood);
                        Moods.RemoveAt(oldIndex + 1);
                    }

                    SelectedMood = newMood;
                    _moodEditor.ModelShed -= Vm_ModelShed;
                    break;
            #endregion

            #region Sleep
                case ModelType.Sleep:
                    var newSleep = model as SleepModel;
                    id = newSleep.ID;

                    oldIndex = Sleeps.IndexOf(x => x.ID == id);
                    if (oldIndex == -1)
                    {
                        Sleeps.Add(newSleep);
                    }
                    else
                    {
                        Sleeps.Insert(oldIndex, newSleep);
                        Sleeps.RemoveAt(oldIndex + 1);
                    }

                    SelectedSleep = newSleep;
                    _sleepEditor.ModelShed -= Vm_ModelShed;
                    break;
            #endregion

            #region Substance
                case ModelType.Substance:
                    var newSubstance = model as SubstanceModel;
                    id = newSubstance.ID;

                    oldIndex = Substances.IndexOf(x => x.ID == id);
                    if (oldIndex == -1)
                    {
                        Substances.Add(newSubstance);
                    }
                    else
                    {
                        Substances.Insert(oldIndex, newSubstance);
                        Substances.RemoveAt(oldIndex + 1);
                    }

                    SelectedSubstance = newSubstance;
                    _substanceEditor.ModelShed -= Vm_ModelShed;
                    break;
                #endregion

            #region Socialization
                case ModelType.Socialization:
                    var newSocial = model as SocializationModel;
                    id = newSocial.ID;

                    oldIndex = Socials.IndexOf(x => x.ID == id);
                    if (oldIndex == -1)
                    {
                        Socials.Add(newSocial);
                    }
                    else
                    {
                        Socials.Insert(oldIndex, newSocial);
                        Socials.RemoveAt(oldIndex + 1);
                    }

                    SelectedSocial = newSocial;
                    _socialEditor.ModelShed -= Vm_ModelShed;
                    break; 
            #endregion
            }
        }



        private void Vm_ModelShedReflection(object sender, EventArgs e)
        {
            IModel model = (e as ModelShedEventArgs).EventModel;
            Type modelType = model.GetType();
            PropertyInfo[] classProperties = GetType().GetProperties();

            foreach(var property in classProperties)
            {
                Type pType = property.PropertyType;


                if(property.PropertyType == typeof(ObservableCollection<>).MakeGenericType(new[] { modelType }))
                {
                }
            }

            switch (model.LogType)
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


                case ModelType.Socialization:

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
                case ModelType.Socialization:
                    if (SelectedSocial!= null)
                    {
                        await _database.DeleteLog(SelectedSocial);
                        Socials.Remove(SelectedSocial);
                        SelectedSocial = null;
                    }
                    break;
            }
        }
    }
}

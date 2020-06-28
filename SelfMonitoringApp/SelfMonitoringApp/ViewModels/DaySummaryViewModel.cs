using SelfMonitoringApp.Models;
using SelfMonitoringApp.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace SelfMonitoringApp.ViewModels
{
    public class DaySummaryViewModel : PropertyChangedBase
    {
        public ObservableCollection<MealModel> Meals { get; private set; }
        public ObservableCollection<SubstanceModel> Substances { get; private set; }
        public ObservableCollection<ActivityModel> Activities { get; private set; }
        public ObservableCollection<MoodModel> Moods { get; private set; }
        public ObservableCollection<SleepModel> Sleeps { get; private set; }

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

        public Command ShowSleepComand { get; private set; }
        public Command ShowMoodCommand { get; private set; }
        public Command ShowSubstanceCommand { get; private set; }
        public Command ShowMealCommand { get; private set; }
        public Command ShowActivityCommand { get; private set; }

        public DaySummaryViewModel(List<SleepModel> sleeps, List<ActivityModel> activities,
            List<MealModel> meals, List<MoodModel> moods, List<SubstanceModel> substances, DateTime date)
        {
            Meals = new ObservableCollection<MealModel>(meals);
            Substances = new ObservableCollection<SubstanceModel>(substances);
            Activities = new ObservableCollection<ActivityModel>(activities);
            Moods = new ObservableCollection<MoodModel>(moods);
            Sleeps = new ObservableCollection<SleepModel>(sleeps);

            ShowSleepComand = new Command(() => SetVisibility(ModelType.Sleep));
            ShowMoodCommand = new Command(() => SetVisibility(ModelType.Mood));
            ShowSubstanceCommand = new Command(() => SetVisibility(ModelType.Substance));
            ShowMealCommand = new Command(() => SetVisibility(ModelType.Meal));
            ShowActivityCommand = new Command(() => SetVisibility(ModelType.Activity));

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

    }
}

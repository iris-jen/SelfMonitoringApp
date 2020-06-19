using SelfMonitoringApp.Models;
using SelfMonitoringApp.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

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

        private bool _mealVisibility;
        public bool MealVisibility
        {
            get => _mealVisibility;
            set
            {
                if (_mealVisibility == value)
                    return;

                _mealVisibility = value;
                NotifyPropertyChanged();
            }
        }

        private bool _substanceVisibility;
        public bool SubstanceVisibility
        {
            get => _substanceVisibility;
            set
            {
                if (_substanceVisibility == value)
                    return;

                _substanceVisibility = value;
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

        public DaySummaryViewModel(List<SleepModel> sleeps, List<ActivityModel> activities,
            List<MealModel> meals, List<MoodModel> moods, List<SubstanceModel> substances, DateTime date)
        {
            Meals = new ObservableCollection<MealModel>(meals);
            Substances = new ObservableCollection<SubstanceModel>(substances);
            Activities = new ObservableCollection<ActivityModel>(activities);
            Moods = new ObservableCollection<MoodModel>(moods);
            Sleeps = new ObservableCollection<SleepModel>(sleeps);

            Date = $"{date.DayOfWeek}: {date.Year}-{date.Month}-{date.Day}";
        }

    }
}

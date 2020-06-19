using SelfMonitoringApp.Models;
using SelfMonitoringApp.Navigation;
using SelfMonitoringApp.Services;
using SelfMonitoringApp.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace SelfMonitoringApp.ViewModels
{
    public class DataExplorerViewModel : NavigatableViewModelBase, INavigationViewModel
    {
        public const string NavigationNodeName = "data";
        public ObservableCollection<DaySummaryViewModel> DaySummaries { get; private set; }

        private DateTime _startDate = new DateTime(DateTime.Now.Year,DateTime.Now.Month, DateTime.Now.Day-1);
        public DateTime StartDate
        {
            get => _startDate;
            set
            {
                if (_startDate == value)
                    return;

                _startDate = value;
                DaySummaries = GetDateFilteredData(_startDate, _endDate);
                RaiseAllPropertiesChanged();
            }
        }

        private DateTime _endDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
        public DateTime EndDate
        {
            get => _endDate;
            set
            {
                if (_endDate == value)
                    return;

                _endDate = value;
                DaySummaries = GetDateFilteredData(_startDate, _endDate);
                RaiseAllPropertiesChanged();
            }
        }


        public DataExplorerViewModel(INavigationService navService) 
            : base(navService)
        {
            DaySummaries = GetDateFilteredData(StartDate, EndDate);
        }

        private bool DateInRange(DateTime startDate, DateTime endDate, DateTime checkDate, DateTime filter) =>
            (checkDate >= startDate && checkDate <= endDate) && checkDate.Day == filter.Day &&
            checkDate.Month == filter.Month && checkDate.Year == filter.Year;

        public ObservableCollection<DaySummaryViewModel> GetDateFilteredData(DateTime startDate, DateTime endDate)
        {
            var filteredData = new ObservableCollection<DaySummaryViewModel>();
            if (startDate <= endDate)
            {
                var sleepData = DataStore.Sleeps;
                var moodData = DataStore.Moods;
                var mealData = DataStore.Meals;
                var substanceData = DataStore.Substances;
                var activityData = DataStore.Activities;

                for (DateTime date = StartDate; date.Date <= EndDate.Date; date = date.AddDays(1))
                {
                    var sleeps = (sleepData.Where(sleep => DateInRange(startDate, endDate, sleep.RegisteredTime,date))).ToList();
                    var moods = (moodData.Where(mood => DateInRange(startDate, endDate, mood.RegisteredTime,date))).ToList();
                    var meals = (mealData.Where(meal => DateInRange(startDate, endDate, meal.RegisteredTime,date))).ToList();
                    var activities = (activityData.Where(activities => DateInRange(startDate, endDate, activities.RegisteredTime,date))).ToList();
                    var substances = (substanceData.Where(substances => DateInRange(startDate, endDate, substances.RegisteredTime,date))).ToList();

                    if (sleeps.Count != 0 || activities.Count != 0 || moods.Count != 0 || meals.Count != 0 || substances.Count != 0)
                        filteredData.Add(new DaySummaryViewModel(sleeps, activities, meals, moods, substances,date));
                }
            }
            return filteredData;
        }

    }
}

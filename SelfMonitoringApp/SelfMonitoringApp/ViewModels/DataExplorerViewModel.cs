using SelfMonitoringApp.Models;
using SelfMonitoringApp.ViewModels.Base;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Linq;
using Acr.UserDialogs;
using Xamarin.Forms;

namespace SelfMonitoringApp.ViewModels
{
    public class DataExplorerViewModel : ViewModelBase, INavigationViewModel
    {
        public const string NavigationNodeName = "data";
        public ObservableCollection<DaySummaryViewModel> DaySummaries { get; private set; }

        private DateTime _startDate = DateTime.Now.AddDays(-1);
        private DateTime _endDate = DateTime.Now.AddDays(1);
        private bool _firstLoad = true;

        private bool _loading;
        public bool Loading
        {
            get => _loading;
            set
            {
                if (_loading == value)
                    return;

                _loading = value;
                NotifyPropertyChanged();
            }
        }

        private bool _noData;
        public bool NoData
        {
            get => _noData;
            set
            {
                if (_noData == value)
                    return;

                _noData = value;
                NotifyPropertyChanged();
            }
        }

        private string _dateDisplay;
        public string DateDisplay
        {
            get => _dateDisplay;
            set
            {
                if (_dateDisplay == value)
                    return;

                _dateDisplay = value;
                NotifyPropertyChanged();
            }
        }

        public Command LoadDataCommand { get; private set; }
        public DataExplorerViewModel()
        {
            LoadDataCommand = new Command(async () => await LoadData());
            LoadData().SafeFireAndForget(false);
        }
         
        private bool DateInRange(DateTime startDate, DateTime endDate, DateTime checkDate, DateTime filter) =>
            (checkDate >= startDate && checkDate <= endDate) && checkDate.Day == filter.Day &&
            checkDate.Month == filter.Month && checkDate.Year == filter.Year;

        public async Task<ObservableCollection<DaySummaryViewModel>> GetDateFilteredData(DateTime startDate, DateTime endDate)
        {
            DateDisplay = $"From: {startDate:ddd dd MMM}\nTo: {endDate:ddd dd MMM}";

            var filteredData = new ObservableCollection<DaySummaryViewModel>();
            if (startDate <= endDate)
            {
                List<SleepModel> sleepData = null;
                List<MoodModel> moodData = null;
                List<MealModel> mealData = null; 
                List<SubstanceModel> substanceData = null;
                List<ActivityModel> activityData = null;
                List<SocializationModel> socialData = null;

                List<Task> loadingTasks = new List<Task>()
                {
                    Task.Run( async ()=> sleepData = await _database.GetSleepsAsync()),
                    Task.Run( async ()=> moodData = await _database.GetMoodsAsync()),
                    Task.Run( async ()=> mealData = await _database.GetMealsAsync()),
                    Task.Run( async ()=> substanceData = await _database.GetSubstancesAsync()),
                    Task.Run( async ()=> activityData = await _database.GetActivitiesAsync()),
                    Task.Run( async ()=> socialData = await _database.GetSocialsAsync())
                };

                await Task.WhenAll(loadingTasks);
                
                //todo
                //this method is kinda fucking gross, i should make it nicer-er at some point
                await Task.Factory.StartNew(() =>
                {
                    for (DateTime date = startDate; date.Date <= endDate.Date; date = date.AddDays(1))
                    {

                        List<SleepModel> sleeps 
                            = (sleepData.Where(sleep => DateInRange(startDate, endDate, sleep.RegisteredTime, date))).ToList();
                        List<MoodModel> moods 
                            = (moodData.Where(mood => DateInRange(startDate, endDate, mood.RegisteredTime, date))).ToList();
                        List<MealModel> meals 
                            = (mealData.Where(meal => DateInRange(startDate, endDate, meal.RegisteredTime, date))).ToList();
                        List<ActivityModel> activities  
                            = (activityData.Where(activity => DateInRange(startDate, endDate, activity.RegisteredTime, date))).ToList();
                        List<SubstanceModel> substances 
                            = (substanceData.Where(substance => DateInRange(startDate, endDate, substance.RegisteredTime, date))).ToList();
                        List<SocializationModel> socials
                            = (socialData.Where(social => DateInRange(startDate, endDate, social.RegisteredTime, date))).ToList();

                        if (sleeps.Count > 0)
                            sleeps.Sort((t1, t2) => DateTime.Compare(t1.RegisteredTime, t2.RegisteredTime));
                        if(moods.Count>0)
                            moods.Sort((t1, t2) => DateTime.Compare(t1.RegisteredTime, t2.RegisteredTime));
                        if(meals.Count>0)
                            meals.Sort((t1, t2) => DateTime.Compare(t1.RegisteredTime, t2.RegisteredTime));
                        if(activities.Count > 0)
                            activities.Sort((t1, t2) => DateTime.Compare(t1.RegisteredTime, t2.RegisteredTime));
                        if(substances.Count > 0)
                            substances.Sort((t1, t2) => DateTime.Compare(t1.RegisteredTime, t2.RegisteredTime));
                        if (socials.Count > 0)
                            socials.Sort((t1, t2) => DateTime.Compare(t1.RegisteredTime, t2.RegisteredTime));

                        if (sleeps.Count != 0 || activities.Count != 0 || moods.Count != 0 || meals.Count != 0 || substances.Count != 0)
                            filteredData.Add(new DaySummaryViewModel(sleeps, activities, meals, moods, substances,socials, date));
                    }
                });
            }
            return filteredData;
        }

        /// <summary>
        /// Sets DaySummaries to GetFilteredData of the date time user inputs
        /// </summary>
        /// <returns></returns>
        public async Task LoadData()
        {
            Loading = true;
            try
            {
                if (!_firstLoad)
                {
                    DatePromptResult fromResult = await UserDialogs.Instance.DatePromptAsync("Select From Date", _startDate);
                    if (fromResult.Ok)
                        _startDate = fromResult.SelectedDate;
                    else
                        return;

                    DatePromptResult toResult = await UserDialogs.Instance.DatePromptAsync("Select To Date", _endDate);
                    if (toResult.Ok)
                        _endDate = toResult.SelectedDate;
                    else
                        return;
                }
                else
                    _firstLoad = false;

                ObservableCollection<DaySummaryViewModel> data = await GetDateFilteredData(_startDate, _endDate);

                NoData = data.Count == 0;
                //Let the render catch up after hiding / showing the warning
                await Task.Delay(50);

                DaySummaries = data;
                NotifyPropertyChanged(nameof(DaySummaries));
            }
            catch(Exception ex)
            {
                await UserDialogs.Instance.AlertAsync(ex.ToString(),  "Could not load data");
            }
            finally
            {
                Loading = false;
            }
        }
    }
}

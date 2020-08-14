using SelfMonitoringApp.Models;
using SelfMonitoringApp.Models.Base;
using SelfMonitoringApp.ViewModels.Base;
using SelfMonitoringApp.ViewModels.Logs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SelfMonitoringApp.ViewModels
{
    public class TrendsViewModel : ViewModelBase, INavigationViewModel
    {
        public ObservableCollection<TrendModel> Trends { get; set; }
        public ObservableCollection<OccuranceModel> MoodOccurances { get; set; }
        /// <summary>
        /// User selected date
        /// </summary>
        public DateTime SelectedDay
        {
            get => _selectedDay;
            set
            {
                if (_selectedDay == value)
                    return;

                _selectedDay = value;
                NotifyPropertyChanged();
                LoadData().SafeFireAndForget(false);
            }
        }
        private DateTime _selectedDay = DateTime.Now;

        /// <summary>
        /// True when loading, used to show the user an ongoing process
        /// </summary>
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
        private bool _loading;

        public Command DateForwardCommand { get; set; }
        public Command DateBackwardCommand { get; set; }
        public Command LoadSelectedDateCommand { get; set; }
          

        public TrendsViewModel()
        {
            MoodOccurances = new ObservableCollection<OccuranceModel>();
            DateForwardCommand = new Command(() => { SelectedDay = SelectedDay.AddDays(1); });
            DateBackwardCommand = new Command(() => { SelectedDay = SelectedDay.AddDays(-1); });
            LoadData().SafeFireAndForget(false);
        }

        public async Task LoadData()
        {
            Loading = true;
            Trends = new ObservableCollection<TrendModel>(await SeperateSingleDayData());
            NotifyPropertyChanged(nameof(Trends));
            Loading = false;
        }

        /// <summary>
        /// Looks at the data base and trys to pick apart information about the users habbits
        /// </summary>
        /// <returns></returns>
        public async Task<List<TrendModel>> SeperateSingleDayData()
        {
            List<TrendModel> output = new List<TrendModel>();
            MoodOccurances = new ObservableCollection<OccuranceModel>();

            #region Grab data
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
            #endregion

            await Task.WhenAll(loadingTasks);

            List<TrendModel> outputTrends = new List<TrendModel>();
            await Task.Factory.StartNew(() =>
            {
                var date = SelectedDay.Date;

                #region Sort and filter data
                List<SleepModel> sleeps
                    = (sleepData.Where(sleep => sleep.SleepEndDate.Date == date)).ToList();
                List<MoodModel> moods
                    = (moodData.Where(mood => mood.RegisteredTime.Date == date)).ToList();
                List<MealModel> meals
                    = (mealData.Where(meal => meal.RegisteredTime.Date == date)).ToList();
                List<ActivityModel> activities
                    = (activityData.Where(activity => activity.RegisteredTime.Date == date)).ToList();
                List<SubstanceModel> substances
                    = (substanceData.Where(substance => substance.RegisteredTime.Date == date)).ToList();
                List<SocializationModel> socials
                    = (socialData.Where(social => social.RegisteredTime.Date == date)).ToList();

                if (sleeps.Count > 0)
                    sleeps.Sort((t1, t2) => DateTime.Compare(t1.RegisteredTime, t2.RegisteredTime));
                if (moods.Count > 0)
                    moods.Sort((t1, t2) => DateTime.Compare(t1.RegisteredTime, t2.RegisteredTime));
                if (meals.Count > 0)
                    meals.Sort((t1, t2) => DateTime.Compare(t1.RegisteredTime, t2.RegisteredTime));
                if (activities.Count > 0)
                    activities.Sort((t1, t2) => DateTime.Compare(t1.RegisteredTime, t2.RegisteredTime));
                if (substances.Count > 0)
                    substances.Sort((t1, t2) => DateTime.Compare(t1.RegisteredTime, t2.RegisteredTime));
                if (socials.Count > 0)
                    socials.Sort((t1, t2) => DateTime.Compare(t1.RegisteredTime, t2.RegisteredTime)); 
                #endregion

                List<OccuranceModel> sleepOccurances = new List<OccuranceModel>();

                foreach (SleepModel model in sleeps)
                    sleepOccurances.Add(new OccuranceModel(model.SleepEndDate, model.TotalSleep));

                foreach (MoodModel model in moods)
                    MoodOccurances.Add(new OccuranceModel(model.RegisteredTime, model.OverallMood));
                
                NotifyPropertyChanged(nameof(MoodOccurances));


                if (sleepOccurances.Count > 0)
                {
                    TrendModel SleepTrend = new TrendModel()
                    {
                        Occurances = sleepOccurances,
                        TrendContextTotal = sleepOccurances.Sum(x => x.Ammount),
                        TrendContextUnit = "Hours",
                        TrendName = "Sleep",
                        TotalOccurances = sleepOccurances.Count
                    };
                    outputTrends.Add(SleepTrend);
                }

                #region Pick out substances
                Dictionary<string, List<OccuranceModel>> substanceOccurances = new Dictionary<string, List<OccuranceModel>>();
                foreach (SubstanceModel substance in substances)
                {
                    OccuranceModel occurance = new OccuranceModel(substance.RegisteredTime, substance.Amount)
                    {
                        Unit = substance.Unit,
                    };

                    if (substanceOccurances.ContainsKey(substance.SubstanceName))
                        substanceOccurances[substance.SubstanceName].Add(occurance);
                    else
                        substanceOccurances.Add(substance.SubstanceName, new List<OccuranceModel>() { occurance });
                }

                foreach (KeyValuePair<string, List<OccuranceModel>> substanceOccurance in substanceOccurances)
                {
                    outputTrends.Add(new TrendModel
                    {
                        Occurances = substanceOccurance.Value,
                        TotalOccurances = substanceOccurance.Value.Count(),
                        TrendContextTotal = substanceOccurance.Value.Sum(x => x.Ammount),
                        TrendContextUnit = substanceOccurance.Value.First().Unit,
                        TrendName = substanceOccurance.Key
                    });
                }
                #endregion

                #region Activity Occurrences
                Dictionary<string, List<OccuranceModel>> activityOccurances = new Dictionary<string, List<OccuranceModel>>();
                foreach (ActivityModel activity in activities)
                {
                    OccuranceModel occurance = new OccuranceModel(activity.EndTime, activity.Duration)
                    {
                        Unit = "Hours",
                    };

                    if (activityOccurances.ContainsKey(activity.ActivityName))
                        activityOccurances[activity.ActivityName].Add(occurance);
                    else
                        activityOccurances.Add(activity.ActivityName, new List<OccuranceModel>() { occurance });
                }

                foreach (KeyValuePair<string, List<OccuranceModel>> activityOccurance in activityOccurances)
                {
                    outputTrends.Add(new TrendModel
                    {
                        Occurances = activityOccurance.Value,
                        TotalOccurances = activityOccurance.Value.Count(),
                        TrendContextTotal = activityOccurance.Value.Sum(x => x.Ammount),
                        TrendContextUnit = activityOccurance.Value.First().Unit,
                        TrendName = activityOccurance.Key
                    });
                } 
                #endregion

            });
            return outputTrends;
        }
    }
}
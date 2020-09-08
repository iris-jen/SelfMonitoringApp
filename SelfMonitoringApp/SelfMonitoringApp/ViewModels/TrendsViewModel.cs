using Acr.UserDialogs;
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
        public ObservableCollection<TrendModel> Trends { get; private set; }
        public ObservableCollection<OccuranceModel> MoodOccurances { get; private set; }
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
            Trends = new ObservableCollection<TrendModel>();
            DateForwardCommand = new Command(() => { SelectedDay = SelectedDay.AddDays(1); });
            DateBackwardCommand = new Command(() => { SelectedDay = SelectedDay.AddDays(-1); });
            LoadData().SafeFireAndForget(false);
        }

        /// <summary>
        /// Looks at the data base and trys to pick apart information about the users habbits
        /// </summary>
        /// <returns></returns>
        public async Task LoadData()
        {
            Loading = true;
            try
            {
                var data = await _database.GetAllModelsInSpanAsync(SelectedDay,SelectedDay);
                MoodOccurances.Clear();
                Trends.Clear();

                foreach (MoodModel model in data[ModelType.Mood])
                    MoodOccurances.Add(new OccuranceModel(model.RegisteredTime, model.OverallMood));

                await Task.Factory.StartNew(() =>
                {
                    var date = SelectedDay.Date;

                    #region Filter sleep data
                    List<OccuranceModel> sleepOccurances = new List<OccuranceModel>();

                    foreach (SleepModel model in data[ModelType.Sleep])
                        sleepOccurances.Add(new OccuranceModel(model.SleepEndDate, model.TotalSleep));

                    if (sleepOccurances.Count > 0)
                    {
                        TrendModel SleepTrend = new TrendModel()
                        {
                            Occurances = sleepOccurances,
                            TrendContextTotal = sleepOccurances.Sum(x => x.Ammount),
                            TrendContextUnit = "Hours",
                            TrendName = "Sleep",
                            TotalOccurances = sleepOccurances.Count,
                        };
                        Trends.Add(SleepTrend);
                    } 
                    #endregion

                    #region Pick out substances
                    Dictionary<string, List<OccuranceModel>> substanceOccurances = new Dictionary<string, List<OccuranceModel>>();
                    foreach (SubstanceModel substance in data[ModelType.Substance])
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

                    foreach (List<OccuranceModel> occurances in substanceOccurances.Values)
                        occurances.OrderBy(x => x.Time);

                    foreach (KeyValuePair<string, List<OccuranceModel>> substanceOccurance in substanceOccurances)
                    {
                        List<TimeSpan> timebetween = new List<TimeSpan>();
                        for (int i = 1; i < substanceOccurance.Value.Count; i++)
                        {
                            timebetween.Add(substanceOccurance.Value[i].Time - substanceOccurance.Value[i - 1].Time);
                        }

                        Trends.Add(new TrendModel
                        {
                            Occurances = substanceOccurance.Value,
                            TotalOccurances = substanceOccurance.Value.Count(),
                            FirstTime = substanceOccurance.Value.First().Time,
                            LastTime = substanceOccurance.Value.Last().Time,
                            AverageTimeBetween = timebetween.Count>0 ? timebetween.Average(x => x.TotalHours) :0,
                            LongestTimeBetween = timebetween.Count>0 ? timebetween.Max().TotalHours : 0,
                            ShortestTimeBetween = timebetween.Count>0 ? timebetween.Min().TotalHours:0,
                            TrendContextTotal = substanceOccurance.Value.Sum(x => x.Ammount),
                            TrendContextUnit = substanceOccurance.Value.First().Unit,
                            TrendName = substanceOccurance.Key,
                            ShowExtendedData = true
                        });
                    }
                    #endregion

                    #region Activity Occurrences
                    Dictionary<string, List<OccuranceModel>> activityOccurances = new Dictionary<string, List<OccuranceModel>>();
                    foreach (ActivityModel activity in data[ModelType.Activity])
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
                        Trends.Add(new TrendModel
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
            }
            catch (Exception e)
            {
                await UserDialogs.Instance.AlertAsync(e.ToString(), "Could not load data");
            }
            finally
            {
                Loading = false;
            }
        }
    }
}
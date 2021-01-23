using SelfMonitoringApp.Models;
using SelfMonitoringApp.ViewModels.Base;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Linq;

using Xamarin.Forms;
using SelfMonitoringApp.Models.Base;

namespace SelfMonitoringApp.ViewModels
{
    public class DataExplorerViewModel : ViewModelBase, INavigationViewModel
    {
        public ObservableCollection<DaySummaryViewModel> DaySummaries { get; private set; }

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

        private bool _daySelect = false;
        public bool DaySelect
        {
            get => _daySelect;
            set
            {
                if (_daySelect == value)
                    return;

                _daySelect = value;
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

        private DaySummaryViewModel _selectedDay;
        public DaySummaryViewModel SelectedDay
        {
            get => _selectedDay;
            set
            {
                if (_selectedDay == value)
                    return;

                _selectedDay = value;
                NotifyPropertyChanged();

                ToggleDaySelect();
            }
        }

        private string _selectedMonth = DateTime.Now.ToString("MMMM");
        public string SelectedMonth
        {
            get => _selectedMonth;
            set
            {
                if (_selectedMonth == value)
                    return;

                _selectedMonth = value;
                NotifyPropertyChanged();
                LoadData().SafeFireAndForget(false);
            }
        }

        private string _selectedYear = DateTime.Now.ToString("yyyy");
        public string SelectedYear
        {
            get => _selectedYear;
            set
            {
                if (_selectedYear == value)
                    return;

                _selectedYear = value;
                NotifyPropertyChanged();
                LoadData().SafeFireAndForget(false);
            }
        }

        public Command LoadDataCommand { get; private set; }
        public Command DaySelectCommand { get; private set; }
        public DataExplorerViewModel()
        {
            LoadDataCommand = new Command(async () => await LoadData());
            DaySelectCommand = new Command(ToggleDaySelect);
            LoadData().SafeFireAndForget(false);
        }

        private void ToggleDaySelect()
        {
            DaySelect = !DaySelect;
        }

        public async Task<ObservableCollection<DaySummaryViewModel>> GetDateFilteredData(DateTime startDate, DateTime endDate)
        {
            var filteredData = new ObservableCollection<DaySummaryViewModel>();
            if (startDate <= endDate)
            {
                DateDisplay = $"From: {startDate:ddd dd MMM}\nTo: {endDate:ddd dd MMM}";

                Dictionary<ModelType, List<IModel>> datesInRange = await _database.GetAllModelsInSpanAsync(startDate, endDate);

                await Task.Factory.StartNew(() =>
                {
                    for (DateTime date = startDate; date.Date <= endDate.Date; date = date.AddDays(1))
                    {
                        List<SleepModel> dailySleeps = datesInRange[ModelType.Sleep].
                            Where(x => x.RegisteredTime.Date == date.Date).Cast<SleepModel>().ToList();

                        List<MoodModel> dailyMoods = datesInRange[ModelType.Mood].
                            Where(x => x.RegisteredTime.Date == date.Date).Cast<MoodModel>().ToList();

                        List<SubstanceModel> dailySubstances = datesInRange[ModelType.Substance].
                            Where(x => x.RegisteredTime.Date == date.Date).Cast<SubstanceModel>().ToList();

                        List<SocializationModel> dailySocials = datesInRange[ModelType.Socialization].
                            Where(x => x.RegisteredTime.Date == date.Date).Cast<SocializationModel>().ToList();

                        List<MealModel> dailyMeals = datesInRange[ModelType.Meal].
                            Where(x => x.RegisteredTime.Date == date.Date).Cast<MealModel>().ToList();

                        List<ActivityModel> dailyActivities = datesInRange[ModelType.Activity].
                            Where(x => x.RegisteredTime.Date == date.Date).Cast<ActivityModel>().ToList();

                        if (dailySleeps.Count != 0 || dailyMoods.Count != 0 || dailySubstances.Count != 0 || dailySocials.Count != 0 || dailyMeals.Count != 0 || dailyActivities.Count != 0)
                            filteredData.Add(new DaySummaryViewModel(dailySleeps, dailyActivities, dailyMeals, dailyMoods, dailySubstances, dailySocials, date));
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
                DateTime _startDate = DateTime.Parse($"{SelectedMonth} {SelectedYear}");
                int daysInMonth = DateTime.DaysInMonth(_startDate.Year, _startDate.Month);
                DateTime _endDate = _startDate.AddDays(daysInMonth - 1);
 
                ObservableCollection<DaySummaryViewModel> data = await GetDateFilteredData(_startDate, _endDate);

                NoData = data.Count == 0;

                //Let the render catch up after hiding / showing the warning
                await Task.Delay(50);

                DaySummaries = data;
                NotifyPropertyChanged(nameof(DaySummaries));

                SelectedDay = DaySummaries.Last();
                ToggleDaySelect();
            }
            catch(Exception ex)
            {

            }
            finally
            {
                Loading = false;
            }
        }
    }
}

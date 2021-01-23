using SelfMonitoringApp.Models;
using SQLite;

using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
using System;
using Splat;
using SelfMonitoringApp.Models.Base;
using System.Linq;

namespace SelfMonitoringApp.Services
{
    public class LocalSqlDatabaseService : IDatabaseService, IEnableLogger
    {
        public const string DatabaseFilename = "UserLogs.db3";

        private const SQLiteOpenFlags _openFlags = 
            SQLiteOpenFlags.ReadWrite |
            SQLiteOpenFlags.Create | 
            SQLiteOpenFlags.SharedCache;

        public static string FilePath
        {
            get
            {
                var basePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                return Path.Combine(basePath, DatabaseFilename);
            }
        }

        private SQLiteAsyncConnection _database;
        private static bool _initialized;

        public LocalSqlDatabaseService()
        {
            InitializeAsync().SafeFireAndForget(false);
        }

        public async Task InitializeAsync()
        {
            _database = new SQLiteAsyncConnection(FilePath, _openFlags);

            var tables = new Type[]
            {
                typeof(MoodModel),
                typeof(MealModel),
                typeof(SleepModel),
                typeof(SubstanceModel),
                typeof(ActivityModel),
                typeof(SocializationModel)
            };

            if (!_initialized)
            {
                await _database.CreateTablesAsync(CreateFlags.None, tables).ConfigureAwait(false);
                _initialized = true;
            }
        }

        public Task ClearSpecificDatabase(ModelType modelType)
        {
            switch (modelType)
            {
                case ModelType.Activity:
                    return _database.DeleteAllAsync<ActivityModel>();
                case ModelType.Meal:    
                    return _database.DeleteAllAsync<MealModel>();
                case ModelType.Mood:
                    return _database.DeleteAllAsync<MoodModel>();
                case ModelType.Substance:
                    return _database.DeleteAllAsync<SubstanceModel>();
                case ModelType.Sleep:
                    return _database.DeleteAllAsync<SleepModel>();
                case ModelType.Socialization:
                    return _database.DeleteAllAsync<SocializationModel>();
                default:
                    throw new ArgumentException("Model type does not exist in db");
            };
        }

        public Task<List<MoodModel>> GetMoodsAsync()            => _database.Table<MoodModel>().ToListAsync();
        public Task<List<MealModel>> GetMealsAsync()            => _database.Table<MealModel>().ToListAsync();
        public Task<List<SleepModel>> GetSleepsAsync()          => _database.Table<SleepModel>().ToListAsync();
        public Task<List<SubstanceModel>> GetSubstancesAsync()  => _database.Table<SubstanceModel>().ToListAsync();
        public Task<List<ActivityModel>> GetActivitiesAsync()   => _database.Table<ActivityModel>().ToListAsync();
        public Task<List<SocializationModel>> GetSocialsAsync() => _database.Table<SocializationModel>().ToListAsync();

        public async Task<Dictionary<ModelType, List<IModel>>> GetAllModelsInSpanAsync(DateTime startDay, DateTime endDate)
        {
            List<MoodModel> moods = null;
            List<MealModel> meals = null;
            List<SleepModel> sleeps = null;
            List<SubstanceModel> substances = null;
            List<ActivityModel> activities = null;
            List<SocializationModel> socials = null;

            await Task.WhenAll(new List<Task>()
            {
                Task.Run(async ()=> moods = await _database.Table<MoodModel>().ToListAsync()),
                Task.Run(async ()=> meals = await _database.Table<MealModel>().ToListAsync()),
                Task.Run(async ()=> sleeps = await _database.Table<SleepModel>().ToListAsync()),
                Task.Run(async ()=> substances = await _database.Table<SubstanceModel>().ToListAsync()),
                Task.Run(async ()=> activities = await _database.Table<ActivityModel>().ToListAsync()),
                Task.Run(async ()=> socials = await _database.Table<SocializationModel>().ToListAsync())
            });

            Dictionary<ModelType, List<IModel>> outputModels = new Dictionary<ModelType, List<IModel>>();
            await Task.Factory.StartNew(() =>
            {
                List<SleepModel> sortedSleeps
                    = (sleeps.Where(sleep => DateInRange(startDay, endDate, sleep.RegisteredTime))).ToList();
                List<MoodModel> sortedMoods
                    = (moods.Where(mood => DateInRange(startDay, endDate, mood.RegisteredTime))).ToList();
                List<MealModel> sortedMeals
                    = (meals.Where(meal => DateInRange(startDay, endDate, meal.RegisteredTime))).ToList();
                List<ActivityModel> sortedActivities
                    = (activities.Where(activity => DateInRange(startDay, endDate, activity.RegisteredTime))).ToList();
                List<SubstanceModel> sortedSubstances
                    = (substances.Where(substance => DateInRange(startDay, endDate, substance.RegisteredTime))).ToList();
                List<SocializationModel> sortedSocials
                    = (socials.Where(social => DateInRange(startDay, endDate, social.RegisteredTime))).ToList();

                if (sortedSleeps.Count > 0)
                    sortedSleeps.Sort((t1, t2) => DateTime.Compare(t1.RegisteredTime, t2.RegisteredTime));
                if (sortedMoods.Count > 0)
                    sortedMoods.Sort((t1, t2) => DateTime.Compare(t1.RegisteredTime, t2.RegisteredTime));
                if (sortedMeals.Count > 0)
                    sortedMeals.Sort((t1, t2) => DateTime.Compare(t1.RegisteredTime, t2.RegisteredTime));
                if (sortedActivities.Count > 0)
                    sortedActivities.Sort((t1, t2) => DateTime.Compare(t1.RegisteredTime, t2.RegisteredTime));
                if (sortedSubstances.Count > 0)
                    sortedSubstances.Sort((t1, t2) => DateTime.Compare(t1.RegisteredTime, t2.RegisteredTime));
                if (sortedSocials.Count > 0)
                    sortedSocials.Sort((t1, t2) => DateTime.Compare(t1.RegisteredTime, t2.RegisteredTime));
                
                outputModels.Add(ModelType.Sleep, sortedSleeps.Cast<IModel>().ToList());
                outputModels.Add(ModelType.Meal, sortedMeals.Cast<IModel>().ToList());
                outputModels.Add(ModelType.Mood, sortedMoods.Cast<IModel>().ToList());
                outputModels.Add(ModelType.Socialization, sortedSocials.Cast<IModel>().ToList());
                outputModels.Add(ModelType.Substance, sortedSubstances.Cast<IModel>().ToList());
                outputModels.Add(ModelType.Activity, sortedActivities.Cast<IModel>().ToList());
            });

            return outputModels;
        }

        private bool DateInRange(DateTime startDate, DateTime endDate, DateTime checkDate) =>
            (checkDate.Date >= startDate.Date && checkDate.Date <= endDate.Date);


        public Task<int> AddOrModifyModelAsync(IModel model)
        {
            try
            {
                if (model.ID != 0)
                    return _database.UpdateAsync(model);
                else
                    return _database.InsertAsync(model);
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                throw;
            }
        }
        public Task DeleteLog(IModel model) => _database.DeleteAsync(model);
    }
}
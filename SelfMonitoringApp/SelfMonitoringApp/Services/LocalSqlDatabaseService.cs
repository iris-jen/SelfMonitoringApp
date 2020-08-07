using SelfMonitoringApp.Models;
using SQLite;

using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
using System;
using Splat;
using SelfMonitoringApp.Models.Base;

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

        public Task<List<MoodModel>> GetMoodsAsync()           => _database.Table<MoodModel>().ToListAsync();
        public Task<List<MealModel>> GetMealsAsync()           => _database.Table<MealModel>().ToListAsync();
        public Task<List<SleepModel>> GetSleepsAsync()         => _database.Table<SleepModel>().ToListAsync();
        public Task<List<SubstanceModel>> GetSubstancesAsync() => _database.Table<SubstanceModel>().ToListAsync();
        public Task<List<ActivityModel>> GetActivitiesAsync()  => _database.Table<ActivityModel>().ToListAsync();
        public Task<List<SocializationModel>> GetSocialsAsync() => _database.Table<SocializationModel>().ToListAsync();


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
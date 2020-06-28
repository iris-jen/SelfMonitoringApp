using SelfMonitoringApp.Models;
using SQLite;

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace SelfMonitoringApp.Services
{
    public class Database  
    {
        public const string _databaseFilename = "UserLogs.db3";

        private const SQLiteOpenFlags _openFlags = 
            SQLiteOpenFlags.ReadWrite |
            SQLiteOpenFlags.Create | 
            SQLiteOpenFlags.SharedCache;

        private static string _filePath
        {
            get
            {
                var basePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                return Path.Combine(basePath, _databaseFilename);
            }
        }

        private static readonly Lazy<SQLiteAsyncConnection> lazyInitializer = new Lazy<SQLiteAsyncConnection>(() =>
        {
            return new SQLiteAsyncConnection(_filePath, _openFlags);
        });

        private SQLiteAsyncConnection _database => lazyInitializer.Value; 
        private static bool _initialized;

        public Database()
        {
            InitializeAsync().SafeFireAndForget(false);
        }

        private async Task InitializeAsync()
        {
            var tables = new Type[]
            {
                typeof(MoodModel),
                typeof(MealModel),
                typeof(SleepModel),
                typeof(SubstanceModel),
                typeof(ActivityModel)
            };

            if (!_initialized)
            {
                await _database.CreateTablesAsync(CreateFlags.None, tables).ConfigureAwait(false);
                _initialized = true;
            }
        }

        public Task ClearSpecificDatabase(ModelType modelType) => 
            modelType switch
        {
            ModelType.Activity => _database.DeleteAllAsync<ActivityModel>(),
            ModelType.Meal => _database.DeleteAllAsync<MealModel>(),
            ModelType.Mood => _database.DeleteAllAsync<MoodModel>(),
            ModelType.Substance => _database.DeleteAllAsync<SubstanceModel>(),
            ModelType.Sleep => _database.DeleteAllAsync<SleepModel>(),
            _ => throw new ArgumentException("Model type does not exist in db")
        };

        #region Get Items
        public Task<List<MoodModel>> GetMoodsAsync() => _database.Table<MoodModel>().ToListAsync();
        public Task<List<MealModel>> GetMealsAsync() => _database.Table<MealModel>().ToListAsync();
        public Task<List<SleepModel>> GetSleepsAsync() => _database.Table<SleepModel>().ToListAsync();
        public Task<List<SubstanceModel>> GetSubstancesAsync() => _database.Table<SubstanceModel>().ToListAsync();
        public Task<List<ActivityModel>> GetActivitiesAsync() => _database.Table<ActivityModel>().ToListAsync();
        #endregion

        public Task<int> AddOrModifyModelAsync(IModel model)
        {
            if (model.ID != 0)
                return _database.UpdateAsync(model);
            else
                return _database.InsertAsync(model);
        }
    }
}
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

        #region Get Items
        public Task<List<MoodModel>> GetMoodsAsync()
        {
            return _database.Table<MoodModel>().ToListAsync();
        }

        public Task<List<MealModel>> GetMealsAsync()
        {
            return _database.Table<MealModel>().ToListAsync();
        }

        public Task<List<SleepModel>> GetSleepsAsync()
        {
            return _database.Table<SleepModel>().ToListAsync();
        }

        public Task<List<SubstanceModel>> GetSubstancesAsync()
        {
            return _database.Table<SubstanceModel>().ToListAsync();
        }

        public Task<List<ActivityModel>> GetActivitiesAsync()
        {
            return _database.Table<ActivityModel>().ToListAsync();
        }
        #endregion

        #region Add Items
        public Task<int> AddOrModifyModelAsync(IModel model)
        {
            if (model.ID != 0)
                return _database.UpdateAsync(model);
            else
                return _database.InsertAsync(model);
        }

        #endregion
    }
}
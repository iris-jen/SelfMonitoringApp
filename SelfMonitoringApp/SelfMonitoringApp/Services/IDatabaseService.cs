using SelfMonitoringApp.Models;
using SelfMonitoringApp.Models.Base;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SelfMonitoringApp.Services
{
    public interface IDatabaseService
    {
        Task InitializeAsync();
        Task ClearSpecificDatabase(ModelType type);
        Task<int> AddOrModifyModelAsync(IModel model);
        Task<List<MoodModel>> GetMoodsAsync();
        Task<List<MealModel>> GetMealsAsync();
        Task<List<SleepModel>> GetSleepsAsync();
        Task<List<SubstanceModel>> GetSubstancesAsync();
        Task<List<ActivityModel>> GetActivitiesAsync();
        Task<List<SocializationModel>> GetSocialsAsync();
        Task<Dictionary<ModelType, List<IModel>>> GetAllModelsInSpanAsync(DateTime startDay, DateTime endDate);
        Task DeleteLog(IModel model);
    }
}

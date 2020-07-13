using SelfMonitoringApp.Models.Base;

namespace SelfMonitoringApp.Models
{
    public class MealModel : LogModelBase, IModel
    {
        public string MealSize     { get; set; }
        public string MealType     { get; set; }
        public string Description  { get; set; }
        public double Satisfaction { get; set; } 

        public MealModel() : base (ModelType.Meal) 
        {
            MealSize = MealType = Description = string.Empty;
        }
    }
}
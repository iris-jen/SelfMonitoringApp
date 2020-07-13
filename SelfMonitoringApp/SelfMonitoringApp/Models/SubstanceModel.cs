using SelfMonitoringApp.Models.Base;

namespace SelfMonitoringApp.Models
{
    public class SubstanceModel : LogModelBase, IModel
    {
        public string ConsumptionMethod { get; set; }
        public string SubstanceName     { get; set; }
        public string Comment           { get; set; } 
        public double Amount            { get; set; }
        public double Satisfaction      { get; set; }
        public string Unit              { get; set; }

        public SubstanceModel(): base(ModelType.Substance)
        {
            ConsumptionMethod = SubstanceName = Comment = Unit = string.Empty;
        }
    }
}
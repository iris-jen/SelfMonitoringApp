using System;

namespace SelfMonitoringApp.Models
{
    public class SubstanceModel : LogModelBase, IModel
    {
        /// <summary>
        /// How the user consumed the substance, smoked, drank etc
        /// </summary>
        public string ConsumptionMethod { get; set; } = string.Empty;

        /// <summary>
        /// What substance was consumed
        /// </summary>
        public string SubstanceName     { get; set; } = string.Empty;

        /// <summary>
        /// Comment entered by user, about how they feel etc
        /// </summary>
        public string Comment           { get; set; } = string.Empty;
        
        /// <summary>
        /// Amount of substance used
        /// </summary>
        public double Amount            { get; set; }


        public double Satisfaction      { get; set; }

        /// <summary>
        /// The unit of measurement for the substance used
        /// </summary>
        public string Unit              { get; set; } = string.Empty;

        public SubstanceModel(): base(ModelType.Sleep)
        {
              
        }
    }
}
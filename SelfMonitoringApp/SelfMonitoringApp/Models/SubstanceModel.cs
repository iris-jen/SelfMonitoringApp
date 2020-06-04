using System;

namespace SelfMonitoringApp.Models
{
    public class SubstanceModel
    {
        public DateTime RegisteredTime { get; set; }
        public string ConsumptionMethod { get; set; }
        public string SubstanceName { get; set; }
        public string Unit { get; set; }
        public string Comment { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public double Amount { get; set; }
    }
}
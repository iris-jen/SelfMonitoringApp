using SelfMonitoringApp.Models.Base;
using System;

namespace SelfMonitoringApp.Models
{
    public class SleepModel: LogModelBase , IModel
    {
        public DateTime SleepEndDate   { get; set; }
        public DateTime SleepStartDate { get; set; }
        public double TotalSleep       { get; set; }
        public bool RememberedDream    { get; set; }
        public bool Nightmare          { get; set; }
        public string DreamLog         { get; set; }
        public double RestRating       { get; set; }

        public SleepModel() : base(ModelType.Sleep)
        {
            DreamLog = string.Empty;

        }
    }
}
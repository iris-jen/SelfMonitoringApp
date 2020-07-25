using SelfMonitoringApp.Models.Base;
using System;

namespace SelfMonitoringApp.Models
{
    public class ActivityModel : LogModelBase, IModel
    {
        public string ActivityName { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public double Duration { get; set; }
        public double Enjoyment { get; set; }
        public bool Exersice { get; set; }
        public bool WantedToStart { get; set; }
        public string Comments { get; set; }


        public ActivityModel() : base(ModelType.Activity)
        {
            ActivityName = string.Empty;
            StartTime = EndTime = DateTime.Now;
        }
    }
}

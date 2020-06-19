using System;
using System.Collections.Generic;
using System.Text;

namespace SelfMonitoringApp.Models
{
    public class ActivityModel : LogModelBase, IModel
    {
        public string Description { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime   { get; set; }
        public double Duration    { get; set; }
        public double Enjoyment   { get; set; }
        public bool Exersice      { get; set; }
        public bool WantedToStart { get; set; }

        public ActivityModel() : base(ModelType.Activity)
        {

        }
    }
}

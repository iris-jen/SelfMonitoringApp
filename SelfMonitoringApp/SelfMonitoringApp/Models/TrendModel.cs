using System;
using System.Collections.Generic;
using System.Text;

namespace SelfMonitoringApp.Models
{
    public class TrendModel
    {
        public string TrendName { get; set; }

        // Amount of the item.
        public double TrendContextTotal { get; set; }

        public bool ShowExtendedData { get; set; }

        public double AverageTimeBetween { get; set; }
        public double ShortestTimeBetween { get; set; }
        public double LongestTimeBetween { get; set; }
        public DateTime FirstTime { get; set; }
        public DateTime LastTime { get; set; }

        public string TrendContextUnit { get; set; }

        public List<OccuranceModel> Occurances { get; set; }

        public double TotalOccurances { get; set; }

    }
}

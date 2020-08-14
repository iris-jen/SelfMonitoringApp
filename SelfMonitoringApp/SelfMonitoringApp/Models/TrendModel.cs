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

        public string TrendContextUnit { get; set; }

        public List<OccuranceModel> Occurances { get; set; }

        public double TotalOccurances { get; set; }

    }
}

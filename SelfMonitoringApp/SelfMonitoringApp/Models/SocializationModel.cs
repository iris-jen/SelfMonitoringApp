using SelfMonitoringApp.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace SelfMonitoringApp.Models
{
    public class SocializationModel : LogModelBase, IModel
    {
        public string SocializationType { get; set; }
        public string Location { get; set; }
        public bool WantedToSocialize { get; set; }
        public bool WasRemote { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public SocializationModel(): base(ModelType.Socialization)
        {
        }
    }
}

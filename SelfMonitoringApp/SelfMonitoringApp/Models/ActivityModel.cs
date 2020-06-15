using System;
using System.Collections.Generic;
using System.Text;

namespace SelfMonitoringApp.Models
{
    public class ActivityModel : LogModelBase, IModel
    {
        public ActivityModel() : base(ModelType.Activity)
        {

        }
    }
}

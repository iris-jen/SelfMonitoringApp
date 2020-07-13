using SelfMonitoringApp.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace SelfMonitoringApp.Models.Suggestions
{
    interface ISugestions
    {
        public ModelType ModelType { get; }
    }
}

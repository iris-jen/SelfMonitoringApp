using SelfMonitoringApp.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace SelfMonitoringApp.Services
{
    public interface ISugestionServices
    {
        ModelType ModelType { get; }
    }
}

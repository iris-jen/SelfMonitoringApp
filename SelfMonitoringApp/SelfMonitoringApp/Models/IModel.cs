using System;

namespace SelfMonitoringApp.Models
{
    public interface IModel
    {
        /// <summary>
        /// The time the model was registered by the user
        /// </summary>
       DateTime RegisteredTime { get; set; }
        
        /// <summary>
        /// Type of log
        /// </summary>
        ModelType LogType { get; }
    }
}
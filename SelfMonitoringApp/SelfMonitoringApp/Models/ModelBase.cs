using System;

namespace SelfMonitoringApp.Models
{
    public abstract class ModelBase
    {
        /// <summary>
        /// The time the model was registered by the user
        /// </summary>
        public DateTime RegisteredTime { get; set; }
        
        /// <summary>
        /// Type of log
        /// </summary>
        public LogType LogType { get; }

        protected ModelBase(LogType type)
        {
            LogType = type;
        }
    }
}
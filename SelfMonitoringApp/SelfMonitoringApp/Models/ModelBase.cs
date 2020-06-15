using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace SelfMonitoringApp.Models
{
    public abstract class LogModelBase
    {
        /// <summary>
        /// The time the model was registered by the user
        /// </summary>
        public DateTime RegisteredTime { get; set; }
        
        /// <summary>
        /// Type of log
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]public ModelType LogType { get; }

        protected LogModelBase(ModelType type)
        {
            LogType = type;
        }
    }
}
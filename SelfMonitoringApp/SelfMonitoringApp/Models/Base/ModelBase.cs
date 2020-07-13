using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using SQLite;
using System;

namespace SelfMonitoringApp.Models.Base
{
    public abstract class LogModelBase
    {
        /// <summary>
        /// The time the model was registered by the user
        /// </summary>
        public DateTime RegisteredTime { get; set; }

        /// <summary>
        /// SQL key
        /// </summary>
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        
        /// <summary>
        /// Type of log
        /// </summary>
        public ModelType LogType { get; }

        protected LogModelBase(ModelType type)
        {
            LogType = type;
        }
    }
}
using System;

namespace SelfMonitoringApp.Models
{
    public class SleepModel: LogModelBase , IModel
    {
        /// <summary>
        /// When the user indicated they fell asleep
        /// </summary>
        public TimeSpan SleepStart { get; set; }
        
        /// <summary>
        /// When the user indicated they awoke
        /// </summary>
        public TimeSpan SleepEnd { get; set; }

        /// <summary>
        /// Total amount of sleep in hours
        /// </summary>
        public double TotalSleep { get; set; }
        
        /// <summary>
        /// True if the user remembered their dream
        /// </summary>
        public bool RememberedDream  { get; set; }

        
        /// <summary>
        /// True if the user experienced a vivid dream
        /// </summary>
        public bool VividDream { get; set; }
        
        /// <summary>
        /// True if the user had a nightmare
        /// </summary>
        public bool Nightmare { get; set; }
        
        /// <summary>
        /// What the user remembers about their dream
        /// </summary>
        public string DreamLog { get; set; }
        
        /// <summary>
        /// How rested the user felt after their sleep out of 10
        /// </summary>
        public double RestRating { get; set; }

        public SleepModel() : base(LogType.Sleep)
        {
            
        }
    }
}
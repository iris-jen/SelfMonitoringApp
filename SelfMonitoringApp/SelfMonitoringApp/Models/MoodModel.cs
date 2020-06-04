using System;

namespace SelfMonitoringApp.Models
{
    public class MoodModel : ModelBase, IModel
    {
        /// <summary>
        /// What was happening at the time of the logs creation
        /// </summary>
        public string Description { get; set; }
        
        /// <summary>
        /// Specific emotion experienced
        /// </summary>
        public string Emotion { get; set; }
        
        /// <summary>
        /// A numerical rating (out of 10) of how the user felt
        /// </summary>
        public double OverallMood { get; set; }

        public MoodModel(): base(LogType.Mood) { }
    }
}
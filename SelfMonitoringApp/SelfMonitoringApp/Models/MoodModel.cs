using System;
using System.Collections.Generic;

namespace SelfMonitoringApp.Models
{
    public class MoodModel : LogModelBase, IModel
    {
        /// <summary>
        /// What was happening at the time of the logs creation
        /// </summary>
        public string Description { get; set; }
        
        /// <summary>
        /// Emotions that the user experienced
        /// </summary>
        public List<string> Emotions { get; set; }
        
        /// <summary>
        /// A numerical rating (out of 10) of how the user felt
        /// </summary>
        public double OverallMood { get; set; }

        public MoodModel(): base(ModelType.Mood) { }

    }
}
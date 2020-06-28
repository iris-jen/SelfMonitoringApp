using System;
using System.Collections.Generic;

namespace SelfMonitoringApp.Models
{
    public class MoodModel : LogModelBase, IModel
    {
        /// <summary>
        /// What was happening at the time of the logs creation
        /// </summary>
        public string Description    { get; set; } = string.Empty;

        /// <summary>
        /// Strongest emotion that the user experienced
        /// </summary>
        public string StrongestEmotion { get; set; } = string.Empty;

        /// <summary>
        /// Location it happened
        /// </summary>
        public string Location { get; set; } = string.Empty;

        /// <summary>
        /// A numerical rating (out of 10) of how the user felt
        /// </summary>
        public double OverallMood    { get; set; }

        public MoodModel(): base(ModelType.Mood) { }

    }
}
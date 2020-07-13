using SelfMonitoringApp.Models.Base;
using System.Collections.Generic;

namespace SelfMonitoringApp.Models.Suggestions
{
    public class MoodSugestions : ISugestions
    {
        public ModelType ModelType
        {
            get
            {
                return ModelType.Mood;
            }
        }

        public List<string> StrongestEmotion { get; set; }
    }
}

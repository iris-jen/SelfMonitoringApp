using SelfMonitoringApp.Models.Base;

namespace SelfMonitoringApp.Models
{
    public class MoodModel : LogModelBase, IModel
    {
        public string Description      { get; set; }
        public string StrongestEmotion { get; set; }
        public string Location         { get; set; }
        public double OverallMood      { get; set; }

        public MoodModel(): base(ModelType.Mood) 
        {
            Description = StrongestEmotion = Location = string.Empty;
        }
    }
}   
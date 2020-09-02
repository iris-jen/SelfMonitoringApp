using SelfMonitoringApp.Models.Base;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace SelfMonitoringApp.Models
{
    public class NotificationModel : IModel
    {
        public ModelType LogType
        {
            get
            {
                return ModelType.Notification;
            }
        }

        public ModelType ModelTarget { get; set; }

        public TimeSpan Interval { get; set; }

        public string Message { get; set; }

        public bool Vibrate { get; set; }

        public bool OpenToPage { get; set; }

        public bool Silent { get; set; }

        public int ID { get; set; }

        public bool IsPeriodic { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }
        
        public DateTime RegisteredTime { get; set; }
        
        public List<TimeSpan> ReminderTimes { get; set; }
    }
}
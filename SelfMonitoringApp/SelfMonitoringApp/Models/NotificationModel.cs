using SelfMonitoringApp.Models.Base;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace SelfMonitoringApp.Models
{
    public class NotificationModel
    {
        public NotificationType NotificationType { get; set; }

        public ModelType ModelTarget { get; set; }

        public TimeSpan Interval { get; set; }

        public string Message { get; set; }

        public bool Vibrate { get; set; }

        public bool OpenToPage { get; set; }

        public bool Silent { get; set; }

        public int Key { get; set; }

        //Time the periodic notifications
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        
        public List<DateTime> ReminderTimes { get; set; }
    }
}

using SelfMonitoringApp.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace SelfMonitoringApp.Models
{
    public enum NotificationType
    {
        PereocdicDaily,
        OneTimeDaily,

        // Two kinds of "notifications" one to remind the user to fill out a log.
        // The other to place constraint or incentives on behaviors, i.e, try and get x exercise hrs per day, < 100 cigs / hour, etc.
        DailyLimit,
        HourlyLimit,
    }

    public enum ConstraintCondition
    {
        GreaterThan,
        LessThan,
        None,
        Equal
    }

    public class NotificationModel
    {
        public NotificationType Type { get; set; }

        public ModelType ModelTarget { get; set; }


        /// <summary>
        // Item to be constrained, i.e a text match field
        /// </summary>
        public string ConstraintSubject { get; set; }

        public double ConstraintAmmount { get; set; }

        public ConstraintCondition ConstraintCondition { get; set; }

        public TimeSpan Interval { get; set; }
    }
}

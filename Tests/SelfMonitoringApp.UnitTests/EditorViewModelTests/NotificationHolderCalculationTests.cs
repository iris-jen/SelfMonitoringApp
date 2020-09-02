using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting.Logging;
using Moq;
using SelfMonitoringApp.Models;
using SelfMonitoringApp.Services;
using SelfMonitoringApp.Services.Navigation;
using SelfMonitoringApp.ViewModels;
using SelfMonitoringApp.ViewModels.Logs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace SelfMonitoringApp.UnitTests
{
    [TestClass]
    public class NotificationHolderCalculationTests
    {
        private NotificationHolder _periodicNotificationHolder;
        private NotificationHolder _notificationHolder;
        private NotificationModel _periodicNotification;
        private NotificationModel _notification;

        private readonly int periodicStartHour = 8;
        private readonly int periodicEndHour = 20;
        private readonly int periodicStartMinute = 0;
        private readonly int periodicEndMinute = 0;
        private readonly int periodicIntervalHour = 1;
        private readonly int periodicIntervalMinute = 0;

        [TestInitialize]
        public void Init()
        {
            _notification = new NotificationModel()
            {
                ReminderTimes = new List<TimeSpan>()
                {
                    new TimeSpan(hours: 8, minutes: 30, seconds: 0),
                    new TimeSpan(hours: 10, minutes: 30, seconds: 0),
                    new TimeSpan(hours: 13, minutes: 20, seconds: 2),
                    new TimeSpan(hours: 18, minutes: 30, seconds: 0),
                }
            };

            _periodicNotification = new NotificationModel()
            {
                Interval = new TimeSpan(hours: periodicIntervalHour, minutes: periodicIntervalMinute, seconds: 0),
                StartTime = new TimeSpan(hours: periodicStartHour, minutes: periodicStartMinute, seconds: 0),
                EndTime = new TimeSpan(hours: periodicEndHour, minutes: periodicEndMinute, seconds: 0),
                IsPeriodic = true
            };
        }

        [TestMethod]
        public void TestTimeCalculations()
        {
            int totalDays = 3;
            _periodicNotificationHolder = new NotificationHolder(0, _periodicNotification, totalDays);
  
            foreach(var kvp in _periodicNotificationHolder.SystemKeys)
            {
                Debug.WriteLine($"Periodic ---> key - {kvp.Key} Time {kvp.Value:yy/MM/dd @ hh:mm tt}");
            }

            _notificationHolder = new NotificationHolder(300, _notification, totalDays);
            foreach (var kvp in _notificationHolder.SystemKeys)
            {
                Debug.WriteLine($"Non Periodic ---> key - {kvp.Key} Time {kvp.Value:yy/MM/dd @ hh:mm tt}");
            }

        }

        [TestCleanup]
        public void Cleanup()
        {
            _notificationHolder = null;
        }
    }
}

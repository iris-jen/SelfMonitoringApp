using System;

namespace SelfMonitoringApp.Services
{
    interface INotificationManagerService
    {
        event EventHandler NotificationReceived;
        void Initialize();
        int ScheduleNotification(string title, string message);
        int ReceiveNotification(string title, string message);
    }
}
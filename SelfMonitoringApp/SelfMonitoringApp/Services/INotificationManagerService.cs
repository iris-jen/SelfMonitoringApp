using SelfMonitoringApp.Models;
using System;

namespace SelfMonitoringApp.Services
{
    interface INotificationManagerService
    {
        void RemoveNotification(NotificationModel model);
        void AddOrUpdateNotification(NotificationModel model);
    }
}
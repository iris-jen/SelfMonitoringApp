using Newtonsoft.Json;
using Plugin.LocalNotifications;
using SelfMonitoringApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SelfMonitoringApp.Services
{
    public class NotificationManagerService : INotificationManagerService
    {
        public const string DatabaseFilename = "UserNotifications.json";

        /// <summary>
        /// contains all registered notification models
        /// </summary>
        private List<NotificationHolder> _notifications;
 
        public static string FilePath
        {
            get
            {
                var basePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                return Path.Combine(basePath, DatabaseFilename);
            }
        }

        public NotificationManagerService()
        {
            if (File.Exists(FilePath))
            {
                _notifications = JsonConvert.DeserializeObject<List<NotificationHolder>>(
                        File.ReadAllText(FilePath));
            }
            else
            {
                Save();
            }
        }
        
        public void Save()
        {
            File.WriteAllText(FilePath, JsonConvert.SerializeObject(_notifications, Formatting.Indented));        
        }

        public void AddOrUpdateNotification(NotificationModel model)
        {
            if(model.ID ==0)
            {
                // Determine how many notifications we need to schedule
                if (model.IsPeriodic)
                {

                        
                }          
            }
            else
            {

            }
            Save();
        }

        public void RemoveNotification(NotificationModel model)
        {
            Save();
        }

    }
}

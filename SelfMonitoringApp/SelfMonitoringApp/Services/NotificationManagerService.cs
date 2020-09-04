using Newtonsoft.Json;
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
        private Dictionary<int,NotificationHolder> _notifications;

        private const int HolderGap = 200;
        private const int DaysToGenerate = 3;

        public static string FilePath
        {
            get
            {
                string basePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                return Path.Combine(basePath, DatabaseFilename);
            }
        }

        public NotificationManagerService()
        {
            if (File.Exists(FilePath))
            {
                string fileContents = File.ReadAllText(FilePath);

                File.Delete(FilePath);

                //if (!string.IsNullOrEmpty(fileContents))
                //{
                //    _notifications = JsonConvert.DeserializeObject<Dictionary<int, NotificationHolder>>(
                //            File.ReadAllText(FilePath));
                //    return;
                //}
            }
            _notifications = new Dictionary<int, NotificationHolder>();
            Save();   
        }
        
        public void Save()
        {
            File.WriteAllText(FilePath, JsonConvert.SerializeObject(_notifications, Formatting.Indented));        
        }

        public void AddOrUpdateNotification(NotificationModel model)
        {
            if(model.ID == 0)
            {
                // if theres nothing in the dictionary just set first key to 100
                int newKey = _notifications.Count > 0 ? _notifications.Keys.Max() + HolderGap : 100;
                
                model.ID = newKey;

                NotificationHolder holder = new NotificationHolder();
                var keys = holder.CalculateNotificationTimes(newKey, model, DaysToGenerate);
                _notifications.Add(newKey, holder);

                foreach(KeyValuePair<int,DateTime> timeSets in keys)
                {
                    //Todo: create your own implementation of this.
                    //CrossLocalNotifications.Current.Show($"Time to log your {model.LogType}!", model.Message,
                    //    timeSets.Key, timeSets.Value);
                }
            }
            else
            {
                NotificationHolder holder = _notifications[model.ID];

                foreach (int key in holder.SystemKeys.Keys)
                {
                    //Todo: create your own implementation of this.
                    //CrossLocalNotifications.Current.Cancel(key);
                }

                foreach (KeyValuePair<int, DateTime> timeSets in holder.SystemKeys)
                {

                    //Todo: create your own implementation of this.
                    //CrossLocalNotifications.Current.Show($"Time to log your {model.LogType}!",
                    //    model.Message, timeSets.Key, timeSets.Value);
                }
        
            }
            Save();
        }

        public void RemoveNotification(NotificationModel model)
        {
            Save();
        }

    }
}

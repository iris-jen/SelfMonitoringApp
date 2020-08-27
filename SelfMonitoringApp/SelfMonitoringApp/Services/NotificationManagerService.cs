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

        private List<NotificationModel> _notifications;

        private Random _keyGen;

        public static string FilePath
        {
            get
            {
                var basePath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                return Path.Combine(basePath, DatabaseFilename);
            }
        }

        public NotificationManagerService()
        {
            _keyGen = new Random(420);

            if (File.Exists(FilePath))
            {
                _notifications = JsonConvert.DeserializeObject<List<NotificationModel>>(File.ReadAllText(FilePath));
            }
            else
            {
                _notifications = new List<NotificationModel>();
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
                int newKey = _keyGen.Next();
                model.ID = newKey;
                _notifications.Add(model);
            }
            else
            {
                NotificationModel lastModel = _notifications.FirstOrDefault(x => x.ID == model.ID);
                
                if(lastModel!=null)
                    _notifications.Remove(lastModel);

                _notifications.Add(model);
            }
            Save();
        }

        public void RemoveNotification(NotificationModel model)
        {
            _notifications.Remove(model);
            Save();
        }

    }
}

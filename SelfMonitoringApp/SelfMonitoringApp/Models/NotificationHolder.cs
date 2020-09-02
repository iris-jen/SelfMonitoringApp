using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;

namespace SelfMonitoringApp.Models
{
    public class NotificationHolder
    {
        public NotificationModel Notification{get;set;}

        public Dictionary<int, DateTime> SystemKeys { get; private set; }

        public NotificationHolder(int id, NotificationModel model, int totalDays)
        {
            CalculateNotificationTimes(id, model, totalDays);
        }

        public void CalculateNotificationTimes(int baseId, NotificationModel model, int totalDays)
        {
            SystemKeys = new Dictionary<int, DateTime>();

            DateTime now = DateTime.Now;
            DateTime baseTime = new DateTime(now.Year,now.Month,now.Day,
                model.StartTime.Hours,model.StartTime.Minutes,model.StartTime.Seconds);

            //How many keys are allocated per day
            int keyOffset = 30;
            int keyRunner = 0;

            if (model.IsPeriodic)
            {
                TimeSpan notificationSpan = model.EndTime - model.StartTime;
                int occurances = (int)(notificationSpan.TotalHours / model.Interval.TotalHours);
               
                for (int day = 0; day < totalDays; day++)
                {
                    for (int i = 0; i < occurances; i++)
                    {
                        //Add an the interval time to the base time
                        DateTime notificationTime = i == 0 ? 
                            baseTime.AddHours(model.Interval.TotalHours) : baseTime;

                        //Designate the key for this time
                        SystemKeys.Add(baseId + i + keyRunner,
                            notificationTime);
                    }
                    // increment key by day offset
                    keyRunner += keyOffset;
                    baseTime = baseTime.AddDays(1);
                }
            }
            else
            {
                for(int day = 0; day<totalDays;day++)
                {
                    for(int i = 0; i< model.ReminderTimes.Count; i++)
                    {
                        TimeSpan time = model.ReminderTimes[i];
                        // create a new date time from the indicated time
                        SystemKeys.Add(baseId + i + keyOffset, new DateTime(baseTime.Year, baseTime.Month, baseTime.Day,
                            time.Hours, time.Minutes, time.Seconds));
                    }
                    // increment key by day offset
                    keyRunner += keyOffset;
                    baseTime = baseTime.AddDays(1);
                }
            }
        }

    }
}   

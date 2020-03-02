using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Notifications.Android;

namespace SuperTanks.Core
{
    public class MobileNotificationAndroid : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            CreateChannel();
            SendBossNotification();
        }

        private void CreateChannel()
        {
            AndroidNotificationChannel channel = new AndroidNotificationChannel()
            {
                Id = "call back",
                Name = "Call back channel",
                Description = "channel to call players back",
                Importance = Importance.Default
            };

            AndroidNotificationCenter.RegisterNotificationChannel(channel);
        }

        private void SendReturnNotification()
        {
            AndroidNotification notification = new AndroidNotification()
            {
                Title = "Time's up!",
                Text = "Time to blow up some enemies",
                LargeIcon = "grey_tank_large",
                FireTime = System.DateTime.Now.AddHours(2)
            };

            var identifier = AndroidNotificationCenter.SendNotification(notification, "call back");

            if (AndroidNotificationCenter.CheckScheduledNotificationStatus(identifier) == NotificationStatus.Delivered)
            {
                SendBossNotification();
            }
        }

        private void SendBossNotification()
        {
            AndroidNotification bossNotification = new AndroidNotification()
            {
                Title = "Boss! Boss!",
                Text = "The Boss is waiting for you",
                LargeIcon = "black_tank_large",
                FireTime = System.DateTime.Now.AddHours(3)
            };

            AndroidNotificationCenter.SendNotification(bossNotification, "call back");
        }

        private void OnApplicationQuit()
        {
            SendReturnNotification();
        }
    }
}

using System;
using UnityEngine;

#if UNITY_ANDROID && !UNITY_EDITOR

using System.Linq;

#endif

namespace Assets.SimpleAndroidNotifications
{
    public static class NotificationManager
    {
        //private const string FullClassName = "com.hippogames.simpleandroidnotifications.Controller";
        #if UNITY_ANDROID && !UNITY_EDITOR
        
        private const string FullClassName = "com.LiMiDyFy.NotificationsController";
        private const string MainActivityClassName = "com.unity3d.player.UnityPlayerActivity";

        #endif
        
        public static int Send(TimeSpan delay, string title, string message, Color smallIconColor, NotificationIcon smallIcon = 0)
        {
            return SendCustom(new NotificationParams
            {
                Id = UnityEngine.Random.Range(0, int.MaxValue),
                Delay = delay,
                Title = title,
                Message = message,
                Ticker = message,
                Sound = true,
                Vibrate = true,
                Light = true,
                SmallIcon = smallIcon,
                SmallIconColor = smallIconColor,
                LargeIcon = ""
            });
        }
        
        public static int SendWithAppIcon(TimeSpan delay, string title, string message, Color smallIconColor, NotificationIcon smallIcon = 0)
        {
            return SendCustom(new NotificationParams
            {
                Id = UnityEngine.Random.Range(0, int.MaxValue),
                Delay = delay,
                Title = title,
                Message = message,
                Ticker = message,
                Sound = true,
                Vibrate = true,
                Light = true,
                SmallIcon = smallIcon,
                SmallIconColor = smallIconColor,
                LargeIcon = "app_icon"
            });
        }

        public static int SendCustom(NotificationParams notificationParams)
        {
            #if UNITY_ANDROID && !UNITY_EDITOR

            var p = notificationParams;
            var delay = (long) p.Delay.TotalMilliseconds;

            new AndroidJavaClass(FullClassName).CallStatic("SetNotification", p.Id, delay, p.Title, p.Message, p.Ticker,
                p.Sound ? 1 : 0, p.Vibrate ? 1 : 0, p.Light ? 1 : 0, p.LargeIcon, GetSmallIconName(p.SmallIcon), ColotToInt(p.SmallIconColor), MainActivityClassName);

            #endif

            return notificationParams.Id;
        }
        
        public static void Cancel(int id)
        {
            #if UNITY_ANDROID && !UNITY_EDITOR

            new AndroidJavaClass(FullClassName).CallStatic("CancelScheduledNotification", id);

            #endif
        }

        public static void CancelAll()
        {
            #if UNITY_ANDROID && !UNITY_EDITOR

            new AndroidJavaClass(FullClassName).CallStatic("CancelAllScheduledNotifications");

            #endif
        }

        private static int ColotToInt(Color color)
        {
            var smallIconColor = (Color32) color;
            
            return smallIconColor.r * 65536 + smallIconColor.g * 256 + smallIconColor.b;
        }

        private static string GetSmallIconName(NotificationIcon icon)
        {
            return "anp_" + icon.ToString().ToLower();
        }
    }
}
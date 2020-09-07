using System.Collections;
using UnityEngine;
using System;

namespace Assets.SimpleAndroidNotifications
{
    public class NotificationsController : MonoBehaviour
    {
        public Data data;
        private ColorsGame mainColors;

        public void Start()
        {
            StartCoroutine(Started());
        }

        public IEnumerator Started()
        {
            yield return new WaitForSeconds(1f);
            mainColors = data.GetColors();
            NotificationsActive();
        }

        public void NotificationsActive()
        {
            NotificationManager.CancelAll();
            if (PlayerPrefs.GetString("Notifications") == "off") { return; }
            NotificationManager.SendWithAppIcon(TimeSpan.FromDays(1), "", "", mainColors.colorBackGround, NotificationIcon.Message);
            NotificationManager.SendWithAppIcon(TimeSpan.FromDays(2), "", "", mainColors.colorTouches, NotificationIcon.Heart);
            NotificationManager.SendWithAppIcon(TimeSpan.FromDays(3), "", "", mainColors.colorGraphs, NotificationIcon.Clock);
            NotificationManager.SendWithAppIcon(TimeSpan.FromDays(7), "", "", mainColors.colorLines, NotificationIcon.Bell);
            NotificationManager.SendWithAppIcon(TimeSpan.FromDays(30), "", "", mainColors.colorError, NotificationIcon.Star);
            return;
        }
    }
}
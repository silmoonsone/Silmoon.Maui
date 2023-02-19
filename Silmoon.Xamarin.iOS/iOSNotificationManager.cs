using Foundation;
using Newtonsoft.Json.Linq;
using Silmoon.Xamarin.Interfaces;
using Silmoon.Xamarin.iOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIKit;
using UserNotifications;
using Xamarin.Forms;

[assembly: Dependency(typeof(iOSNotificationManager))]
namespace Silmoon.Xamarin.iOS
{
    public class iOSNotificationManager : INotificationManager
    {
        bool hasNotificationsPermission;
        public event Action<NotificationEventArgs> NotificationReceived;
        public event Action<string> OnDeviceTokenReceived;
        public event Action<bool> OnNotificationPermissionResult;

        public iOSNotificationManager()
        {

        }
        public void Initialize()
        {
            UNUserNotificationCenter.Current.RequestAuthorization(UNAuthorizationOptions.Sound | UNAuthorizationOptions.Badge | UNAuthorizationOptions.Alert | UNAuthorizationOptions.Announcement, (approved, err) =>
            {
                hasNotificationsPermission = approved;
                OnNotificationPermissionResult?.Invoke(approved);
            });
        }

        public bool SendNotification(string title, string subTitle, string message, DateTime? notifyTime = null)
        {
            if (!hasNotificationsPermission) return false;

            var content = new UNMutableNotificationContent()
            {
                Title = title,
                Subtitle = subTitle,
                Body = message,
                Badge = 1
            };

            UNNotificationTrigger trigger;
            if (notifyTime != null)
            {
                // Create a calendar-based trigger.
                trigger = UNCalendarNotificationTrigger.CreateTrigger(GetNSDateComponents(notifyTime.Value), false);
            }
            else
            {
                // Create a time-based trigger, interval is in seconds and must be greater than 0.
                trigger = UNTimeIntervalNotificationTrigger.CreateTrigger(0.25, false);
            }

            var request = UNNotificationRequest.FromIdentifier(DateTime.Now.Ticks.ToString(), content, trigger);
            UNUserNotificationCenter.Current.AddNotificationRequest(request, (err) =>
            {
                if (err != null)
                {
                    throw new Exception($"Failed to schedule notification: {err}");
                }
            });
            return true;
        }
        public void SetBadgeNumber(int number)
        {
            UIApplication.SharedApplication.ApplicationIconBadgeNumber = number;
        }
        public void onReceiveNotification(string title, string subTitle, string message, ReceiveType type, string identifier, JObject data, PushPlatform pushPlatform)
        {
            var args = new NotificationEventArgs()
            {
                Title = title,
                SubTitle = subTitle,
                Message = message,
                Type = type,
                Identifier = identifier,
                Data = data,
                PushPlatform = pushPlatform
            };
            NotificationReceived?.Invoke(args);
        }
        public void onReceiveDeviceToken(string deviceToken)
        {
            OnDeviceTokenReceived?.Invoke(deviceToken);
        }


        NSDateComponents GetNSDateComponents(DateTime dateTime)
        {
            return new NSDateComponents
            {
                Month = dateTime.Month,
                Day = dateTime.Day,
                Year = dateTime.Year,
                Hour = dateTime.Hour,
                Minute = dateTime.Minute,
                Second = dateTime.Second
            };
        }
    }
    public class iOSNotificationReceiver : UNUserNotificationCenterDelegate
    {
        public override void WillPresentNotification(UNUserNotificationCenter center, UNNotification notification, Action<UNNotificationPresentationOptions> completionHandler)
        {
            NSError error;
            var json = NSJsonSerialization.Serialize(notification.Request.Content.UserInfo, 0, out error).ToString();
            JObject data = null;
            if (error == null) data = JObject.Parse(json);
            string title = notification.Request.Content.Title;
            string subTitle = notification.Request.Content.Subtitle;
            string message = notification.Request.Content.Body;
            string identifier = notification.Request.Identifier;
            DependencyService.Get<INotificationManager>().onReceiveNotification(title, subTitle, message, ReceiveType.NotificationListen, identifier, data, PushPlatform.iOS);

            completionHandler(UNNotificationPresentationOptions.Alert);
        }
        public override void DidReceiveNotificationResponse(UNUserNotificationCenter center, UNNotificationResponse response, Action completionHandler)
        {
            if (response.IsDefaultAction)
            {
                NSError error;
                var json = NSJsonSerialization.Serialize(response.Notification.Request.Content.UserInfo, 0, out error).ToString();
                JObject data = null;
                if (error == null) data = JObject.Parse(json);

                string title = response.Notification.Request.Content.Title;
                string subTitle = response.Notification.Request.Content.Subtitle;
                string message = response.Notification.Request.Content.Body;
                string identifier = response.Notification.Request.Identifier;
                DependencyService.Get<INotificationManager>().onReceiveNotification(title, subTitle, message, ReceiveType.NotificationClicked, identifier, data, PushPlatform.iOS);
            }
            completionHandler();
        }
    }
}
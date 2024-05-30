using Foundation;
using Newtonsoft.Json.Linq;
using Silmoon.Maui.Services.PushNotifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIKit;
using UserNotifications;

namespace Silmoon.Maui.Services.NotificationManager
{
    public class NotificationManagerService : INotificationManagerService
    {
        public event Action<NotificationEventArgs> OnNotificationReceived;
        public event Action<string> OnDeviceTokenReceived;
        public event Action<bool> OnNotificationPermissionResult;

        public string DeviceToken { get; private set; }
        public bool IsNotificationPermissionGranted { get; private set; }

        public void Initialize()
        {
            UNUserNotificationCenter.Current.Delegate = new iOSNotificationReceiver(this);
            UNUserNotificationCenter.Current.RequestAuthorization(UNAuthorizationOptions.Sound | UNAuthorizationOptions.Badge | UNAuthorizationOptions.Alert, (approved, err) =>
            {
                UIApplication.SharedApplication.InvokeOnMainThread(() => UIApplication.SharedApplication.RegisterForRemoteNotifications());
                IsNotificationPermissionGranted = approved;
                OnNotificationPermissionResult?.Invoke(approved);
            });
        }

        public bool SendNotification(string title, string subTitle, string message, DateTime? notifyTime = null)
        {
            if (!IsNotificationPermissionGranted) return false;

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
#pragma warning disable CA1416 // Validate platform compatibility
            UNUserNotificationCenter.Current.SetBadgeCountAsync(number);
#pragma warning restore CA1416 // Validate platform compatibility
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
            OnNotificationReceived?.Invoke(args);
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
}

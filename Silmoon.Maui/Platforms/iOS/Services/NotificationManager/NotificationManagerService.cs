using Foundation;
using Newtonsoft.Json.Linq;
using Silmoon.Maui.Enums;
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
        public event Func<NotificationEventArgs, NotificationBehaviorType?> OnNotificationReceived;
        public event Action<NotificationEventArgs> OnNotificationClicked;
        public event Action<string> OnDeviceTokenReceived;
        public event Action<bool> OnNotificationPermissionResult;

        public string DeviceToken { get; private set; }
        public bool IsNotificationPermissionGranted { get; private set; }

        public bool Initialize()
        {
            UNUserNotificationCenter.Current.Delegate = new iOSNotificationReceiver(this);
            UNUserNotificationCenter.Current.RequestAuthorization(UNAuthorizationOptions.Sound | UNAuthorizationOptions.Badge | UNAuthorizationOptions.Alert, (approved, err) =>
            {
                UIApplication.SharedApplication.InvokeOnMainThread(() => UIApplication.SharedApplication.RegisterForRemoteNotifications());
                IsNotificationPermissionGranted = approved;
                OnNotificationPermissionResult?.Invoke(approved);
            });
            return true;
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
        public bool SetBadgeNumber(int number)
        {
#pragma warning disable CA1416 // Validate platform compatibility
            UNUserNotificationCenter.Current.SetBadgeCountAsync(number);
            return true;
#pragma warning restore CA1416 // Validate platform compatibility
        }
        /// <summary>
        /// 这里返回的NotificationBehaviorType类型表示如果应用在前台的时候收到了通知，应该如何处理。有的情况下应用在前台收到通知不应该弹出通知，而是直接处理。如果返回null，会使用默认的处理方式（List、Banner）。
        /// </summary>
        /// <param name="title"></param>
        /// <param name="subTitle"></param>
        /// <param name="message"></param>
        /// <param name="identifier"></param>
        /// <param name="data"></param>
        /// <param name="pushPlatform"></param>
        /// <returns></returns>
        public NotificationBehaviorType? onReceiveNotification(string title, string subTitle, string message, string identifier, JObject data, PushPlatform pushPlatform)
        {
            var args = new NotificationEventArgs()
            {
                Title = title,
                SubTitle = subTitle,
                Message = message,
                Identifier = identifier,
                Data = data,
                PushPlatform = pushPlatform,
            };
            return OnNotificationReceived?.Invoke(args);
        }
        public void onClickNotification(string title, string subTitle, string message, string identifier, JObject data, PushPlatform pushPlatform)
        {
            var args = new NotificationEventArgs()
            {
                Title = title,
                SubTitle = subTitle,
                Message = message,
                Identifier = identifier,
                Data = data,
                PushPlatform = pushPlatform
            };
            OnNotificationClicked?.Invoke(args);
        }
        public void onReceiveDeviceToken(string deviceToken) => OnDeviceTokenReceived?.Invoke(deviceToken);


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

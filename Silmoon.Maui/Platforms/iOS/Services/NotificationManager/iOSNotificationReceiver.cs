using Foundation;
using Newtonsoft.Json.Linq;
using Silmoon.Maui.Services.NotificationManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserNotifications;

namespace Silmoon.Maui.Services.PushNotifications
{
    public class iOSNotificationReceiver : UNUserNotificationCenterDelegate
    {
        NotificationManagerService notificationManagerService;
        public iOSNotificationReceiver(NotificationManagerService notificationManagerService)
        {
            this.notificationManagerService = notificationManagerService;
        }
        public override void WillPresentNotification(UNUserNotificationCenter center, UNNotification notification, Action<UNNotificationPresentationOptions> completionHandler)
        {
            var data = NSJsonSerialization.Serialize(notification.Request.Content.UserInfo, 0, out NSError error).ToString();
            var jsonData = JObject.Parse(data);

            string title = notification.Request.Content.Title;
            string subTitle = notification.Request.Content.Subtitle;
            string message = notification.Request.Content.Body;
            string identifier = notification.Request.Identifier;
            var result = notificationManagerService.onReceiveNotification(title, subTitle, message, identifier, jsonData, PushPlatform.iOS);

            if (result is null)
            {
                completionHandler(UNNotificationPresentationOptions.List | UNNotificationPresentationOptions.Banner);
            }
            else
            {
                UNNotificationPresentationOptions options = (UNNotificationPresentationOptions)result;
                completionHandler(options);
            }
        }
        public override void DidReceiveNotificationResponse(UNUserNotificationCenter center, UNNotificationResponse response, Action completionHandler)
        {
            if (response.IsDefaultAction)
            {
                var data = NSJsonSerialization.Serialize(response.Notification.Request.Content.UserInfo, 0, out NSError error).ToString();
                var jsonData = JObject.Parse(data);

                string title = response.Notification.Request.Content.Title;
                string subTitle = response.Notification.Request.Content.Subtitle;
                string message = response.Notification.Request.Content.Body;
                string identifier = response.Notification.Request.Identifier;
                notificationManagerService.onClickNotification(title, subTitle, message, identifier, jsonData, PushPlatform.iOS);
            }
            completionHandler();
        }
    }
}

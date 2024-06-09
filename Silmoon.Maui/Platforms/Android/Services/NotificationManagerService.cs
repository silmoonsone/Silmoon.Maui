using Newtonsoft.Json.Linq;
using Silmoon.Maui.ArgumentModels;
using Silmoon.Maui.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            return false;
        }

        public void onReceiveDeviceToken(string deviceToken)
        {
            throw new NotImplementedException();
        }

        public NotificationBehaviorType? onReceiveNotification(string title, string subTitle, string message, string identifier, JObject data, Enums.PlatformType pushPlatform)
        {
            throw new NotImplementedException();
        }

        public void onClickNotification(string title, string subTitle, string message, string identifier, JObject data, Enums.PlatformType pushPlatform)
        {
            throw new NotImplementedException();
        }

        public bool SendNotification(string title, string subTitle, string message, DateTime? notifyTime = null)
        {
            return false;
        }

        public bool SetBadgeNumber(int number)
        {
            return false;
        }
    }
}

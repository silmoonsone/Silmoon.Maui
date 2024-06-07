using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Silmoon.Maui.Services.NotificationManager;

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

        }

        public void onReceiveDeviceToken(string deviceToken)
        {
            throw new NotImplementedException();
        }

        public void onReceiveNotification(string title, string subTitle, string message, ReceiveType type, string identifier, JObject data, PushPlatform pushPlatform)
        {
            throw new NotImplementedException();
        }

        public bool SendNotification(string title, string subTitle, string message, DateTime? notifyTime = null)
        {
            throw new NotImplementedException();
        }

        public void SetBadgeNumber(int number)
        {
            throw new NotImplementedException();
        }
    }
}

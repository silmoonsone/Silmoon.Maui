using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Silmoon.Maui.Services.NotificationManager;

namespace Silmoon.Maui.Services.NotificationManager
{
    internal class NotificationManager : INotificationManagerService
    {
        public event Action<NotificationEventArgs> NotificationReceived;
        public event Action<string> OnDeviceTokenReceived;
        public event Action<bool> OnNotificationPermissionResult;

        public void Initialize()
        {
            throw new NotImplementedException();
        }

        public void onReceiveDeviceToken(string deviceToken)
        {
            throw new NotImplementedException();
        }

        public void onReceiveNotification(string title, string subTitle, string message, ReceiveType type, string identifier, string data, PushPlatform pushPlatform)
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

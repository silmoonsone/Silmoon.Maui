using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silmoon.Maui.Services.NotificationManager
{
    public interface INotificationManagerService
    {
        event Action<NotificationEventArgs> NotificationReceived;
        event Action<string> OnDeviceTokenReceived;
        event Action<bool> OnNotificationPermissionResult;
        void Initialize();
        bool SendNotification(string title, string subTitle, string message, DateTime? notifyTime = null);
        /// <summary>
        /// 用于接收来自通知中心的消息，并且使用NotificationReceived引发事件
        /// </summary>
        /// <param name="title"></param>
        /// <param name="message"></param>
        /// <param name="type"></param>
        /// <param name="identifier"></param>
        /// <param name="data"></param>
        /// <param name="pushPlatform"></param>
        void onReceiveNotification(string title, string subTitle, string message, ReceiveType type, string identifier, string data, PushPlatform pushPlatform);
        void onReceiveDeviceToken(string deviceToken);

        void SetBadgeNumber(int number);
    }
}

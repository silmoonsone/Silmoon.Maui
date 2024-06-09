using Newtonsoft.Json.Linq;
using Silmoon.Maui.ArgumentModels;
using Silmoon.Maui.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silmoon.Maui.Services
{
    public interface INotificationManagerService
    {
        /// <summary>
        /// 这里返回的NotificationBehaviorType类型表示如果应用在前台的时候收到了通知，应该如何处理。有的情况下应用在前台收到通知不应该弹出通知，而是直接处理。如果返回null，会使用默认的处理方式（List、Banner）。
        /// </summary>
        event Func<NotificationEventArgs, NotificationBehaviorType?> OnNotificationReceived;
        event Action<NotificationEventArgs> OnNotificationClicked;
        event Action<string> OnDeviceTokenReceived;
        event Action<bool> OnNotificationPermissionResult;
        string DeviceToken { get; }
        bool IsNotificationPermissionGranted { get; }
        bool Initialize();
        bool SendNotification(string title, string subTitle, string message, DateTime? notifyTime = null);
        /// <summary>
        /// 用于接收来自通知中心的消息，并且使用OnNotificationReceived引发事件
        /// </summary>
        /// <param name="title"></param>
        /// <param name="subTitle"></param>
        /// <param name="message"></param>
        /// <param name="identifier"></param>
        /// <param name="data"></param>
        /// <param name="pushPlatform"></param>
        /// <returns>这里返回的NotificationBehaviorType类型表示如果应用在前台的时候收到了通知，应该如何处理。有的情况下应用在前台收到通知不应该弹出通知，而是直接处理。如果返回null，会使用默认的处理方式（List、Banner）。</returns>
        NotificationBehaviorType? onReceiveNotification(string title, string subTitle, string message, string identifier, JObject data, Enums.PlatformType pushPlatform);
        void onClickNotification(string title, string subTitle, string message, string identifier, JObject data, Enums.PlatformType pushPlatform);
        void onReceiveDeviceToken(string deviceToken);

        bool SetBadgeNumber(int number);
    }
}

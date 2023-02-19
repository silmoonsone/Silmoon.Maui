using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace Silmoon.Xamarin.Interfaces
{
    public interface INotificationManager
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
        void onReceiveNotification(string title, string subTitle, string message, ReceiveType type, string identifier, JObject data, PushPlatform pushPlatform);
        void onReceiveDeviceToken(string deviceToken);

        void SetBadgeNumber(int number);
    }
    public class NotificationEventArgs
    {
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string Message { get; set; }
        public ReceiveType Type { get; set; }
        public string Identifier { get; set; }
        public JObject Data { get; set; }
        public PushPlatform PushPlatform { get; set; }
    }
    public enum ReceiveType
    {
        NotificationListen = 4,
        NotificationClicked = 7,
        LaunchedFrom = 9,
    }
    public enum PushPlatform
    {
        Unknown = 0,
        iOS = 1,
        Android = 2,
    }
}

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json.Linq;
using Silmoon.Xamarin.Android;
using Silmoon.Xamarin.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using AndroidApp = Android.App.Application;

[assembly: Dependency(typeof(AndroidNotificationManager))]
namespace Silmoon.Xamarin.Android
{
    public class AndroidNotificationManager : INotificationManager
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
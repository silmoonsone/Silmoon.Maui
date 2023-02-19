using Foundation;
using Silmoon.Xamarin.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIKit;
using Xamarin.Forms;

namespace Silmoon.Xamarin.iOS
{
    public class XamarinHelper
    {
        public static void RegisterServices()
        {
            DependencyService.Register<IFileService, FileService>();
            DependencyService.Register<IInAppPurchase, InAppPurchase>();
            DependencyService.Register<INotificationManager, iOSNotificationManager>();
        }
    }
}
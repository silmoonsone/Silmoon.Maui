using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Silmoon.Xamarin.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace Silmoon.Xamarin.Android
{
    public class XamarinHelper
    {
        public static void RegisterServices()
        {
            DependencyService.Register<IFileService, FileService>();
            DependencyService.Register<IAppInstaller, AppInstaller>();
            //DependencyService.Register<INotificationManager, AndroidNotificationManager>();
            DependencyService.Register<IAppPackage, AppPackage>();
        }
    }
}
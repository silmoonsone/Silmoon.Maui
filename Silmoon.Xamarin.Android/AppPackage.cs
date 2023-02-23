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

namespace Silmoon.Xamarin.Android
{
    public class AppPackage : IAppPackage
    {
        public AppPackageInfo GetPackageInfo()
        {

            var context = Application.Context;
            var packageName = context.PackageName;
            var packageInfo = context.PackageManager.GetPackageInfo(packageName, 0);

            var appPackageInfo = new AppPackageInfo
            {
                PackageName = packageName,
                BuildVersion = packageInfo.LongVersionCode.ToString(),
                Version = packageInfo.VersionName,
            };


            return appPackageInfo;
        }
    }
}
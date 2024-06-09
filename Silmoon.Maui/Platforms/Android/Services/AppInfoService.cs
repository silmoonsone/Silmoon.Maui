using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Silmoon.Maui.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Silmoon.Maui.Services
{
    public class AppInfoService : IAppInfoService
    {
        public AppPackageInfo GetPackageInfo()
        {
            var context = global::Android.App.Application.Context;
            var packageName = context.PackageName;
            var packageInfo = context.PackageManager.GetPackageInfo(packageName, 0);

            var appPackageInfo = new AppPackageInfo
            {
                PackageName = packageName,
#pragma warning disable CA1416 // Validate platform compatibility
                BuildVersion = packageInfo.LongVersionCode.ToString(),
#pragma warning restore CA1416 // Validate platform compatibility
                Version = packageInfo.VersionName,
            };


            return appPackageInfo;
        }
    }
}
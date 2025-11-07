using Foundation;
using Silmoon.Maui.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Silmoon.Maui.Platforms.Services
{
    public class AppInfoService : IAppInfoService
    {
        public AppPackageInfo GetPackageInfo()
        {
            var appPackageInfo = new AppPackageInfo
            {
                PackageName = NSBundle.MainBundle.InfoDictionary["CFBundleIdentifier"].ToString(),
                BuildVersion = NSBundle.MainBundle.InfoDictionary["CFBundleVersion"].ToString(),
                Version = NSBundle.MainBundle.InfoDictionary["CFBundleShortVersionString"].ToString()
            };
            return appPackageInfo;
        }
    }
}
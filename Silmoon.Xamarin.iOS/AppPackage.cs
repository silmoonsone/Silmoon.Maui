using Foundation;
using Silmoon.Xamarin.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIKit;

namespace Silmoon.Xamarin.iOS
{
    public class AppPackage : IAppPackage
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
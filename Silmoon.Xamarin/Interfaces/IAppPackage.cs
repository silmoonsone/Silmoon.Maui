using System;
using System.Collections.Generic;
using System.Text;

namespace Silmoon.Xamarin.Interfaces
{
    public interface IAppPackage
    {
        AppPackageInfo GetPackageInfo();
    }
    public class AppPackageInfo
    {
        /// <summary>
        /// App版本号，在iOS中是CFBundleShortVersionString，在Android是VersionName
        /// </summary>
        public string Version { get; set; }
        /// <summary>
        /// App版本号，在iOS中是CFBundleVersion，在Android是LongVersionCode
        /// </summary>
        public string BuildVersion { get; set; }
        /// <summary>
        /// 包ID名称
        /// </summary>
        public string PackageName { get; set; }
    }
}

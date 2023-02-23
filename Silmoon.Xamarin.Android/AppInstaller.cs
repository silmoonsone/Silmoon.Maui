using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.Core.Content;
using Java.IO;
using Silmoon.Xamarin.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Silmoon.Xamarin.Android
{
    public class AppInstaller : IAppInstaller
    {
        public void InstallApp(string apkPath)
        {
            try
            {
                var context = global::Android.App.Application.Context;

                // Get the URI for the APK file
                var file = new File(apkPath);
                var apkUri = FileProvider.GetUriForFile(context, $"{context.PackageName}.provider", file);

                // Create a new intent to install the APK
                var intent = new Intent(Intent.ActionView);
                intent.SetDataAndType(apkUri, "application/vnd.android.package-archive");
                intent.SetFlags(ActivityFlags.NewTask);
                intent.AddFlags(ActivityFlags.GrantReadUriPermission);
                intent.AddFlags(ActivityFlags.GrantWriteUriPermission);
                intent.AddFlags(ActivityFlags.GrantPersistableUriPermission);

                // Start the installation process
                context.StartActivity(intent);
            }
            catch
            {
                // An error occurred while attempting to install the app
            }
        }
    }
}
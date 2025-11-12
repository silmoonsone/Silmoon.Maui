using AndroidX.AppCompat.Widget;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silmoon.Maui.Platforms.Android.Handlers
{
    public class ClearEntryHandler : EntryHandler
    {
        protected override void ConnectHandler(MauiAppCompatEditText platformView)
        {
            platformView.SetBackgroundColor(global::Android.Graphics.Color.Transparent);
            base.ConnectHandler(platformView);
        }
    }
}

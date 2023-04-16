using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silmoon.Maui.Platforms.iOS.Handlers
{
    public class ClearEntryHandler : EntryHandler
    {
        protected override void ConnectHandler(MauiTextField platformView)
        {
            platformView.BorderStyle = UIKit.UITextBorderStyle.None;
            base.ConnectHandler(platformView);
        }
    }
}

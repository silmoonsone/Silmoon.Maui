using Foundation;
using Silmoon.Xamarin.Controller;
using Silmoon.Xamarin.iOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ClearEntry), typeof(ClearEntryRender))]
namespace Silmoon.Xamarin.iOS
{
    public class ClearEntryRender : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control == null || e.NewElement == null) return;

            Control.BorderStyle = UITextBorderStyle.None;

            //CALayer line = new CALayer
            //{
            //    BorderColor = UIColor.FromRGB(174, 174, 174).CGColor,
            //    BackgroundColor = UIColor.FromRGB(174, 174, 174).CGColor,
            //    Frame = new CGRect(0, Frame.Height / 2 + 5, Frame.Width, 1)
            //};
            //Control.Layer.AddSublayer(line);


            //Control.Layer.BorderColor = UIColor.FromRGB(220, 220, 220).CGColor;
            //Control.Layer.BorderWidth = 1;
            //Control.Layer.CornerRadius = 5;
        }
    }
}
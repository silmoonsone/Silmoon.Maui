using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Silmoon.Xamarin.Android.Renders;
using Silmoon.Xamarin.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(ClearEntry), typeof(ClearEntryRender))]
namespace Silmoon.Xamarin.Android.Renders
{
    public class ClearEntryRender : EntryRenderer
    {
        public ClearEntryRender(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control == null || e.NewElement == null) return;

            (EditText as FormsEditText).SetBackgroundColor(global::Android.Graphics.Color.Transparent);
        }
    }
}
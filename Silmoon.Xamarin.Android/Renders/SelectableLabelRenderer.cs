using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Silmoon.Xamarin.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using Silmoon.Xamarin.Android.Renders;

[assembly: ExportRenderer(typeof(SelectableLabel), typeof(SelectableLabelRenderer))]
namespace Silmoon.Xamarin.Android.Renders
{
    [Preserve(AllMembers = true)]
    public class SelectableLabelRenderer : ViewRenderer<SelectableLabel, TextView>
    {
        TextView textView;

        public SelectableLabelRenderer(Context context) : base(context)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<SelectableLabel> e)
        {
            base.OnElementChanged(e);

            var label = Element;
            if (label == null)
                return;

            if (Control == null)
            {
                textView = new TextView(Context);
            }

            textView.Enabled = true;
            textView.Focusable = true;
            textView.LongClickable = true;
            textView.SetTextIsSelectable(true);

            // Initial properties Set
            textView.Text = label.Text;
            textView.SetTextColor(label.TextColor.ToAndroid());
            switch (label.FontAttributes)
            {
                case FontAttributes.None:
                    textView.SetTypeface(null, global::Android.Graphics.TypefaceStyle.Normal);
                    break;
                case FontAttributes.Bold:
                    textView.SetTypeface(null, global::Android.Graphics.TypefaceStyle.Bold);
                    break;
                case FontAttributes.Italic:
                    textView.SetTypeface(null, global::Android.Graphics.TypefaceStyle.Italic);
                    break;
                default:
                    textView.SetTypeface(null, global::Android.Graphics.TypefaceStyle.Normal);
                    break;
            }

            textView.TextSize = (float)label.FontSize;

            SetNativeControl(textView);
        }
    }
}
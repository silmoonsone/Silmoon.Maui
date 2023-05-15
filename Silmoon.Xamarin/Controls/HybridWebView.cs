using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Silmoon.Xamarin.Controls
{
    [global::Xamarin.Forms.Internals.Preserve(AllMembers = true)]
    public class HybridWebView : WebView
    {

        Action<string, object> action;

        public static readonly BindableProperty UriProperty = BindableProperty.Create("Uri", typeof(string), typeof(HybridWebView), default(string));
        public static readonly BindableProperty IgnoreSslErrorsProperty = BindableProperty.Create("IgnoreSslErrors", typeof(bool), typeof(HybridWebView), false);

        public string Uri
        {
            get { return (string)GetValue(UriProperty); }
            set { SetValue(UriProperty, value); }
        }
        public bool IgnoreSslErrors
        {
            get { return (bool)GetValue(IgnoreSslErrorsProperty); }
            set { SetValue(IgnoreSslErrorsProperty, value); }
        }

        public void OnInvoke(Action<string, object> callback)
        {
            action = callback;
        }

        public void Cleanup()
        {
            action = null;
        }

        public void WebInvoking(string method, object data)
        {
            if (action == null || method == null || data == null) return;
            action.Invoke(method, data);
        }
    }
}

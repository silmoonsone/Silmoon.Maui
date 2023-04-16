using Android.Content;
using Android.Net.Http;
using Android.Webkit;
using Android.Widget;
using Java.Interop;
using Microsoft.Maui.Controls.Compatibility.Platform.Android;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using Silmoon.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Android.App.ActionBar;
using AWebView = Android.Webkit.WebView;


namespace Silmoon.Maui.Platforms.Android.Handlers
{
    public class HybridWebViewHandler : WebViewHandler
    {
        const string JavascriptFunction = "function __webAppInvoke(method, data){jsBridge.__webAppInvoke(method, data);}";
        protected override void ConnectHandler(AWebView platformView)
        {
            platformView.SetWebViewClient(new HybridWebViewClient(this, $"javascript: {JavascriptFunction}"));
            platformView.AddJavascriptInterface(new JSBridge(this), "jsBridge");
            platformView.Settings.SetSupportMultipleWindows(false);

            base.ConnectHandler(platformView);
        }
        protected override void DisconnectHandler(AWebView platformView)
        {
            ((HybridWebView)VirtualView).Cleanup();
            base.DisconnectHandler(platformView);
        }
        public class HybridWebViewClient : MauiWebViewClient
        {
            string _javascript { get; set; }
            HybridWebViewHandler hybridWebViewHandler;
            public HybridWebViewClient(HybridWebViewHandler handler, string javascript) : base(handler)
            {
                _javascript = javascript;
                hybridWebViewHandler = handler;
            }
            public override void OnPageFinished(AWebView view, string url)
            {
                base.OnPageFinished(view, url);
                view.EvaluateJavascript(_javascript, null);
            }
            public override bool ShouldOverrideUrlLoading(AWebView view, IWebResourceRequest request)
            {
                if (request.Url.Scheme != "http" && request.Url.Scheme != "https")
                {
                    try
                    {
                        Intent intent = new Intent() { };
                        intent.SetAction(Intent.ActionView);
                        intent.SetData(request.Url);
                        hybridWebViewHandler.Context.StartActivity(intent);
                    }
                    catch (Exception)
                    {
                        Toast.MakeText(hybridWebViewHandler.Context, "调用错误，相关应用没有安装？", ToastLength.Long).Show();
                    }
                    return true;
                }
                return base.ShouldOverrideUrlLoading(view, request);
            }
            public override void OnReceivedSslError(AWebView view, SslErrorHandler handler, SslError error)
            {
                //忽略开发HTTPS验证
                if (((HybridWebView)hybridWebViewHandler.VirtualView).IgnoreSslErrors)
                    handler.Proceed();
                else
                    base.OnReceivedSslError(view, handler, error);
            }
            [Obsolete]
            public override bool ShouldOverrideUrlLoading(AWebView view, string url)
            {
                if (url.StartsWith("weixin"))
                {
                    try
                    {
                        Intent intent = new Intent() { };
                        intent.SetAction(Intent.ActionView);
                        intent.SetData(global::Android.Net.Uri.Parse(url));
                        hybridWebViewHandler.Context.StartActivity(intent);
                    }
                    catch (Exception)
                    {
                        Toast.MakeText(hybridWebViewHandler.Context, "调用错误，相关应用没有安装？", ToastLength.Long).Show();
                    }
                    return true;
                }
                return base.ShouldOverrideUrlLoading(view, url);
            }
        }
        public class JSBridge : Java.Lang.Object
        {
            readonly WeakReference<HybridWebViewHandler> weakReference;

            public JSBridge(HybridWebViewHandler hybridWebViewHandler) => weakReference = new WeakReference<HybridWebViewHandler>(hybridWebViewHandler);

            [JavascriptInterface]
            [Export("__webAppInvoke")]
            public void WebAppInvoke(string method, string data)
            {
                if (weakReference != null && weakReference.TryGetTarget(out HybridWebViewHandler hybridRenderer))
                    ((HybridWebView)hybridRenderer.VirtualView).WebInvoking(method, data);
            }
        }

    }
}

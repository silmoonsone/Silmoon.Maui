using Android.App;
using Android.Content;
using Android.Net.Http;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Webkit;
using Android.Widget;
using Java.Interop;
using Silmoon.Xamarin.Android.Renders;
using Silmoon.Xamarin.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using AWebView = Android.Webkit.WebView;
using WebView = Xamarin.Forms.WebView;

[assembly: ExportRenderer(typeof(HybridWebView), typeof(HybridWebViewRenderer))]
namespace Silmoon.Xamarin.Android.Renders
{
    [Preserve(AllMembers = true)]
    public class HybridWebViewRenderer : WebViewRenderer
    {
        const string JavascriptFunction = "function __webAppInvoke(method, data){jsBridge.__webAppInvoke(method, data);}";
        public HybridWebViewRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<WebView> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
                Control.RemoveJavascriptInterface("jsBridge");
                ((HybridWebView)Element).Cleanup();
            }
            if (e.NewElement != null)
            {
                Control.SetWebViewClient(new HybridWebViewClient(this, $"javascript: {JavascriptFunction}"));
                Control.AddJavascriptInterface(new JSBridge(this), "jsBridge");
                AWebView aWebview = Control;
                aWebview.Settings.SetSupportMultipleWindows(false);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ((HybridWebView)Element).Cleanup();
            }
            base.Dispose(disposing);
        }
        public class HybridWebViewClient : FormsWebViewClient
        {
            string _javascript;
            HybridWebViewRenderer renderer;
            public HybridWebViewClient(HybridWebViewRenderer renderer, string javascript) : base(renderer)
            {
                _javascript = javascript;
                this.renderer = renderer;
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
                        renderer.Context.StartActivity(intent);
                    }
                    catch (Exception)
                    {
                        Toast.MakeText(renderer.Context, "调用错误，相关应用没有安装？", ToastLength.Long).Show();
                    }
                    return true;
                }
                return base.ShouldOverrideUrlLoading(view, request);
            }
            public override void OnReceivedSslError(AWebView view, SslErrorHandler handler, SslError error)
            {
                //忽略开发HTTPS验证
                if (((HybridWebView)renderer.Element).IgnoreSslErrors)
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
                        renderer.Context.StartActivity(intent);
                    }
                    catch (Exception)
                    {
                        Toast.MakeText(renderer.Context, "调用错误，相关应用没有安装？", ToastLength.Long).Show();
                    }
                    return true;
                }
                return base.ShouldOverrideUrlLoading(view, url);
            }
        }
        public class JSBridge : Java.Lang.Object
        {
            readonly WeakReference<HybridWebViewRenderer> hybridWebViewRenderer;

            public JSBridge(HybridWebViewRenderer hybridRenderer) => hybridWebViewRenderer = new WeakReference<HybridWebViewRenderer>(hybridRenderer);

            [JavascriptInterface]
            [Export("__webAppInvoke")]
            public void WebAppInvoke(string method, string data)
            {
                if (hybridWebViewRenderer != null && hybridWebViewRenderer.TryGetTarget(out HybridWebViewRenderer hybridRenderer))
                    ((HybridWebView)hybridRenderer.Element).WebInvoking(method, data);
            }
        }
    }
}
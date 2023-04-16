using Foundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIKit;
using WebKit;
using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms;
using Silmoon.Xamarin.Controller;
using Silmoon.Xamarin.iOS.Renders;

[assembly: ExportRenderer(typeof(HybridWebView), typeof(HybridWebViewRenderer))]
namespace Silmoon.Xamarin.iOS.Renders
{
    [Preserve(AllMembers = true)]
    public class HybridWebViewRenderer : WkWebViewRenderer, IWKScriptMessageHandler
    {
        const string JavaScriptFunction = "function __webAppInvoke(method, data){window.webkit.messageHandlers.invokeAction.postMessage(method + \";\" + data);}";
        WKUserContentController userController;

        public HybridWebViewRenderer() : this(new WKWebViewConfiguration() { AllowsInlineMediaPlayback = true })
        {
        }

        public HybridWebViewRenderer(WKWebViewConfiguration config) : base(config)
        {
            config.AllowsInlineMediaPlayback = true;
            userController = config.UserContentController;
            var script = new WKUserScript(new NSString(JavaScriptFunction), WKUserScriptInjectionTime.AtDocumentEnd, false);
            userController.AddUserScript(script);
            userController.AddScriptMessageHandler(this, "invokeAction");
        }

        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
                userController.RemoveAllUserScripts();
                userController.RemoveScriptMessageHandler("invokeAction");
                HybridWebView hybridWebView = e.OldElement as HybridWebView;
                hybridWebView.Cleanup();
            }
            if (NativeView != null && e.NewElement != null)
            {
                var webView = ((WKWebView)NativeView);
                if (webView != null)
                {
                    //webView.Configuration.SetUrlSchemeHandler(new WeixinHandler(), "weixin");
                    webView.NavigationDelegate = new MysWKNavigationDelegate(this, webView.NavigationDelegate);
                }
            }

        }
        public void DidReceiveScriptMessage(WKUserContentController userContentController, WKScriptMessage message)
        {
            var str = message.Body.ToString();
            var strs = str.Split(new string[] { ";" }, 2, StringSplitOptions.None);
            ((HybridWebView)Element).WebInvoking(strs[0], strs[1]);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ((HybridWebView)Element).Cleanup();
            }
            base.Dispose(disposing);
        }


        private class MysWKNavigationDelegate : WKNavigationDelegate
        {
            private readonly IWKNavigationDelegate _defaultDelegate;
            HybridWebViewRenderer renderer { get; set; }

            public MysWKNavigationDelegate(HybridWebViewRenderer renderer, IWKNavigationDelegate defaultDelegate)
            {
                this.renderer = renderer;
                _defaultDelegate = defaultDelegate;
            }

            public override void DidFailNavigation(WKWebView webView, WKNavigation navigation, NSError error) => _defaultDelegate.DidFailNavigation(webView, navigation, error);

            public override void DidFinishNavigation(WKWebView webView, WKNavigation navigation) => _defaultDelegate.DidFinishNavigation(webView, navigation);

            public override void DidStartProvisionalNavigation(WKWebView webView, WKNavigation navigation) => _defaultDelegate.DidStartProvisionalNavigation(webView, navigation);
            public override void DidReceiveAuthenticationChallenge(WKWebView webView, NSUrlAuthenticationChallenge challenge, Action<NSUrlSessionAuthChallengeDisposition, NSUrlCredential> completionHandler)
            {
                //忽略开发HTTPS验证
                if (((HybridWebView)renderer.Element).IgnoreSslErrors)
                {
                    using (var cred = NSUrlCredential.FromTrust(challenge.ProtectionSpace.ServerSecTrust))
                    {
                        completionHandler.Invoke(NSUrlSessionAuthChallengeDisposition.UseCredential, cred);
                    }
                }
                else
                {
                    completionHandler.Invoke(NSUrlSessionAuthChallengeDisposition.PerformDefaultHandling, null);

                }
            }
            public override void DecidePolicy(WKWebView webView, WKNavigationAction navigationAction, Action<WKNavigationActionPolicy> decisionHandler)
            {
                try
                {
                    if (navigationAction.Request?.Url != null)
                    {
                        var url = navigationAction.Request.Url.ToString();
                        Uri uri = new Uri(url);
                        if (uri.Scheme != "http" && uri.Scheme != "https")
                        {
                            UIApplication.SharedApplication.OpenUrl(uri);
                            decisionHandler(WKNavigationActionPolicy.Cancel);
                            return;
                        }
                    }
                }
                catch (Exception)
                {

                }
                _defaultDelegate.DecidePolicy(webView, navigationAction, decisionHandler);
            }
        }
    }
}
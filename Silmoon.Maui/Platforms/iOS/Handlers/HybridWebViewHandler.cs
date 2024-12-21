using Foundation;
using Microsoft.Maui.Handlers;
using ObjCRuntime;
using Silmoon.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;
using UIKit;
using WebKit;
using HybridWebView = Silmoon.Maui.Controls.HybridWebView;

namespace Silmoon.Maui.Platforms.iOS.Handlers
{
    public class HybridWebViewHandler : WebViewHandler
    {
        ScriptHandler scriptHandler;
        const string JavaScriptFunction = "function __webAppInvoke(method, data){window.webkit.messageHandlers.invokeAction.postMessage(method + \";\" + data);}";

        public NativeHandle Handle => PlatformView.Handle;

        protected override void ConnectHandler(WKWebView platformView)
        {
            if (platformView != null)
            {
                scriptHandler = new ScriptHandler((IWebView)VirtualView);

                platformView.Configuration.AllowsInlineMediaPlayback = true;
                platformView.Configuration.UserContentController.AddUserScript(new WKUserScript(new NSString(JavaScriptFunction), WKUserScriptInjectionTime.AtDocumentEnd, false));
                platformView.Configuration.UserContentController.AddScriptMessageHandler(scriptHandler, "invokeAction");
                //webView.Configuration.SetUrlSchemeHandler(new WeixinHandler(), "weixin");
                platformView.NavigationDelegate = new MyWKNavigationDelegate(this, platformView.NavigationDelegate);
            }

            base.ConnectHandler(platformView);
        }


        protected override void DisconnectHandler(WKWebView platformView)
        {
            ((HybridWebView)VirtualView).Cleanup();

            base.DisconnectHandler(platformView);
        }

        class ScriptHandler : NSObject, IWKScriptMessageHandler
        {
            public IWebView WebView { get; private set; }
            public ScriptHandler(IWebView webView)
            {
                WebView = webView;
            }
            public void DidReceiveScriptMessage(WKUserContentController userContentController, WKScriptMessage message)
            {
                var str = message.Body.ToString();
                var strs = str.Split(new string[] { ";" }, 2, StringSplitOptions.None);
                ((HybridWebView)WebView).WebInvoking(strs[0], strs[1]);
            }
        }

        private class MyWKNavigationDelegate : WKNavigationDelegate
        {
            private readonly IWKNavigationDelegate _defaultDelegate;
            HybridWebViewHandler _renderer { get; set; }

            public MyWKNavigationDelegate(HybridWebViewHandler renderer, IWKNavigationDelegate defaultDelegate)
            {
                _renderer = renderer;
                _defaultDelegate = defaultDelegate;
            }

            public override void DidFailNavigation(WKWebView webView, WKNavigation navigation, NSError error) => _defaultDelegate.DidFailNavigation(webView, navigation, error);

            public override void DidFinishNavigation(WKWebView webView, WKNavigation navigation) => _defaultDelegate.DidFinishNavigation(webView, navigation);

            public new void DidStartProvisionalNavigation(WKWebView webView, WKNavigation navigation) => _defaultDelegate.DidStartProvisionalNavigation(webView, navigation);

            public override void DidReceiveAuthenticationChallenge(WKWebView webView, NSUrlAuthenticationChallenge challenge, Action<NSUrlSessionAuthChallengeDisposition, NSUrlCredential> completionHandler)
            {
                //忽略开发HTTPS验证
                if (((HybridWebView)_renderer.VirtualView).IgnoreSslErrors)
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
                            UIApplication.SharedApplication.OpenUrl(uri, new NSDictionary(), completion: null);
                            decisionHandler(WKNavigationActionPolicy.Cancel);
                            return;
                        }
                    }
                }
                catch (Exception)
                {

                }
                decisionHandler(WKNavigationActionPolicy.Allow);
            }
        }
    }
}

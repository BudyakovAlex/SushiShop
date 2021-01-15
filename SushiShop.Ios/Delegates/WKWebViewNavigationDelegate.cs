using System;
using Foundation;
using UIKit;
using WebKit;

namespace SushiShop.Ios.Delegates
{
    public class WKWebViewNavigationDelegate : NSObject, IWKNavigationDelegate
    {
        [Export("webView:decidePolicyForNavigationAction:decisionHandler:")]
        public void DecidePolicy(WKWebView webView, WKNavigationAction navigationAction, Action<WKNavigationActionPolicy> decisionHandler)
        {
            if (navigationAction.Request.Url?.Scheme == "tel")
            {
                UIApplication.SharedApplication.OpenUrl(navigationAction.Request.Url);
                decisionHandler.Invoke(WKNavigationActionPolicy.Cancel);
            }
            else
            {
                decisionHandler.Invoke(WKNavigationActionPolicy.Allow);
            }
        }
    }
}

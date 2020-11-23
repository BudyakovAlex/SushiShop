using System;
using CoreGraphics;
using Foundation;
using WebKit;

namespace SushiShop.Ios.Views.Controls
{
    [Register(nameof(ResizableWebView))]
    public class ResizableWebView : WKWebView, IWKNavigationDelegate
    {
        public ResizableWebView(NSCoder coder)
            : base(coder)
        {
            Initialize();
        }

        public ResizableWebView(CGRect frame, WKWebViewConfiguration configuration)
            : base(frame, configuration)
        {
            Initialize();
        }

        protected ResizableWebView(NSObjectFlag t)
            : base(t)
        {
        }

        protected ResizableWebView(IntPtr handle)
            : base(handle)
        {
        }

        public override CGSize IntrinsicContentSize
        {
            get
            {
                var size = base.IntrinsicContentSize;
                return new CGSize(size.Width, ScrollView.ContentSize.Height);
            }
        }

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();
            Initialize();
        }

        [Export("webView:didFinishNavigation:")]
        public void DidFinishNavigation(WKWebView webView, WKNavigation navigation)
        {
            InvalidateIntrinsicContentSize();
        }

        private void Initialize()
        {
            NavigationDelegate = this;
            ScrollView.ScrollEnabled = false;
        }
    }
}

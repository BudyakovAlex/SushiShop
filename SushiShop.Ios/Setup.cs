using MvvmCross.Binding.Bindings.Target.Construction;
using MvvmCross.Platforms.Ios.Core;
using SushiShop.Core;
using SushiShop.Ios.Common;
using SushiShop.Ios.TargetBindings;
using UIKit;
using WebKit;

namespace SushiShop.Ios
{
    public class Setup : MvxIosSetup<App>
    {
        protected override void FillTargetFactories(IMvxTargetBindingFactoryRegistry registry)
        {
            base.FillTargetFactories(registry);

            registry.RegisterCustomBindingFactory<UIButton>(BindingConstants.Image, view => new UIButtonImageTargetBinding(view));
            registry.RegisterCustomBindingFactory<WKWebView>(nameof(WKWebViewHtmlStringTargetBinding), view => new WKWebViewHtmlStringTargetBinding(view));
        }
    }
}
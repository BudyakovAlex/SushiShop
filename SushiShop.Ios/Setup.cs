using BuildApps.Core.Mobile.MvvmCross.IoC;
using MvvmCross.Binding.Bindings.Target.Construction;
using MvvmCross.Platforms.Ios.Core;
using SushiShop.Core;
using SushiShop.Core.Plugins;
using SushiShop.Ios.Common;
using SushiShop.Ios.Plugins;
using SushiShop.Ios.TargetBindings;
using UIKit;
using WebKit;

namespace SushiShop.Ios
{
    public class Setup : MvxIosSetup<App>
    {
        protected override void InitializeLastChance()
        {
            base.InitializeLastChance();

            var container = CompositionRoot.Container;

            container.RegisterSingleton<IDialog, Dialog>();
            container.RegisterSingleton<ILocation, Location>();
        }

        protected override void FillTargetFactories(IMvxTargetBindingFactoryRegistry registry)
        {
            base.FillTargetFactories(registry);

            registry.RegisterCustomBindingFactory<UIButton>(BindingConstants.Image, view => new UIButtonImageTargetBinding(view));
            registry.RegisterCustomBindingFactory<WKWebView>(nameof(WKWebViewHtmlStringTargetBinding), view => new WKWebViewHtmlStringTargetBinding(view));
            registry.RegisterCustomBindingFactory<WKWebView>(nameof(WKWebViewUrlTargetBinding), view => new WKWebViewUrlTargetBinding(view));
        }
    }
}
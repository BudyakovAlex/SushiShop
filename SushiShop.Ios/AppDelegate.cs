using Foundation;
using MvvmCross.Platforms.Ios.Core;
using SushiShop.Core;
using SushiShop.Ios.Common.Styles;
using UIKit;

namespace SushiShop.Ios
{
    [Register(nameof(AppDelegate))]
    public class AppDelegate : MvxApplicationDelegate<Setup, App>
    {
        public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
        {
            base.FinishedLaunching(application, launchOptions);

            Appearance.SetGlobalAppearance();

            return true;
        }
    }
}
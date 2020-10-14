using Foundation;
using MvvmCross.Platforms.Ios.Core;
using SushiShop.Core;
using UIKit;

namespace SushiShop.Ios
{
    [Register(nameof(AppDelegate))]
    public class AppDelegate : MvxApplicationDelegate<Setup, App>
    {
        public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
        {
            var result = base.FinishedLaunching(application, launchOptions);
            UINavigationBar.Appearance.BarTintColor = UIColor.White;
            return result;
        }
    }
}
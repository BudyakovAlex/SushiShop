using BuildApps.Core.Mobile.Common.Extensions;
using UIKit;

namespace SushiShop.Ios.Extensions
{
    public static class WindowExtensions
    {
        public static UIViewController GetTopViewController(this UIWindow window)
        {
            var viewController = window.RootViewController;
            if (viewController is UITabBarController tabBarController)
            {
                viewController = tabBarController.SelectedViewController;
            }

            while (viewController.ChildViewControllers.IsNotEmpty())
            {
                viewController = viewController.ChildViewControllers[^1];
            }

            return viewController;
        }
    }
}

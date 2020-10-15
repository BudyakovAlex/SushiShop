using UIKit;

namespace SushiShop.Ios.Common.Styles
{
    public static class TabBarStyles
    {
        public static void ApplyPrimaryStyle(this UITabBar tabBar)
        {
            tabBar.Translucent = false;
            tabBar.ShadowImage = new UIImage();
            tabBar.BackgroundImage = new UIImage();
            tabBar.BarTintColor = Colors.White;
            tabBar.UnselectedItemTintColor = Colors.FigmaBlack;
            tabBar.SelectedImageTintColor = Colors.Orange2;
        }
    }
}

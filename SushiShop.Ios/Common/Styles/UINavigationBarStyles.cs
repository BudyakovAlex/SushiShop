using UIKit;

namespace SushiShop.Ios.Common.Styles
{
    public static class UINavigationBarStyles
    {
        public static void SetDefaultAppearance()
        {
            if (UIDevice.CurrentDevice.CheckSystemVersion(15, 0))
            {
                var appearance = new UINavigationBarAppearance();
                appearance.ConfigureWithOpaqueBackground();
                appearance.BackgroundColor = UIColor.White;
                appearance.ShadowColor = UIColor.Clear;
                appearance.ShadowImage = new UIImage();
                UINavigationBar.Appearance.StandardAppearance = appearance;
                UINavigationBar.Appearance.ScrollEdgeAppearance = appearance;
                UINavigationBar.Appearance.CompactScrollEdgeAppearance = appearance;
            }

            UINavigationBar.Appearance.BackgroundColor = UIColor.White;
            UINavigationBar.Appearance.ShadowImage = new UIImage();
            UINavigationBar.Appearance.Translucent = false;
        }
    }
}

using SushiShop.Core.Data.Enums;
using UIKit;

namespace SushiShop.Ios.Common.Styles
{
    public static class Appearance
    {
        public static void SetGlobalAppearance()
        {
            SetNavigationBarAppearance();
            SetTabBarItemAppearance();
        }

        private static void SetNavigationBarAppearance()
        {
            var appearance = UINavigationBar.Appearance;
            appearance.Translucent = false;
            appearance.ShadowImage = new UIImage();
            appearance.BarTintColor = Colors.White;
            appearance.TintColor = Colors.FigmaBlack;
            appearance.TitleTextAttributes = new UIStringAttributes { Font = Font.Create(FontStyle.Medium, 18f) };

            var backImage = UIImage.FromBundle(ImageNames.ArrowBack);
            appearance.BackIndicatorImage = backImage;
            appearance.BackIndicatorTransitionMaskImage = backImage;
        }

        private static void SetTabBarItemAppearance()
        {
            UITabBarItem.Appearance.SetTitleTextAttributes(
                new UITextAttributes { Font = Font.Create(FontStyle.Medium, 9f) },
                UIControlState.Normal);
        }
    }
}

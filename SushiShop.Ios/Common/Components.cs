using CoreGraphics;
using UIKit;

namespace SushiShop.Ios.Common
{
    public static class Components
    {
        public static UIButton CreateDefaultBarButton(string imageName)
        {
            var button = new UIButton(new CGRect(0, 0, 24f, 24f));
            button.SetImage(UIImage.FromBundle(imageName), UIControlState.Normal);

            return button;
        }

        public static UIBarButtonItem CreateBarButtonItem(string imageName) =>
            new UIBarButtonItem(UIImage.FromBundle(imageName), UIBarButtonItemStyle.Plain, null);

        public static UIBarButtonItem CreateBarButtonItemText(string title) =>
            new UIBarButtonItem(title, UIBarButtonItemStyle.Plain, null)
            {
                TintColor = Colors.Orange2
            };
    }
}
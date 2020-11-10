using CoreAnimation;
using CoreGraphics;
using UIKit;

namespace SushiShop.Ios.Common
{
    public static class Components
    {
        public static UIButton CreateDefaultBarButton(string imageName, float width = 24f, float heigth = 24f)
        {
            var button = new UIButton(new CGRect(0, 0, width, heigth));
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

        public static CAGradientLayer CreateGradientLayer() =>
            new CAGradientLayer()
            {
                Colors = new[] { Colors.OrangeGradientStart.CGColor, Colors.OrangeGradientEnd.CGColor },
                StartPoint = new CGPoint(0, 0.5),
                EndPoint = new CGPoint(1, 0.5)
            };
    }
}
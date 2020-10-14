using CoreGraphics;
using UIKit;

namespace SushiShop.Ios.Common
{
    public static class UIHelper
    {
        public static UIButton CreateDefaultBarButton(string normalImagePath, string highlightedImagePath)
        {
            var button = new UIButton(new CGRect(0, 0, 24f, 24f));
            button.SetImage(UIImage.FromBundle(normalImagePath), UIControlState.Normal);
            button.SetImage(UIImage.FromBundle(highlightedImagePath), UIControlState.Highlighted);

            return button;
        }
    }
}
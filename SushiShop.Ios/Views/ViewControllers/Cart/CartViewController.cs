using BuildApps.Core.Mobile.MvvmCross.UIKit.Views.ViewControllers;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using SushiShop.Core.ViewModels.Cart;
using SushiShop.Ios.Common;

namespace SushiShop.Ios.Views.ViewControllers.Cart
{
    [MvxTabPresentation(WrapInNavigationController = true, TabIconName = ImageNames.CartTabIcon)]
    public partial class CartViewController : BaseViewController<CartViewModel>
    {
    }
}

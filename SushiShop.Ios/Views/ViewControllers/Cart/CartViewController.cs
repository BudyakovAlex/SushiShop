using BuildApps.Core.Mobile.MvvmCross.UIKit.Views.ViewControllers;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using SushiShop.Core.ViewModels.Cart;

namespace SushiShop.Ios.Views.ViewControllers.Cart
{
    [MvxTabPresentation(WrapInNavigationController = true)]
    public partial class CartViewController : BaseViewController<CartViewModel>
    {
    }
}

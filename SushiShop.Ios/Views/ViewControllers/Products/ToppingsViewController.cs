using BuildApps.Core.Mobile.MvvmCross.UIKit.Views.ViewControllers;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using SushiShop.Core.ViewModels.Products;

namespace SushiShop.Ios.Views.ViewControllers.Products
{
    [MvxChildPresentation(Animated = true)]
    public partial class ToppingsViewController : BaseViewController<ToppingsViewModel>
    {

    }
}


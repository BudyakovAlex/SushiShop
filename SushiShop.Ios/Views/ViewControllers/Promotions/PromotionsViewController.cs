using BuildApps.Core.Mobile.MvvmCross.UIKit.Views.ViewControllers;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using SushiShop.Core.ViewModels.Promotions;

namespace SushiShop.Ios.Views.ViewControllers.Promotions
{
    [MvxTabPresentation(WrapInNavigationController = true)]
    public partial class PromotionsViewController : BaseViewController<PromotionsViewModel>
    {
    }
}

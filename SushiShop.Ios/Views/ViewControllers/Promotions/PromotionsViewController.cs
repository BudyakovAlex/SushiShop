using BuildApps.Core.Mobile.MvvmCross.UIKit.Views.ViewControllers;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using SushiShop.Core.ViewModels.Promotions;
using SushiShop.Ios.Common;

namespace SushiShop.Ios.Views.ViewControllers.Promotions
{
    [MvxTabPresentation(WrapInNavigationController = true, TabIconName = ImageNames.PromotionsTabIcon)]
    public partial class PromotionsViewController : BaseViewController<PromotionsViewModel>
    {
    }
}

using BuildApps.Core.Mobile.MvvmCross.UIKit.Views.ViewControllers;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using SushiShop.Core.ViewModels.Common;
using UIKit;

namespace SushiShop.Ios.Views.ViewControllers.CommonInfo
{
    [MvxModalPresentation(
        WrapInNavigationController = true,
        Animated = true,
        ModalPresentationStyle = UIModalPresentationStyle.OverFullScreen)]
    public partial class CommonInfoViewController : BaseViewController<CommonInfoViewModel>
    {
        public CommonInfoViewController() : base("CommonInfoViewController", null)
        {
        }
    }
}
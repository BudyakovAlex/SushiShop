using BuildApps.Core.Mobile.MvvmCross.UIKit.Views.ViewControllers;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using SushiShop.Core.ViewModels.Info;
using SushiShop.Ios.Common;

namespace SushiShop.Ios.Views.ViewControllers.Info
{
    [MvxTabPresentation(WrapInNavigationController = true, TabIconName = ImageNames.InfoTabIcon)]
    public partial class InfoViewController : BaseViewController<InfoViewModel>
    {
    }
}


using BuildApps.Core.Mobile.MvvmCross.UIKit.Views.ViewControllers;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using SushiShop.Core.ViewModels.Info;

namespace SushiShop.Ios.Views.ViewControllers.Info
{
    [MvxTabPresentation(WrapInNavigationController = true)]
    public partial class InfoViewController : BaseViewController<InfoViewModel>
    {
    }
}


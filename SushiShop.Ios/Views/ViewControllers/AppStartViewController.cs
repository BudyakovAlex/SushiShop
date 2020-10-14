using BuildApps.Core.Mobile.MvvmCross.UIKit.Views.ViewControllers;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using SushiShop.Core.ViewModels;

namespace SushiShop.Ios.Views.ViewControllers
{
    [MvxRootPresentation(WrapInNavigationController = false)]
    public partial class AppStartViewController : BaseViewController<AppStartViewModel>
    {
    }
}
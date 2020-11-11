using BuildApps.Core.Mobile.MvvmCross.UIKit.Views.ViewControllers;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using SushiShop.Core.ViewModels.Profile;

namespace SushiShop.Ios.Views.ViewControllers.Profile
{
    [MvxTabPresentation(WrapInNavigationController = true)]
    public partial class ProfileViewController : BaseViewController<ProfileViewModel>
    {
    }
}

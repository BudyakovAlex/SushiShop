using BuildApps.Core.Mobile.MvvmCross.UIKit.Views.ViewControllers;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using SushiShop.Core.ViewModels.Profile;
using SushiShop.Ios.Common;

namespace SushiShop.Ios.Views.ViewControllers.Profile
{
    [MvxTabPresentation(WrapInNavigationController = true, TabIconName = ImageNames.ProfileTabIcon)]
    public partial class ProfileViewController : BaseViewController<ProfileViewModel>
    {
    }
}

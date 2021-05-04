using BuildApps.Core.Mobile.MvvmCross.UIKit.Views.Fragments;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using SushiShop.Core.ViewModels;
using SushiShop.Core.ViewModels.Profile;
using SushiShop.Droid.Views.Fragments.Abstract;

namespace SushiShop.Droid.Views.Fragments.Profile
{
    [MvxTabLayoutPresentation(
        TabLayoutResourceId = Resource.Id.main_tab_layout,
        ViewPagerResourceId = Resource.Id.main_view_pager,
        ActivityHostViewModelType = typeof(MainViewModel))]
    public class ProfileFragment : BaseFragment<ProfileViewModel>, ITabFragment
    {
        public ProfileFragment()
            : base(Resource.Layout.fragment_profile)
        {
        }

        public bool IsActivated { get; set; }
    }
}
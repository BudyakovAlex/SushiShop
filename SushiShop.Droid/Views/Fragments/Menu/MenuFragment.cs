using BuildApps.Core.Mobile.MvvmCross.UIKit.Views.Fragments;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using SushiShop.Core.ViewModels;
using SushiShop.Core.ViewModels.Menu;

namespace SushiShop.Droid.Views.Fragments.Menu
{
    [MvxTabLayoutPresentation(
        TabLayoutResourceId = Resource.Id.main_tab_layout,
        ViewPagerResourceId = Resource.Id.main_view_pager,
        ActivityHostViewModelType = typeof(MainViewModel))]
    public class MenuFragment : BaseFragment<MenuViewModel>
    {
        public MenuFragment()
            : base(Resource.Layout.fragment_menu)
        {
        }
    }
}

using BuildApps.Core.Mobile.MvvmCross.UIKit.Views.Fragments;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using SushiShop.Core.ViewModels;
using SushiShop.Core.ViewModels.Info;

namespace SushiShop.Droid.Views.Fragments.Info
{
    [MvxTabLayoutPresentation(
        TabLayoutResourceId = Resource.Id.main_tab_layout,
        ViewPagerResourceId = Resource.Id.main_view_pager,
        ActivityHostViewModelType = typeof(MainViewModel))]
    public class InfoFragment : BaseFragment<InfoViewModel>
    {
        public InfoFragment()
            : base(Resource.Layout.fragment_info)
        {
        }
    }
}
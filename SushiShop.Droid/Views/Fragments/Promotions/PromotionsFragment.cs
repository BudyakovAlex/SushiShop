using BuildApps.Core.Mobile.MvvmCross.UIKit.Views.Fragments;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using SushiShop.Core.ViewModels;
using SushiShop.Core.ViewModels.Promotions;

namespace SushiShop.Droid.Views.Fragments.Promotions
{
    [MvxTabLayoutPresentation(
        TabLayoutResourceId = Resource.Id.main_tab_layout,
        ViewPagerResourceId = Resource.Id.main_view_pager,
        ActivityHostViewModelType = typeof(MainViewModel))]
    public class PromotionsFragment : BaseFragment<PromotionsViewModel>
    {
        public PromotionsFragment()
            : base(Resource.Layout.fragment_promotions)
        {
        }
    }
}

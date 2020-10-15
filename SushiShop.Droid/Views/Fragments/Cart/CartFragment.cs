using BuildApps.Core.Mobile.MvvmCross.UIKit.Views.Fragments;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using SushiShop.Core.ViewModels;
using SushiShop.Core.ViewModels.Cart;

namespace SushiShop.Droid.Views.Fragments.Cart
{
    [MvxTabLayoutPresentation(
        TabLayoutResourceId = Resource.Id.main_tab_layout,
        ViewPagerResourceId = Resource.Id.main_view_pager,
        ActivityHostViewModelType = typeof(MainViewModel))]
    public class CartFragment : BaseFragment<CartViewModel>
    {
        public CartFragment()
            : base(Resource.Layout.fragment_cart)
        {
        }
    }
}
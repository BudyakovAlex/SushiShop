using Android.OS;
using Android.Views;
using Android.Widget;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Views.Fragments;
using MvvmCross.DroidX.RecyclerView;
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
        private MvxRecyclerView _listRecyclerView;
        private MvxRecyclerView _gridRecyclerView;
        private TextView _toolbarTitleTextView;

        public MenuFragment()
            : base(Resource.Layout.fragment_menu)
        {
        }

        protected override void InitializeViewPoroperties(View view, Bundle savedInstanceState)
        {
            base.InitializeViewPoroperties(view, savedInstanceState);

            _listRecyclerView = View.FindViewById<MvxRecyclerView>(Resource.Id.list_recycler_view);
            _gridRecyclerView = View.FindViewById<MvxRecyclerView>(Resource.Id.grid_recycler_view);
            _toolbarTitleTextView = View.FindViewById<TextView>(Resource.Id.toolbar_title_text_view);
            _listRecyclerView = View.FindViewById<MvxRecyclerView>(Resource.Id.list_recycler_view);
        }

        protected override void Bind()
        {
            base.Bind();
        }
    }
}
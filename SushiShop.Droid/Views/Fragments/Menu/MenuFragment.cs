using Android.OS;
using Android.Views;
using Android.Widget;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Views.Fragments;
using MvvmCross.Binding.BindingContext;
using MvvmCross.DroidX.RecyclerView;
using MvvmCross.Platforms.Android.Binding;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using SushiShop.Core.Extensions;
using SushiShop.Core.Resources;
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
        private MvxRecyclerView listRecyclerView;
        private MvxRecyclerView gridRecyclerView;
        private TextView toolbarTitleTextView;
        private ImageView chevronImageView;
        private ImageView changeModeImageView;

        public MenuFragment()
            : base(Resource.Layout.fragment_menu)
        {
        }

        protected override void InitializeViewPoroperties(View view, Bundle savedInstanceState)
        {
            base.InitializeViewPoroperties(view, savedInstanceState);

            toolbarTitleTextView = View.FindViewById<TextView>(Resource.Id.toolbar_title_text_view);
            chevronImageView = View.FindViewById<ImageView>(Resource.Id.chevron_image_view);
            changeModeImageView = View.FindViewById<ImageView>(Resource.Id.change_mode_image_view);

            InitializeListRecyclerView();
            InitializeGridRecyclerView();
        }

        protected override void Bind()
        {
            base.Bind();

            using var bindingSet = CreateBindingSet();

            bindingSet.Bind(toolbarTitleTextView).For(v => v.Text).To(vm => vm.CityName);
            bindingSet.Bind(toolbarTitleTextView).For(v => v.BindClick()).To(vm => vm.SelectCityCommand);
            bindingSet.Bind(chevronImageView).For(v => v.BindClick()).To(vm => vm.SelectCityCommand);
            bindingSet.Bind(changeModeImageView).For(v => v.BindClick()).To(vm => vm.SwitchPresentationCommand);
            bindingSet.Bind(changeModeImageView).For(v => v.BindDrawableId()).To(vm => vm.IsListMenuPresentation)
                      .WithConversion((bool isListMode) => isListMode ? Resource.Drawable.ic_list : Resource.Drawable.ic_grid);
        }

        private void InitializeListRecyclerView()
        {
            listRecyclerView = View.FindViewById<MvxRecyclerView>(Resource.Id.list_recycler_view);
        }

        private void InitializeGridRecyclerView()
        {
            gridRecyclerView = View.FindViewById<MvxRecyclerView>(Resource.Id.grid_recycler_view);
        }
    }
}
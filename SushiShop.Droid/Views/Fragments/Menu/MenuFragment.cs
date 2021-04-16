using Android.OS;
using Android.Views;
using Android.Widget;
using AndroidX.RecyclerView.Widget;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Adapter.TemplateSelectors;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Adapters;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Views.Fragments;
using MvvmCross.DroidX.RecyclerView;
using MvvmCross.Platforms.Android.Binding;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using SushiShop.Core.Extensions;
using SushiShop.Core.ViewModels;
using SushiShop.Core.ViewModels.Menu;
using SushiShop.Core.ViewModels.Menu.Items;
using SushiShop.Droid.Views.Decorators;
using SushiShop.Droid.Views.LayoutManagers;
using SushiShop.Droid.Views.ViewHolders.Menu.Grid;
using SushiShop.Droid.Views.ViewHolders.Menu.Simple;

namespace SushiShop.Droid.Views.Fragments.Menu
{
    [MvxTabLayoutPresentation(
        TabLayoutResourceId = Resource.Id.main_tab_layout,
        ViewPagerResourceId = Resource.Id.main_view_pager,
        ActivityHostViewModelType = typeof(MainViewModel))]
    public class MenuFragment : BaseFragment<MenuViewModel>
    {
        private RecycleViewBindableAdapter listAdapter;
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
                      .WithConversion((bool isListMode) => isListMode ? Resource.Drawable.ic_grid : Resource.Drawable.ic_list);

            bindingSet.Bind(listRecyclerView).For(v => v.ItemsSource).To(vm => vm.SimpleItems);
            bindingSet.Bind(gridRecyclerView).For(v => v.ItemsSource).To(vm => vm.Items);

            bindingSet.Bind(listRecyclerView).For(v => v.BindVisible()).To(vm => vm.IsListMenuPresentation);
            bindingSet.Bind(gridRecyclerView).For(v => v.BindHidden()).To(vm => vm.IsListMenuPresentation);
        }

        private void InitializeListRecyclerView()
        {
            listRecyclerView = View.FindViewById<MvxRecyclerView>(Resource.Id.list_recycler_view);
            listAdapter = new RecycleViewBindableAdapter((IMvxAndroidBindingContext)BindingContext);
            listRecyclerView.Adapter = listAdapter;

            var gridLayoutManager = new GridLayoutManager(Context, 2);
            gridLayoutManager.SetSpanSizeLookup(new DelegateGridLayoutManagerSpanSizeLookup(GetListRecyclerViewItemsSpan));

            listRecyclerView.SetLayoutManager(gridLayoutManager);

            listRecyclerView.ItemTemplateSelector = new TemplateSelector()
                .AddElement<CategoryMenuItemViewModel, SimpleMenuItemViewHolder>(Resource.Layout.item_simple_menu)
                .AddElement<MenuActionItemViewModel, SimpleMenuActionItemViewHolder>(Resource.Layout.item_simple_menu_action)
                .AddElement<GroupsMenuItemViewModel, SimpleMenuGroupsViewHolder>(Resource.Layout.item_grouped_menu);
        }

        private int GetListRecyclerViewItemsSpan(int position)
        {
            var itemType = listAdapter.GetItemViewType(position);
            return itemType switch
            {
                Resource.Layout.item_grouped_menu => 2,
                _ => 1,
            };
        }

        private void InitializeGridRecyclerView()
        {
            gridRecyclerView = View.FindViewById<MvxRecyclerView>(Resource.Id.grid_recycler_view);
            gridRecyclerView.Adapter = new RecycleViewBindableAdapter((IMvxAndroidBindingContext)BindingContext);

            var gridLayoutManager = new GridLayoutManager(Context, 2, LinearLayoutManager.Vertical, false);
            gridLayoutManager.SetSpanSizeLookup(new DelegateGridLayoutManagerSpanSizeLookup(GetGridRecyclerViewItemsSpan));
            gridRecyclerView.SetLayoutManager(gridLayoutManager);
            gridRecyclerView.AddItemDecoration(new MenuGridItemDecorator());

            gridRecyclerView.ItemTemplateSelector = new TemplateSelector()
               .AddElement<MenuPromotionListItemViewModel, MenuPromotionListItemViewHolder>(Resource.Layout.item_promotion_list)
               .AddElement<CategoryMenuItemViewModel, CategoryMenuItemViewHolder>(Resource.Layout.item_menu_category);
        }

        private int GetGridRecyclerViewItemsSpan(int position)
        {
            if (position == 0 ||
                position == 1)
            {
                return 2;
            }

            if (position % 3 == 0 ||
                position % 3 == 2)
            {
                return 1;
            }
            else
            {
                return 2;
            }
        }
    }
}
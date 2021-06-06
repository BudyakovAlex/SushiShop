using Android.OS;
using Android.Views;
using AndroidX.RecyclerView.Widget;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Adapter.TemplateSelectors;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Adapters;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Views.Fragments;
using MvvmCross.Commands;
using MvvmCross.DroidX.RecyclerView;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using SushiShop.Core.Resources;
using SushiShop.Core.ViewModels.Info;
using SushiShop.Core.ViewModels.Shops.Sections;
using SushiShop.Droid.Enums;
using SushiShop.Droid.Extensions;
using SushiShop.Droid.Presenter.Attributes;
using SushiShop.Droid.Views.Adapters;
using SushiShop.Droid.Views.LayoutManagers;
using SushiShop.Droid.Views.Listeners;
using SushiShop.Droid.Views.ViewHolders.Shops.Sections;
using Toolbar = AndroidX.AppCompat.Widget.Toolbar;

namespace SushiShop.Droid.Views.Fragments.Shops
{
    [NestedFragmentPresentation(FragmentContentId = Resource.Id.container_view)]
    public class ShopsFragment : BaseFragment<ShopsViewModel>
    {
        private Toolbar toolbar;
        private MvxRecyclerView tabsRecyclerView;
        private MvxRecyclerView contentRecyclerView;
        private TabsAdapter tabsAdapter;
        private MvxGuardedGridLayoutManager tabsLayoutManager;
        private MvxGuardedLinearLayoutManager contentLayoutManager;
        private SnapOnScrollListener scrollListener;
        private PagerSnapHelper snapHelper;

        public ShopsFragment()
            : base(Resource.Layout.fragment_shops)
        {
        }

        protected override void InitializeViewPoroperties(View view, Bundle savedInstanceState)
        {
            base.InitializeViewPoroperties(view, savedInstanceState);

            tabsRecyclerView = view.FindViewById<MvxRecyclerView>(Resource.Id.shops_tabs_recycler_view);
            contentRecyclerView = view.FindViewById<MvxRecyclerView>(Resource.Id.shops_content_recycler_view);
            toolbar = view.FindViewById<Toolbar>(Resource.Id.toolbar);

            InitializeTabsRecyclerView();
            InitializeContentRecyclerView();

            toolbar.Title = AppStrings.Shops;
        }

        protected override void Bind()
        {
            base.Bind();

            using var bindingSet = CreateBindingSet();

            bindingSet.Bind(tabsRecyclerView).For(v => v.ItemsSource).To(vm => vm.TabsTitles);
            bindingSet.Bind(contentRecyclerView).For(v => v.ItemsSource).To(vm => vm.Items);
            bindingSet.Bind(toolbar).For(v => v.BindBackNavigationItemCommand()).To(vm => vm.CloseCommand);
            bindingSet.Bind(tabsAdapter).For(v => v.SelectedIndex).To(vm => vm.SelectedIndex).TwoWay();
            bindingSet.Bind(tabsLayoutManager).For(v => v.SpanCount).To(vm => vm.TabsTitles.Count);
        }

        private void InitializeTabsRecyclerView()
        {
            tabsLayoutManager = new MvxGuardedGridLayoutManager(Context, 3);
            tabsRecyclerView.SetLayoutManager(tabsLayoutManager);

            tabsRecyclerView.Adapter = tabsAdapter = new TabsAdapter((IMvxAndroidBindingContext)BindingContext, Resource.Layout.item_shops_tab);
            tabsRecyclerView.ItemTemplateId = Resource.Layout.item_shops_tab;
            tabsAdapter.ItemClick = new MvxCommand<int>(OnTabClick);
        }
        
        private void InitializeContentRecyclerView()
        {
            contentRecyclerView.Adapter = new RecycleViewBindableAdapter((IMvxAndroidBindingContext)BindingContext);
            contentRecyclerView.ItemTemplateSelector = new TemplateSelector()
                .AddElement<MetroSectionViewModel, MetroSectionViewHolder>(Resource.Layout.item_metro_section)
                .AddElement<ShopsListSectionViewModel, ShopsListSectionViewHolder>(Resource.Layout.item_shops_list_section)
                .AddElement<ShopsOnMapSectionViewModel, ShopsOnMapSectionViewHolder>(Resource.Layout.item_shops_on_map_section);
            contentLayoutManager = new ScrollableMvxGuardedLinearLayoutManager(Context, CanScrollContentRecyclerView) { Orientation = MvxGuardedLinearLayoutManager.Horizontal };
            contentRecyclerView.SetLayoutManager(contentLayoutManager);

            snapHelper = new PagerSnapHelper();
            snapHelper.AttachToRecyclerView(contentRecyclerView);

            scrollListener = new SnapOnScrollListener(snapHelper, BehaviorScrollListener.NotifyOnScrollStateIdle, OnSnapPagerPositionChanged);
            contentRecyclerView.AddOnScrollListener(scrollListener);
        }

        private void OnTabClick(int position)
        {
            contentRecyclerView.ScrollToPosition(position);
            tabsLayoutManager.ScrollToPosition(position);
            scrollListener.Position = position;
        }

        private void OnSnapPagerPositionChanged(int position)
        {
            tabsAdapter.SelectedIndex = position;
            tabsLayoutManager.ScrollToPosition(position);
        }

        private bool CanScrollContentRecyclerView(ScrollableMvxGuardedLinearLayoutManager.ScrollDirection scrollDirection)
        {
            return scrollDirection switch
            {
                ScrollableMvxGuardedLinearLayoutManager.ScrollDirection.Horizontal => ViewModel.SelectedIndex != 0,
                ScrollableMvxGuardedLinearLayoutManager.ScrollDirection.Vertical => false,
                _ => true,
            };
        }
    }
}

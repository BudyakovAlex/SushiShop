using Android.OS;
using Android.Views;
using AndroidX.RecyclerView.Widget;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Adapter.TemplateSelectors;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Adapters;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Extensions;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Views.Fragments;
using MvvmCross.Commands;
using MvvmCross.DroidX.RecyclerView;
using MvvmCross.Platforms.Android.Binding;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using SushiShop.Core.ViewModels.Menu;
using SushiShop.Droid.Enums;
using SushiShop.Droid.Extensions;
using SushiShop.Droid.Views.Adapters;
using SushiShop.Droid.Views.Listeners;
using SushiShop.Droid.Views.ViewHolders.Menu.Products;
using Toolbar = AndroidX.AppCompat.Widget.Toolbar;

namespace SushiShop.Droid.Views.Fragments.Menu
{
    [MvxFragmentPresentation(
        FragmentContentId = Resource.Id.container_view,
        AddToBackStack = true,
        IsCacheableFragment = true)]
    public class ProductsFragment : BaseFragment<ProductsViewModel>
    {
        private Toolbar toolbar;
        private View loadingOverlayView;
        private MvxRecyclerView tabsRecyclerView;
        private MvxRecyclerView productsRecyclerView;
        private MvxGuardedLinearLayoutManager productsLayoutManager;
        private MvxGuardedLinearLayoutManager tabsLayoutManager;
        private PagerSnapHelper snapHelper;
        private SnapOnScrollListener scrollListener;
        private ProductsTabsAdapter productsTabsAdapter;

        public ProductsFragment()
            : base(Resource.Layout.fragment_products)
        {
        }

        protected override void InitializeViewPoroperties(View view, Bundle savedInstanceState)
        {
            base.InitializeViewPoroperties(view, savedInstanceState);

            toolbar = view.FindViewById<Toolbar>(Resource.Id.toolbar);
            loadingOverlayView = view.FindViewById<View>(Resource.Id.loading_overlay_view);

            InitializeTabsRecyclerView();
            InitializeProductsRecyclerView();
        }

        protected override void Bind()
        {
            base.Bind();

            using var bindingSet = CreateBindingSet();

            bindingSet.Bind(loadingOverlayView).For(v => v.BindVisible()).To(vm => vm.IsBusy);
            bindingSet.Bind(toolbar).For(v => v.Title).To(vm => vm.Title);
            bindingSet.Bind(toolbar).For(v => v.BindBackNavigationItemCommand()).To(vm => vm.CloseCommand);
            bindingSet.Bind(productsRecyclerView).For(v => v.ItemsSource).To(vm => vm.Items);
            bindingSet.Bind(tabsRecyclerView).For(v => v.ItemsSource).To(vm => vm.Filters);
            bindingSet.Bind(productsTabsAdapter).For(v => v.SelectedIndex).To(vm => vm.SelectedFilterIndex).TwoWay();
            bindingSet.Bind(tabsRecyclerView).For(v => v.BindVisible()).To(vm => vm.IsFiltersVisible);
        }

        private void InitializeTabsRecyclerView()
        {
            tabsRecyclerView = View.FindViewById<MvxRecyclerView>(Resource.Id.tabs_recycler_view);
            tabsLayoutManager = new MvxGuardedLinearLayoutManager(Context) { Orientation = MvxGuardedLinearLayoutManager.Horizontal };
            tabsRecyclerView.SetLayoutManager(tabsLayoutManager);
            tabsRecyclerView.Adapter = productsTabsAdapter = new ProductsTabsAdapter((IMvxAndroidBindingContext)BindingContext);
            tabsRecyclerView.ItemTemplateId = Resource.Layout.item_product_tab;
            productsTabsAdapter.ItemClick = new MvxCommand<int>(OnTabClick);
            tabsRecyclerView.OffsetLeftAndRight((int)Context.DpToPx(8));
        }

        private void InitializeProductsRecyclerView()
        {
            productsRecyclerView = View.FindViewById<MvxRecyclerView>(Resource.Id.products_filtered_recycler_view);
            productsRecyclerView.Adapter = new RecycleViewBindableAdapter((IMvxAndroidBindingContext)BindingContext);
            productsRecyclerView.ItemTemplateSelector = new TemplateSelector()
                .AddElement<FilteredProductsViewModel, FilteredProdutsItemViewHolder>(Resource.Layout.item_products_filtered_products_list);
            productsLayoutManager = new MvxGuardedLinearLayoutManager(Context) { Orientation = MvxGuardedLinearLayoutManager.Horizontal } ;
            productsRecyclerView.SetLayoutManager(productsLayoutManager);

            snapHelper = new PagerSnapHelper();
            snapHelper.AttachToRecyclerView(productsRecyclerView);

            scrollListener = new SnapOnScrollListener(snapHelper, BehaviorScrollListener.NotifyOnScrollStateIdle, OnSnapPagerPositionChanged);
            productsRecyclerView.AddOnScrollListener(scrollListener);
        }

        private void OnSnapPagerPositionChanged(int position)
        {
            productsTabsAdapter.SelectedIndex = position;
            tabsLayoutManager.ScrollToPosition(position);
        }

        private void OnTabClick(int position)
        {
            productsRecyclerView.ScrollToPosition(position);
            tabsLayoutManager.ScrollToPosition(position);
            scrollListener.Position = position;
        }
    }
}

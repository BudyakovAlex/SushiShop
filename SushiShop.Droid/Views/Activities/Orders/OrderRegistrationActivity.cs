using Android.App;
using Android.Views;
using AndroidX.AppCompat.Widget;
using AndroidX.RecyclerView.Widget;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Adapter.TemplateSelectors;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Adapters;
using MvvmCross.Binding.Combiners;
using MvvmCross.Commands;
using MvvmCross.DroidX.RecyclerView;
using MvvmCross.Platforms.Android.Binding;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using SushiShop.Core.Resources;
using SushiShop.Core.ViewModels.Orders;
using SushiShop.Core.ViewModels.Orders.Sections;
using SushiShop.Droid.Enums;
using SushiShop.Droid.Extensions;
using SushiShop.Droid.Views.Activities.Abstract;
using SushiShop.Droid.Views.Adapters;
using SushiShop.Droid.Views.Listeners;
using SushiShop.Droid.Views.ViewHolders.Orders;

namespace SushiShop.Droid.Views.Activities.Orders
{
    [Activity]
    public class OrderRegistrationActivity : BaseActivity<OrderRegistrationViewModel>
    {
        private Toolbar toolbar;
        private View loadingOverlayView;
        private MvxRecyclerView tabsRecyclerView;
        private MvxRecyclerView contentRecyclerView;
        private TabsAdapter tabsAdapter;
        private MvxGuardedGridLayoutManager tabsLayoutManager;
        private MvxGuardedLinearLayoutManager contentLayoutManager;
        private SnapOnScrollListener scrollListener;
        private PagerSnapHelper snapHelper;

        public OrderRegistrationActivity() : base(Resource.Layout.activity_order_registration)
        {
        }

        protected override void InitializeViewPoroperties()
        {
            base.InitializeViewPoroperties();

            loadingOverlayView = FindViewById<View>(Resource.Id.loading_overlay_view);
            tabsRecyclerView = FindViewById<MvxRecyclerView>(Resource.Id.tabs_recycler_view);
            contentRecyclerView = FindViewById<MvxRecyclerView>(Resource.Id.content_recycler_view);
            toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);

            InitializeTabsRecyclerView();
            InitializeContentRecyclerView();

            toolbar.Title = AppStrings.OrderRegistrationTitle;
        }

        protected override void Bind()
        {
            base.Bind();

            using var bindingSet = CreateBindingSet();

            bindingSet.Bind(tabsRecyclerView).For(v => v.ItemsSource).To(vm => vm.TabsTitles);
            bindingSet.Bind(contentRecyclerView).For(v => v.ItemsSource).To(vm => vm.Items);
            bindingSet.Bind(toolbar).For(v => v.BindBackNavigationItemCommand()).To(vm => vm.CloseCommand);
            bindingSet.Bind(tabsLayoutManager).For(v => v.SpanCount).To(vm => vm.TabsTitles.Count);
            bindingSet.Bind(loadingOverlayView).For(v => v.BindVisible()).ByCombining(
                new MvxOrValueCombiner(),
                vm => vm.PickupOrderSectionViewModel.ExecutionStateWrapper.IsBusy,
                vm => vm.DeliveryOrderSectionViewModel.ExecutionStateWrapper.IsBusy);
        }

        private void InitializeTabsRecyclerView()
        {
            tabsLayoutManager = new MvxGuardedGridLayoutManager(this, 2);
            tabsRecyclerView.SetLayoutManager(tabsLayoutManager);

            tabsRecyclerView.Adapter = tabsAdapter = new TabsAdapter((IMvxAndroidBindingContext)BindingContext, Resource.Layout.item_tab);
            tabsRecyclerView.ItemTemplateId = Resource.Layout.item_tab;
            tabsAdapter.ItemClick = new MvxCommand<int>(OnTabClick);
            tabsAdapter.SelectedIndex = 0;
        }

        private void InitializeContentRecyclerView()
        {
            contentRecyclerView.Adapter = new RecycleViewBindableAdapter((IMvxAndroidBindingContext)BindingContext);
            contentRecyclerView.ItemTemplateSelector = new TemplateSelector()
                .AddElement<DeliveryOrderSectionViewModel, DeliveryOrderSectionViewHolder>(Resource.Layout.item_delivery_order_section)
                .AddElement<PickupOrderSectionViewModel, PickupOrderSectionViewHolder>(Resource.Layout.item_pickup_order_section);

            contentLayoutManager = new MvxGuardedLinearLayoutManager(this) { Orientation = LinearLayoutManager.Horizontal };
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
    }
}
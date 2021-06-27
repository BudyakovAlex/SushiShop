using Android.App;
using Android.Views;
using Android.Widget;
using AndroidX.RecyclerView.Widget;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Adapter.TemplateSelectors;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Adapters;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Extensions;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Listeners;
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
using SushiShop.Droid.Views.LayoutManagers;
using SushiShop.Droid.Views.Listeners;
using SushiShop.Droid.Views.ViewHolders.Orders;
using System.Threading.Tasks;
using Toolbar = AndroidX.AppCompat.Widget.Toolbar;

namespace SushiShop.Droid.Views.Activities.Orders
{
    [Activity(WindowSoftInputMode = SoftInput.AdjustResize)]
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
        private View thanksOrderContainerView;
        private TextView thanksOrderTitleTextView;
        private TextView thanksOrderContentTextView;
        private TextView thanksOrderNumberTitleTextView;
        private TextView thanksOrderNumberTextView;
        private ImageView thanksOrderImageView;
        private Button goToCartButton;

        public OrderRegistrationActivity() : base(Resource.Layout.activity_order_registration)
        {
        }

        public OrderThanksSectionViewModel OrderThanksSection
        {
            set
            {
                thanksOrderContainerView.Visibility = value is null ? ViewStates.Gone : ViewStates.Visible;
            }
        }

        protected override void InitializeViewPoroperties()
        {
            base.InitializeViewPoroperties();

            loadingOverlayView = FindViewById<View>(Resource.Id.loading_overlay_view);
            tabsRecyclerView = FindViewById<MvxRecyclerView>(Resource.Id.tabs_recycler_view);
            contentRecyclerView = FindViewById<MvxRecyclerView>(Resource.Id.content_recycler_view);
            toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);

            thanksOrderContainerView = FindViewById<View>(Resource.Id.success_message_container_view);
            thanksOrderTitleTextView = FindViewById<TextView>(Resource.Id.thanks_for_order_text_view);
            thanksOrderContentTextView = FindViewById<TextView>(Resource.Id.description_text_view);
            thanksOrderNumberTitleTextView = FindViewById<TextView>(Resource.Id.order_number_title_text_view);
            thanksOrderNumberTextView = FindViewById<TextView>(Resource.Id.order_number_text_view);
            thanksOrderImageView = FindViewById<ImageView>(Resource.Id.order_registered_image_view);
            goToCartButton = FindViewById<Button>(Resource.Id.go_to_cart_button);
            goToCartButton.SetOnClickListener(new ViewOnClickListener(OnGoToCartButtonTappedAsync));
            goToCartButton.Text = AppStrings.OnMainPage;
            goToCartButton.SetRoundedCorners(this.DpToPx(25));

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

            bindingSet.Bind(thanksOrderImageView).For(v => v.BindUrl()).To(vm => vm.OrderThanksSectionViewModel.Image);
            bindingSet.Bind(thanksOrderTitleTextView).For(v => v.Text).To(vm => vm.OrderThanksSectionViewModel.Title);
            bindingSet.Bind(thanksOrderContentTextView).For(v => v.Text).To(vm => vm.OrderThanksSectionViewModel.Content);
            bindingSet.Bind(thanksOrderNumberTitleTextView).For(v => v.Text).To(vm => vm.OrderThanksSectionViewModel.OrderNumberTitle);
            bindingSet.Bind(thanksOrderNumberTextView).For(v => v.Text).To(vm => vm.OrderThanksSectionViewModel.OrderNumber);
            bindingSet.Bind(this).For(nameof(OrderThanksSection)).To(vm => vm.OrderThanksSectionViewModel);
        }

        private Task OnGoToCartButtonTappedAsync(View _)
        {
            ViewModel?.CloseCommand.Execute(null);
            MainActivity.Instance.ShowMainTab();
            return Task.CompletedTask;
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

            contentLayoutManager = new ScrollableMvxGuardedLinearLayoutManager(this, CanScrollContentRecyclerView) { Orientation = LinearLayoutManager.Horizontal};
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

        private bool CanScrollContentRecyclerView(ScrollDirection scrollDirection)
        {
            return scrollDirection switch
            {
                ScrollDirection.Vertical => false,
                _ => true,
            };
        }
    }
}
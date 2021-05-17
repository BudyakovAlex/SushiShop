using Android.OS;
using Android.Views;
using AndroidX.AppCompat.Widget;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Adapter.TemplateSelectors;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Adapters;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Views.Fragments;
using MvvmCross.DroidX;
using MvvmCross.DroidX.RecyclerView;
using MvvmCross.Platforms.Android.Binding;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using SushiShop.Core.ViewModels.Orders;
using SushiShop.Core.ViewModels.Orders.Items;
using SushiShop.Droid.Presenter.Attributes;
using SushiShop.Droid.Views.ViewHolders.Orders;

namespace SushiShop.Droid.Views.Fragments.Orders
{
    [NestedFragmentPresentation(FragmentContentId = Resource.Id.container_view)]
    public class MyOrdersFragment : BaseFragment<MyOrdersViewModel>
    {
        private Toolbar toolbar;
        private View loadingOverlayView;
        private MvxSwipeRefreshLayout swipeRefreshLayout;
        private MvxRecyclerView recyclerView;

        public MyOrdersFragment() : base(Resource.Layout.fragment_my_orders)
        {
        }

        protected override void InitializeViewPoroperties(View view, Bundle savedInstanceState)
        {
            base.InitializeViewPoroperties(view, savedInstanceState);

            toolbar = view.FindViewById<Toolbar>(Resource.Id.toolbar);
            loadingOverlayView = view.FindViewById<View>(Resource.Id.loading_overlay_view);
            swipeRefreshLayout = view.FindViewById<MvxSwipeRefreshLayout>(Resource.Id.refresh_layout);

            InitializeRecyclerView(view);
        }

        protected override void Bind()
        {
            base.Bind();

            using var bindingSet = CreateBindingSet();

            bindingSet.Bind(loadingOverlayView).For(v => v.BindVisible()).To(v => v.IsBusy);
            bindingSet.Bind(toolbar).For(v => v.Title).To(v => v.Title);
            bindingSet.Bind(recyclerView).For(v => v.ItemsSource).To(vm => vm.Items);
            bindingSet.Bind(swipeRefreshLayout).For(v => v.Refreshing).To(vm => vm.IsRefreshing);
            bindingSet.Bind(swipeRefreshLayout).For(v => v.RefreshCommand).To(vm => vm.RefreshDataCommand);
        }

        private void InitializeRecyclerView(View view)
        {
            recyclerView = view.FindViewById<MvxRecyclerView>(Resource.Id.recycler_view);
            recyclerView.Adapter = new RecycleViewBindableAdapter((IMvxAndroidBindingContext)BindingContext);
            recyclerView.ItemTemplateSelector = new TemplateSelector()
                .AddElement<OrderItemViewModel, OrderItemViewHolder>(Resource.Layout.item_order);
        }
    }
}

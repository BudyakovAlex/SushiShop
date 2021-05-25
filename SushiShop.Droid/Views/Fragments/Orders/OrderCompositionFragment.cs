﻿using Android.OS;
using Android.Views;
using AndroidX.AppCompat.Widget;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Adapter.TemplateSelectors;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Adapters;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Views.Fragments;
using MvvmCross.DroidX.RecyclerView;
using MvvmCross.Platforms.Android.Binding;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using SushiShop.Core.ViewModels.Orders;
using SushiShop.Core.ViewModels.Orders.Items;
using SushiShop.Droid.Extensions;
using SushiShop.Droid.Presenter.Attributes;
using SushiShop.Droid.Views.ViewHolders.Orders;

namespace SushiShop.Droid.Views.Fragments.Orders
{
    [NestedFragmentPresentation(FragmentContentId = Resource.Id.order_details_container_view)]
    public class OrderCompositionFragment : BaseFragment<OrderCompositionViewModel>
    {
        private Toolbar toolbar;
        private View loadingOverlayView;
        private MvxRecyclerView recyclerView;

        public OrderCompositionFragment() : base(Resource.Layout.fragment_order_composition)
        {
        }

        protected override void InitializeViewPoroperties(View view, Bundle savedInstanceState)
        {
            base.InitializeViewPoroperties(view, savedInstanceState);

            toolbar = view.FindViewById<Toolbar>(Resource.Id.toolbar);
            loadingOverlayView = view.FindViewById<View>(Resource.Id.loading_overlay_view);

            InitializeRecyclerView(view);
        }

        protected override void Bind()
        {
            base.Bind();

            using var bindingSet = CreateBindingSet();

            bindingSet.Bind(loadingOverlayView).For(v => v.BindVisible()).To(v => v.IsBusy);
            bindingSet.Bind(toolbar).For(v => v.Title).To(v => v.Title);
            bindingSet.Bind(toolbar).For(v => v.BindBackNavigationItemCommand()).To(vm => vm.CloseCommand);
            bindingSet.Bind(recyclerView).For(v => v.ItemsSource).To(vm => vm.Items);
        }

        private void InitializeRecyclerView(View view)
        {
            recyclerView = view.FindViewById<MvxRecyclerView>(Resource.Id.recycler_view);
            recyclerView.Adapter = new RecycleViewBindableAdapter((IMvxAndroidBindingContext)BindingContext);
            recyclerView.ItemTemplateSelector = new TemplateSelector()
                .AddElement<OrderProductItemViewModel, OrderProductItemViewHolder>(Resource.Layout.item_order_product);
        }
    }
}
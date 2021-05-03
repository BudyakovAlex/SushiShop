using Android.OS;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.Widget;
using AndroidX.RecyclerView.Widget;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Adapter.TemplateSelectors;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Adapters;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Views.Fragments;
using MvvmCross.DroidX;
using MvvmCross.DroidX.RecyclerView;
using MvvmCross.Platforms.Android.Binding;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using SushiShop.Core.ViewModels;
using SushiShop.Core.ViewModels.Promotions;
using SushiShop.Core.ViewModels.Promotions.Items;
using SushiShop.Droid.Views.LayoutManagers;
using SushiShop.Droid.Views.ViewHolders.Promotions;

namespace SushiShop.Droid.Views.Fragments.Promotions
{
    [MvxTabLayoutPresentation(
        TabLayoutResourceId = Resource.Id.main_tab_layout,
        ViewPagerResourceId = Resource.Id.main_view_pager,
        ActivityHostViewModelType = typeof(MainViewModel))]
    public class PromotionsFragment : BaseFragment<PromotionsViewModel>
    {
        private MvxRecyclerView recyclerView;
        private MvxSwipeRefreshLayout swipeRefreshLayout;
        private TextView toolbarTitleTextView;
        private View loadingOverlayView;

        public PromotionsFragment()
            : base(Resource.Layout.fragment_promotions)
        {
        }

        protected override void InitializeViewPoroperties(View view, Bundle savedInstanceState)
        {
            base.InitializeViewPoroperties(view, savedInstanceState);

            loadingOverlayView = View.FindViewById<View>(Resource.Id.loading_overlay_view);
            swipeRefreshLayout = View.FindViewById<MvxSwipeRefreshLayout>(Resource.Id.refresh_layout);
            toolbarTitleTextView = View.FindViewById<TextView>(Resource.Id.toolbar_title_text_view);

            InitializeListRecyclerView();
        }

        protected override void Bind()
        {
            base.Bind();

            using var bindingSet = CreateBindingSet();

            bindingSet.Bind(toolbarTitleTextView).For(v => v.Text).To(vm => vm.Title);
            bindingSet.Bind(recyclerView).For(v => v.ItemsSource).To(vm => vm.Items);
            bindingSet.Bind(loadingOverlayView).For(v => v.BindVisible()).To(vm => vm.IsBusy);
            bindingSet.Bind(swipeRefreshLayout).For(v => v.Refreshing).To(vm => vm.IsRefreshing);
            bindingSet.Bind(swipeRefreshLayout).For(v => v.RefreshCommand).To(vm => vm.RefreshDataCommand);
        }

        private void InitializeListRecyclerView()
        {
            recyclerView = View.FindViewById<MvxRecyclerView>(Resource.Id.recycler_view);
            var adapter = new RecycleViewBindableAdapter((IMvxAndroidBindingContext)BindingContext);
            recyclerView.Adapter = adapter;

            var gridLayoutManager = new GridLayoutManager(Context, 2);
            gridLayoutManager.SetSpanSizeLookup(new DelegateGridLayoutManagerSpanSizeLookup(GetRecyclerViewItemsSpan));

            recyclerView.SetLayoutManager(gridLayoutManager);

            recyclerView.ItemTemplateSelector = new TemplateSelector()
                .AddElement<PromotionItemViewModel, PromotionItemViewHolder>(Resource.Layout.item_promotion);
        }

        private int GetRecyclerViewItemsSpan(int position)
        {
            if (position == 0)
            {
                return 2;
            }

            if (position % 3 == 0)
            {
                return 2;
            }
            else
            {
                return 1;
            }
        }
    }
}
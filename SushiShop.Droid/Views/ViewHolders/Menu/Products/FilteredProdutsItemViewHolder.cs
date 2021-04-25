using Android.Graphics;
using Android.Views;
using AndroidX.RecyclerView.Widget;
using AndroidX.SwipeRefreshLayout.Widget;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Adapter.TemplateSelectors;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Adapters;
using MvvmCross.Commands;
using MvvmCross.DroidX;
using MvvmCross.DroidX.RecyclerView;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using SushiShop.Core.ViewModels.Menu;
using SushiShop.Core.ViewModels.Menu.Items;
using SushiShop.Droid.Views.Decorators;
using SushiShop.Droid.Views.Listeners;
using SushiShop.Droid.Views.ViewHolders.Abstract;

namespace SushiShop.Droid.Views.ViewHolders.Menu.Products
{
    public class FilteredProdutsItemViewHolder : CardViewHolder<FilteredProductsViewModel>
    {
        private int itemSpace;
        private MvxSwipeRefreshLayout swipeRefreshLayout;
        private MvxRecyclerView productsRecyclerView;
        private RecycleViewBindableAdapter productsRecyclerViewAdapter;
        private HideTabLayoutRecyclerViewScrollListener tabLayoutRecyclerViewRecyclerViewListener;

        public FilteredProdutsItemViewHolder(View view, IMvxAndroidBindingContext context) : base(view, context)
        {
        }

        protected override void DoInit(View view)
        {
            base.DoInit(view);

            swipeRefreshLayout = view.FindViewById<MvxSwipeRefreshLayout>(Resource.Id.refresh_layout);

            productsRecyclerView = view.FindViewById<MvxRecyclerView>(Resource.Id.products_recycler_view);
            productsRecyclerView.Adapter = productsRecyclerViewAdapter = new RecycleViewBindableAdapter((IMvxAndroidBindingContext)BindingContext);
            productsRecyclerView.ItemTemplateSelector = new TemplateSelector()
                .AddElement<ProductItemViewModel, MenuProductItemViewHolder>(Resource.Layout.item_products_product);

            var gridLayoutManager = new GridLayoutManager(view.Context, 2);
            productsRecyclerView.SetLayoutManager(gridLayoutManager);

            productsRecyclerView.AddItemDecoration(new SpacesItemDecoration(CalculateItemMargin));
            itemSpace = (int)view.Context.Resources.GetDimension(Resource.Dimension.product_item_margin);

            tabLayoutRecyclerViewRecyclerViewListener = new HideTabLayoutRecyclerViewScrollListener();
            productsRecyclerView.AddOnScrollListener(tabLayoutRecyclerViewRecyclerViewListener);
            productsRecyclerViewAdapter.ItemClick = new MvxCommand(OnItemClick);
        }

        public override void BindData()
        {
            base.BindData();

            using var bindingSet = CreateBindingSet();

            bindingSet.Bind(productsRecyclerView).For(v => v.ItemsSource).To(vm => vm.Items);
            bindingSet.Bind(swipeRefreshLayout).For(v => v.Refreshing).To(vm => vm.IsRefreshing);
            bindingSet.Bind(swipeRefreshLayout).For(v => v.RefreshCommand).To(vm => vm.RefreshDataCommand);
        }

        private void OnItemClick()
        {
            tabLayoutRecyclerViewRecyclerViewListener.ResetPosition();
        }

        private Rect CalculateItemMargin(int position, Rect rect)
        {
            rect.Top = position == 0 || position == 1 ? itemSpace : 0;
            rect.Left = position % 2 == 1 ? 0 : itemSpace;
            rect.Right = itemSpace;
            rect.Bottom = itemSpace;
            return rect;
        }
    }
}

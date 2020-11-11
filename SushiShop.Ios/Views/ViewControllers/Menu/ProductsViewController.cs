using BuildApps.Core.Mobile.MvvmCross.UIKit.Views.ViewControllers;
using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Binding;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using SushiShop.Core.ViewModels.Menu;
using SushiShop.Ios.Delegates;
using SushiShop.Ios.Extensions;
using SushiShop.Ios.Sources;
using SushiShop.Ios.Views.Cells.Menu;
using UIKit;

namespace SushiShop.Ios.Views.ViewControllers.Menu
{
    [MvxChildPresentation]
    public partial class ProductsViewController : BaseViewController<ProductsViewModel>
    {
        private CollectionViewSource source;

        protected override void InitStylesAndContent()
        {
            base.InitStylesAndContent();

            InitializeCollectionView();
            FilterTabView.OnTabChangedAfterTapAction = OnTabChangedAfterTap;
        }

        protected override void Bind()
        {
            base.Bind();

            var bindingSet = this.CreateBindingSet<ProductsViewController, ProductsViewModel>();

            bindingSet.Bind(this).For(v => v.Title).To(vm => vm.Title);
            bindingSet.Bind(FilterTabView).For(v => v.Items).To(vm => vm.Filters);
            bindingSet.Bind(FilterTabView).For(v => v.SelectedIndex).To(vm => vm.SelectedFilterIndex);
            bindingSet.Bind(FilterTabView).For(v => v.BindVisible()).To(vm => vm.IsFiltersVisible);
            bindingSet.Bind(source).For(v => v.ItemsSource).To(vm => vm.Items);
            bindingSet.Bind(LoadingView).For(v => v.BindVisible()).To(vm => vm.IsLoading);

            bindingSet.Apply();
        }

        private void OnTabChangedAfterTap()
        {
            CollectionView.ScrollToItem(NSIndexPath.FromRowSection(FilterTabView.SelectedIndex, 0), UICollectionViewScrollPosition.CenteredHorizontally, true);
        }

        private void InitializeCollectionView()
        {
            source = new CollectionViewSource(CollectionView)
                .Register<FilteredProductsViewModel>(FilteredProductsItemViewCell.Nib, FilteredProductsItemViewCell.Key);

            CollectionView.Source = source;
            CollectionView.Delegate = new GroupsMenuItemCollectionViewDelegateFlowLayout(() => { }, OnDecelerated);
        }

        private void OnDecelerated()
        {
            var indexPath = CollectionView.GetCenterIndexPathOrDefault();
            if (indexPath != null && FilterTabView.SelectedIndex != indexPath.Row)
            {
                FilterTabView.SelectedIndex = indexPath.Row;
            }
        }
    }
}

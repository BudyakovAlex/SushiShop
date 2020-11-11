using BuildApps.Core.Mobile.MvvmCross.UIKit.Views.Cells;
using Foundation;
using MvvmCross.Binding.BindingContext;
using SushiShop.Core.ViewModels.Menu;
using SushiShop.Core.ViewModels.Menu.Items;
using SushiShop.Ios.Delegates;
using SushiShop.Ios.Sources;
using System;
using UIKit;

namespace SushiShop.Ios.Views.Cells.Menu
{
    public partial class FilteredProductsItemViewCell : BaseCollectionViewCell
    {
        public static readonly NSString Key = new NSString("FilteredProductsItemViewCell");
        public static readonly UINib Nib;

        private CollectionViewSource source;

        static FilteredProductsItemViewCell()
        {
            Nib = UINib.FromName("FilteredProductsItemViewCell", NSBundle.MainBundle);
        }

        protected FilteredProductsItemViewCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        protected override void Initialize()
        {
            base.Initialize();
            InitializeCollectionView();
        }

        protected override void Bind()
        {
            base.Bind();

            var bindingSet = this.CreateBindingSet<FilteredProductsItemViewCell, FilteredProductsViewModel>();

            bindingSet.Bind(source).For(v => v.ItemsSource).To(vm => vm.Items);

            bindingSet.Apply();
        }

        private void InitializeCollectionView()
        {
            source = new CollectionViewSource(ProductsCollectionView)
                .Register<ProductItemViewModel>(ProductItemViewCell.Nib, ProductItemViewCell.Key);

            ProductsCollectionView.Source = source;
            ProductsCollectionView.Delegate = new ProductCollectionViewDelegateFlowLayout();
        }
    }
}
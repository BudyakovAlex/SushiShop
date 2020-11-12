using System;
using Foundation;
using MvvmCross.Platforms.Ios.Binding.Views;
using SushiShop.Ios.Views.Cells.Menu;
using UIKit;

namespace SushiShop.Ios.Sources
{
    public class ProductsCollectionViewSource : MvxCollectionViewSource
    {
        private readonly Func<bool> listenScrollChanges;

        public ProductsCollectionViewSource(UICollectionView collectionView, Func<bool> listenScrollChanges)
            : base(collectionView)
        {
            this.listenScrollChanges = listenScrollChanges;

            collectionView.RegisterNibForCell(FilteredProductsItemViewCell.Nib, FilteredProductsItemViewCell.Key);
        }

        protected override UICollectionViewCell GetOrCreateCellFor(UICollectionView collectionView, NSIndexPath indexPath, object item)
        {
            var cell = (FilteredProductsItemViewCell) collectionView.DequeueReusableCell(FilteredProductsItemViewCell.Key, indexPath);
            cell.ListenScrollChanges = listenScrollChanges;

            return cell;
        }
    }
}

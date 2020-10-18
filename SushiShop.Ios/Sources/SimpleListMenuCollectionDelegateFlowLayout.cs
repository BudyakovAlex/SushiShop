using CoreGraphics;
using Foundation;
using MvvmCross.Platforms.Ios.Binding.Views;
using SushiShop.Ios.Views.Cells.Menu;
using System.Linq;
using SushiShop.Core.Extensions;
using UIKit;
using SushiShop.Core.ViewModels.Menu.Items;
using System;

namespace SushiShop.Ios.Sources
{
    public class SimpleListMenuCollectionDelegateFlowLayout : UICollectionViewDelegateFlowLayout
    {
        private readonly MvxCollectionViewSource collectionViewSource;

        public SimpleListMenuCollectionDelegateFlowLayout(MvxCollectionViewSource collectionViewSource)
        {
            this.collectionViewSource = collectionViewSource;
        }

        public override nfloat GetMinimumLineSpacingForSection(UICollectionView collectionView, UICollectionViewLayout layout, nint section)
        {
            return 0;
        }

        public override nfloat GetMinimumInteritemSpacingForSection(UICollectionView collectionView, UICollectionViewLayout layout, nint section)
        {
            return 0;
        }

        public override UIEdgeInsets GetInsetForSection(UICollectionView collectionView, UICollectionViewLayout layout, nint section)
        {
            return new UIEdgeInsets(12, 0, 0, 0);
        }

        public override CGSize GetSizeForItem(UICollectionView collectionView, UICollectionViewLayout layout, NSIndexPath indexPath)
        {
            var viewModel = collectionViewSource.ItemsSource.ElementAtOrDefault(indexPath.Row);
            return viewModel switch
            {
                MenuItemViewModel _ => new CGSize(collectionView.Frame.Width / 2, 48),
                _ => base.GetSizeForItem(collectionView, layout, indexPath)
            };
        }
    }
}

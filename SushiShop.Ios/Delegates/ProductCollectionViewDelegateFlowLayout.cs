using System;
using CoreGraphics;
using Foundation;
using UIKit;

namespace SushiShop.Ios.Delegates
{
    public class ProductCollectionViewDelegateFlowLayout : UICollectionViewDelegateFlowLayout
    {
        private const float InteritemSpacing = 7f;
        private const float SideInset = 8f;
        private const float CellHeight = 260f;

        public override nfloat GetMinimumInteritemSpacingForSection(UICollectionView collectionView, UICollectionViewLayout layout, nint section)
        {
            return InteritemSpacing;
        }

        public override nfloat GetMinimumLineSpacingForSection(UICollectionView collectionView, UICollectionViewLayout layout, nint section)
        {
            return 8f;
        }

        public override UIEdgeInsets GetInsetForSection(UICollectionView collectionView, UICollectionViewLayout layout, nint section)
        {
            return new UIEdgeInsets(8f, SideInset, 8f, SideInset);
        }

        public override CGSize GetSizeForItem(UICollectionView collectionView, UICollectionViewLayout layout, NSIndexPath indexPath)
        {
            var width = ((collectionView.Bounds.Width - InteritemSpacing) / 2f) - SideInset;
            return new CGSize(width, CellHeight);
        }
    }
}

using CoreGraphics;
using Foundation;
using System;
using UIKit;

namespace SushiShop.Ios.Delegates
{
    public class PhotoDetailsCollectionViewDelegateFlowLayout : UICollectionViewDelegateFlowLayout
    {
        public override nfloat GetMinimumInteritemSpacingForSection(UICollectionView collectionView, UICollectionViewLayout layout, nint section) => 0;

        public override nfloat GetMinimumLineSpacingForSection(UICollectionView collectionView, UICollectionViewLayout layout, nint section) => 0;

        public override UIEdgeInsets GetInsetForSection(UICollectionView collectionView, UICollectionViewLayout layout, nint section) =>
            UIEdgeInsets.Zero;

        public override CGSize GetSizeForItem(UICollectionView collectionView, UICollectionViewLayout layout, NSIndexPath indexPath) =>
            collectionView.Bounds.Size;
    }
}

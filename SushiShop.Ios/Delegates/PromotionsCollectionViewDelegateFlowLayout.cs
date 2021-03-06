using System;
using CoreGraphics;
using Foundation;
using UIKit;

namespace SushiShop.Ios.Delegates
{
    public class PromotionsCollectionViewDelegateFlowLayout : UICollectionViewDelegateFlowLayout
    {
        private const float Inset = 16f;
        private const float InteritemSpacing = 9f;

        private int position;

        public override UIEdgeInsets GetInsetForSection(UICollectionView collectionView, UICollectionViewLayout layout, nint section)
        {
            return new UIEdgeInsets(Inset, Inset, Inset, Inset);
        }

        public override nfloat GetMinimumInteritemSpacingForSection(UICollectionView collectionView, UICollectionViewLayout layout, nint section)
        {
            return InteritemSpacing;
        }

        public override nfloat GetMinimumLineSpacingForSection(UICollectionView collectionView, UICollectionViewLayout layout, nint section)
        {
            return 9f;
        }

        public override CGSize GetSizeForItem(UICollectionView collectionView, UICollectionViewLayout layout, NSIndexPath indexPath)
        {
            if (position == 0)
            {
                ++position;

                var size = collectionView.Bounds.Width - (Inset * 2f);
                return new CGSize(size, size);
            }
            else
            {
                position = position == 2 ? 0 : position + 1;

                var size = (collectionView.Bounds.Width - (Inset * 2f) - InteritemSpacing) / 2f;
                return new CGSize(size, size);
            }
        }
    }
}

using System;
using CoreGraphics;
using Foundation;
using UIKit;

namespace SushiShop.Ios.Delegates
{
    public class ShopsContentCollectionViewDelegateFlowLayout : UICollectionViewDelegateFlowLayout
    {
        private readonly Action onDecelereted;

        public ShopsContentCollectionViewDelegateFlowLayout(Action onDecelereted)
        {
            this.onDecelereted = onDecelereted;
        }

        public override nfloat GetMinimumLineSpacingForSection(UICollectionView collectionView, UICollectionViewLayout layout, nint section)
        {
            return 0f;
        }

        public override CGSize GetSizeForItem(UICollectionView collectionView, UICollectionViewLayout layout, NSIndexPath indexPath)
        {
            return collectionView.Bounds.Size;
        }

        public override void DecelerationEnded(UIScrollView scrollView)
        {
            onDecelereted?.Invoke();
        }

        public override void DraggingEnded(UIScrollView scrollView, bool willDecelerate)
        {
            if (willDecelerate)
            {
                return;
            }

            onDecelereted?.Invoke();
        }
    }
}

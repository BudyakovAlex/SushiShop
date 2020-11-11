using CoreGraphics;
using Foundation;
using System;
using UIKit;

namespace SushiShop.Ios.Delegates
{
    public class GroupsMenuItemCollectionViewDelegateFlowLayout : UICollectionViewDelegateFlowLayout
    {
        private readonly Action onScrolled;
        private readonly Action onDecelereted;

        public GroupsMenuItemCollectionViewDelegateFlowLayout(Action onScrolled, Action onDecelereted = null)
        {
            this.onScrolled = onScrolled;
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

        public override void Scrolled(UIScrollView scrollView) =>
            onScrolled();

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

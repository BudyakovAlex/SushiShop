using System;
using CoreGraphics;
using Foundation;
using UIKit;

namespace SushiShop.Ios.Delegates
{
    public class GroupsMenuItemCollectionViewDelegateFlowLayout : UICollectionViewDelegateFlowLayout
    {
        private readonly Action onScrolled;

        public GroupsMenuItemCollectionViewDelegateFlowLayout(Action onScrolled)
        {
            this.onScrolled = onScrolled;
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
    }
}

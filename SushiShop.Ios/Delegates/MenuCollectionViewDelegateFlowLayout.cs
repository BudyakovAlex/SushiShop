using System;
using CoreGraphics;
using Foundation;
using SushiShop.Core.ViewModels.Menu.Items;
using SushiShop.Ios.Sources;
using UIKit;

namespace SushiShop.Ios.Delegates
{
    public class MenuCollectionViewDelegateFlowLayout : UICollectionViewDelegateFlowLayout
    {
        private const float GroupHeight = 180f;
        private const float CategoryHeight = 160f;
        private const float CategoryInteritemSpacing = 9f;
        private const float CategorySideInset = 16f;

        private readonly MenuCollectionViewSource viewSource;

        private int categoryPosition;

        public MenuCollectionViewDelegateFlowLayout(MenuCollectionViewSource viewSource)
        {
            this.viewSource = viewSource;
        }

        public override nfloat GetMinimumInteritemSpacingForSection(UICollectionView collectionView, UICollectionViewLayout layout, nint section)
        {
            return section == 0 ? 0f : CategoryInteritemSpacing;
        }

        public override nfloat GetMinimumLineSpacingForSection(UICollectionView collectionView, UICollectionViewLayout layout, nint section)
        {
            return section == 0 ? 0f : 10f;
        }

        public override UIEdgeInsets GetInsetForSection(UICollectionView collectionView, UICollectionViewLayout layout, nint section)
        {
            return section == 0
                ? UIEdgeInsets.Zero
                : new UIEdgeInsets(8f, CategorySideInset, 16f, CategorySideInset);
        }

        public override CGSize GetSizeForItem(UICollectionView collectionView, UICollectionViewLayout layout, NSIndexPath indexPath)
        {
            var item = viewSource.GetItem(indexPath);
            switch (item)
            {
                case GroupsMenuItemViewModel _:
                    categoryPosition = 0;
                    return new CGSize(collectionView.Bounds.Width, GroupHeight);

                case CategoryMenuItemViewModel _:
                    var availableWidth = collectionView.Bounds.Width - (CategorySideInset * 2f);
                    var width = categoryPosition == 0 ? availableWidth : (availableWidth - CategoryInteritemSpacing) / 2f;
                    UpdateCategoryPosition();

                    return new CGSize(width, CategoryHeight);

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void UpdateCategoryPosition()
        {
            if (categoryPosition == 2)
            {
                categoryPosition = 0;
            }
            else
            {
                categoryPosition++;
            }
        }
    }
}

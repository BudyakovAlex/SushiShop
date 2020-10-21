using System;
using System.Linq;
using BuildApps.Core.Mobile.Common.Extensions;
using CoreGraphics;
using Foundation;
using UIKit;

namespace SushiShop.Ios.Extensions
{
    public static class CollectionViewExtensions
    {
        public static NSIndexPath GetCenterIndexPathOrDefault(this UICollectionView collectionView)
        {
            var visibleIndexPaths = collectionView.IndexPathsForVisibleItems;
            if (visibleIndexPaths.IsEmpty())
            {
                return null;
            }

            if (visibleIndexPaths.Length == 1)
            {
                return visibleIndexPaths[0];
            }

            var x = (collectionView.Bounds.Width / 2f) + collectionView.ContentOffset.X;
            var indexPath = collectionView.IndexPathForItemAtPoint(new CGPoint(x, 0f));
            if (indexPath != null)
            {
                return indexPath;
            }

            var cell = collectionView.VisibleCells
                .Aggregate((firstCell, secondCell) =>
                {
                    var firstDistance = GetDistanceToX(firstCell, x);
                    var secondDistance = GetDistanceToX(secondCell, x);
                    return firstDistance <= secondDistance ? firstCell : secondCell;
                });

            return collectionView.IndexPathForCell(cell);

            static double GetDistanceToX(UICollectionViewCell cell, nfloat x)
            {
                var cellX = cell.Frame.X < x ? cell.Frame.X + cell.Frame.Width : cell.Frame.X;
                return Math.Abs(cellX - x);
            }
        }
    }
}

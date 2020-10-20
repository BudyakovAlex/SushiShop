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
                .Aggregate((cell1, cell2) =>
                {
                    var distance1 = GetDistanceToX(cell1, x);
                    var distance2 = GetDistanceToX(cell2, x);
                    return distance1 <= distance2 ? cell1 : cell2;
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

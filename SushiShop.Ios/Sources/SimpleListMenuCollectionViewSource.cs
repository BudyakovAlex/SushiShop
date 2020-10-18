using Foundation;
using MvvmCross.Platforms.Ios.Binding.Views;
using SushiShop.Ios.Views.Cells.Menu;
using UIKit;

namespace SushiShop.Ios.Sources
{
    public class SimpleListMenuCollectionViewSource : MvxCollectionViewSource
    {
        public SimpleListMenuCollectionViewSource(UICollectionView collectionView) : base(collectionView)
        {
        }

        public SimpleListMenuCollectionViewSource(UICollectionView collectionView, NSString defaultCellIdentifier) : base(collectionView, defaultCellIdentifier)
        {
        }

        protected override UICollectionViewCell GetOrCreateCellFor(UICollectionView collectionView, NSIndexPath indexPath, object item)
        {
            return (UICollectionViewCell)collectionView.DequeueReusableCell(SimpleMenuItemCell.Key, indexPath);
        } 
    }
}
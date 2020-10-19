using Foundation;
using MvvmCross.Platforms.Ios.Binding.Views;
using SushiShop.Core.ViewModels.Menu.Items;
using SushiShop.Ios.Views.Cells.Menu;
using System;
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
            var cell = item switch
            {
                MenuActionItemViewModel _ => collectionView.DequeueReusableCell(SimpleMenuActionItemCell.Key, indexPath),
                CategoryMenuItemViewModel _ => collectionView.DequeueReusableCell(SimpleMenuItemCell.Key, indexPath),
                GroupsMenuItemViewModel _ => collectionView.DequeueReusableCell(SimpleMenuGroupsCell.Key, indexPath),
                _ => throw new ArgumentException($"Failed to get cell for {item.GetType()} object"),
            };

            return (UICollectionViewCell)cell;
        } 
    }
}
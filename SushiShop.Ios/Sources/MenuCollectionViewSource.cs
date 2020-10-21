using System;
using Foundation;
using MvvmCross.Binding.Extensions;
using MvvmCross.Platforms.Ios.Binding.Views;
using SushiShop.Core.ViewModels.Menu.Items;
using SushiShop.Ios.Views.Cells.Menu;
using UIKit;

namespace SushiShop.Ios.Sources
{
    public class MenuCollectionViewSource : MvxCollectionViewSource
    {
        public MenuCollectionViewSource(UICollectionView collectionView)
            : base(collectionView)
        {
            CollectionView.RegisterNibForCell(MenuPromotionListItemViewCell.Nib, MenuPromotionListItemViewCell.Key);
            CollectionView.RegisterNibForCell(CategoryMenuItemViewCell.Nib, CategoryMenuItemViewCell.Key);
        }

        public override nint NumberOfSections(UICollectionView collectionView)
        {
            return 2;
        }

        public override nint GetItemsCount(UICollectionView collectionView, nint section)
        {
            var itemsCount = ItemsSource?.Count() ?? 0;
            if (itemsCount == 0)
            {
                return 0;
            }

            return section == 0 ? 1 : itemsCount - 1;
        }

        public object GetItem(NSIndexPath indexPath) =>
            GetItemAt(indexPath);

        protected override object GetItemAt(NSIndexPath indexPath)
        {
            var position = indexPath.Row + indexPath.Section;
            return ItemsSource.ElementAt(position);
        }

        protected override UICollectionViewCell GetOrCreateCellFor(UICollectionView collectionView, NSIndexPath indexPath, object item)
        {
            var reuseIdentifier = GetReuseIdentifier(item);
            return (UICollectionViewCell) collectionView.DequeueReusableCell(reuseIdentifier, indexPath);

            static NSString GetReuseIdentifier(object item) => item switch
            {
                MenuPromotionListItemViewModel _ => MenuPromotionListItemViewCell.Key,
                CategoryMenuItemViewModel _      => CategoryMenuItemViewCell.Key,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}

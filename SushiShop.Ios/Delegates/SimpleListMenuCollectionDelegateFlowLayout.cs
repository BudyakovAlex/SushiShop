using System;
using System.Linq;
using CoreGraphics;
using Foundation;
using MvvmCross.Platforms.Ios.Binding.Views;
using SushiShop.Core.Extensions;
using SushiShop.Core.ViewModels.Menu.Items;
using UIKit;

namespace SushiShop.Ios.Delegates
{
    public class SimpleListMenuCollectionDelegateFlowLayout : UICollectionViewDelegateFlowLayout
    {
        private const int SimpleCellHeight = 48;
        private const int GroupsCellHeight = 244;

        private readonly MvxCollectionViewSource collectionViewSource;

        public SimpleListMenuCollectionDelegateFlowLayout(MvxCollectionViewSource collectionViewSource)
        {
            this.collectionViewSource = collectionViewSource;
        }

        public override nfloat GetMinimumLineSpacingForSection(UICollectionView collectionView, UICollectionViewLayout layout, nint section)
        {
            return 0;
        }

        public override nfloat GetMinimumInteritemSpacingForSection(UICollectionView collectionView, UICollectionViewLayout layout, nint section)
        {
            return 0;
        }

        public override UIEdgeInsets GetInsetForSection(UICollectionView collectionView, UICollectionViewLayout layout, nint section)
        {
            return new UIEdgeInsets(12, 0, 0, 0);
        }

        public override CGSize GetSizeForItem(UICollectionView collectionView, UICollectionViewLayout layout, NSIndexPath indexPath)
        {
            var viewModel = collectionViewSource.ItemsSource.ElementAtOrDefault(indexPath.Row);
            return viewModel switch
            {
                MenuActionItemViewModel _ => new CGSize(collectionView.Frame.Width / 2, SimpleCellHeight),
                CategoryMenuItemViewModel _ => new CGSize(collectionView.Frame.Width / 2, SimpleCellHeight),
                GroupsMenuItemViewModel _ => new CGSize(collectionView.Frame.Width, GroupsCellHeight),
                _ => base.GetSizeForItem(collectionView, layout, indexPath)
            };
        }
    }
}
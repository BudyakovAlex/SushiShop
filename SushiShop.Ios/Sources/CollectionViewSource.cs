using System;
using System.Collections.Generic;
using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract;
using Foundation;
using MvvmCross.Platforms.Ios.Binding.Views;
using UIKit;

namespace SushiShop.Ios.Sources
{
    public class CollectionViewSource : MvxCollectionViewSource
    {
        private readonly Dictionary<Type, NSString> reuseIdentifierDictionary = new Dictionary<Type, NSString>();

        public CollectionViewSource(UICollectionView collectionView)
            : base(collectionView)
        {
        }

        public CollectionViewSource Register<TViewModel>(UINib nib, NSString reuseIdentifier)
            where TViewModel : BaseViewModel
        {
            reuseIdentifierDictionary.Add(typeof(TViewModel), reuseIdentifier);
            CollectionView.RegisterNibForCell(nib, reuseIdentifier);

            return this;
        }

        protected override UICollectionViewCell GetOrCreateCellFor(UICollectionView collectionView, NSIndexPath indexPath, object item)
        {
            var reuseIdentifier = GetReuseIdentifier(item);
            return (UICollectionViewCell) collectionView.DequeueReusableCell(reuseIdentifier, indexPath);
        }

        private NSString GetReuseIdentifier(object item)
        {
            var itemType = item.GetType();
            if (reuseIdentifierDictionary.TryGetValue(itemType, out var reuseIdentifier))
            {
                return reuseIdentifier;
            }

            throw new KeyNotFoundException(
                $"Failed to get a reuse identifier for {itemType}, please make sure to register it.");
        }
    }
}

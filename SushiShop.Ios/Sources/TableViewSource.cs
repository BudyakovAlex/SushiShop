using System;
using System.Collections.Generic;
using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract;
using CoreFoundation;
using Foundation;
using MvvmCross.Platforms.Ios.Binding.Views;
using SushiShop.Ios.Views.Cells.Interfaces;
using UIKit;

namespace SushiShop.Ios.Sources
{
    public class TableViewSource : MvxTableViewSource
    {
        private readonly Dictionary<Type, NSString> _reuseIdentifierDictionary = new Dictionary<Type, NSString>();

        public TableViewSource(UITableView tableView) : base(tableView)
        {
        }

        public TableViewSource Register<TViewModel>(UINib nib, NSString reuseIdentifier)
            where TViewModel : BaseViewModel
        {
            _reuseIdentifierDictionary.Add(typeof(TViewModel), reuseIdentifier);
            TableView.RegisterNibForCellReuse(nib, reuseIdentifier);

            return this;
        }

        protected override UITableViewCell GetOrCreateCellFor(UITableView tableView, NSIndexPath indexPath, object item)
        {
            var reuseIdentifier = GetReuseIdentifier(item);
            var cell = tableView.DequeueReusableCell(reuseIdentifier, indexPath);
            if (cell is IUpdatableViewCell updatedCell)
            {
                updatedCell.RefreshLayoutAction = ReloadCellOfTableView;
            }

            return cell;
        }

        private NSString GetReuseIdentifier(object item)
        {
            var itemType = item.GetType();
            if (_reuseIdentifierDictionary.TryGetValue(itemType, out var reuseIdentifier))
            {
                return reuseIdentifier;
            }

            throw new KeyNotFoundException(
                $"Failed to get a reuse identifier for {itemType}, please make sure to register it.");
        }

        private void ReloadCellOfTableView(UIView cell)
        {
            DispatchQueue.MainQueue.DispatchAsync(() =>
            {
                var indexPath = TableView?.IndexPathForCell((UITableViewCell)cell);
                if (indexPath == null)
                {
                    return;
                }

                TableView.ReloadRows(new[] { indexPath }, UITableViewRowAnimation.Fade);
            });
        }
    }
}

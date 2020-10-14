using Foundation;
using MvvmCross.Platforms.Ios.Binding.Views;
using SushiShop.Ios.Views.Cells;
using System;
using UIKit;

namespace SushiShop.Ios.Sources
{
    public class SelectableTableSource : MvxTableViewSource
    {
        private readonly UINib customCellNib;
        private readonly NSString customCellKey;

        public SelectableTableSource(UITableView tableView) : base(tableView)
        {
            tableView.RegisterNibForCellReuse(SelectableItemCell.Nib, SelectableItemCell.Key);
        }

        public SelectableTableSource(UITableView tableView, UINib customCellNib, NSString customCellKey) : base(tableView)
        {
            this.customCellNib = customCellNib;
            this.customCellKey = customCellKey;

            tableView.RegisterNibForCellReuse(customCellNib, customCellKey);
        }

        public SelectableTableSource(IntPtr handle) : base(handle)
        {
        }

        protected override UITableViewCell GetOrCreateCellFor(UITableView tableView, NSIndexPath indexPath, object item)
        {
            if (customCellNib is null || customCellKey is null)
            {
                return tableView.DequeueReusableCell(SelectableItemCell.Key);
            }

            return tableView.DequeueReusableCell(customCellKey);
        }
    }
}
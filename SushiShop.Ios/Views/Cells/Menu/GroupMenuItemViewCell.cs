using System;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Views.Cells;
using Foundation;
using UIKit;

namespace SushiShop.Ios.Views.Cells.Menu
{
    public partial class GroupMenuItemViewCell : BaseCollectionViewCell
    {
        public static readonly NSString Key = new NSString(nameof(GroupMenuItemViewCell));
        public static readonly UINib Nib = UINib.FromName(Key, NSBundle.MainBundle);

        protected GroupMenuItemViewCell(IntPtr handle)
            : base(handle)
        {
        }
    }
}

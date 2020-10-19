using System;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Views.Cells;
using Foundation;
using UIKit;

namespace SushiShop.Ios.Views.Cells.Menu
{
    public partial class SimpleMenuGroupsCell : BaseCollectionViewCell
    {
        public static readonly NSString Key = new NSString("SimpleMenuGroupsCell");
        public static readonly UINib Nib;

        static SimpleMenuGroupsCell()
        {
            Nib = UINib.FromName("SimpleMenuGroupsCell", NSBundle.MainBundle);
        }

        protected SimpleMenuGroupsCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }
    }
}

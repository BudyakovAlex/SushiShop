using System;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Views.Cells;
using Foundation;
using UIKit;

namespace SushiShop.Ios.Views.Cells.Menu
{
    public partial class SimpleMenuGroupCell : BaseCollectionViewCell
    {
        public static readonly NSString Key = new NSString("SimpleMenuGroupCell");
        public static readonly UINib Nib;

        static SimpleMenuGroupCell()
        {
            Nib = UINib.FromName("SimpleMenuGroupCell", NSBundle.MainBundle);
        }

        protected SimpleMenuGroupCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }
    }
}

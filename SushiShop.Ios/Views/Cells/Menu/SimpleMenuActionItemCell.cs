using System;
using Foundation;
using MvvmCross.Platforms.Ios.Binding.Views;
using UIKit;

namespace SushiShop.Ios.Views.Cells.Menu
{
    public partial class SimpleMenuActionItemCell : MvxTableViewCell
    {
        public static readonly NSString Key = new NSString("SimpleMenuActionItemCell");
        public static readonly UINib Nib;

        static SimpleMenuActionItemCell()
        {
            Nib = UINib.FromName("SimpleMenuActionItemCell", NSBundle.MainBundle);
        }

        protected SimpleMenuActionItemCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }
    }
}
using System;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Views.Cells;
using Foundation;
using MvvmCross.Binding.BindingContext;
using SushiShop.Core.ViewModels.Menu.Items;
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

        protected override void Bind()
        {
            base.Bind();

            var bindingSet = this.CreateBindingSet<GroupMenuItemViewCell, GroupMenuItemViewModel>();
            bindingSet.Bind(ImageView).For(v => v.ImagePath).To(vm => vm.ImageUrl);
            bindingSet.Apply();
        }
    }
}

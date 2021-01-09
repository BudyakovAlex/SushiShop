using System;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Views.Cells;
using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Binding;
using SushiShop.Core.ViewModels.Shops.Items;
using UIKit;

namespace SushiShop.Ios.Views.Cells.Shops
{
    public partial class MetroItemViewCell : BaseTableViewCell
    {
        public static readonly NSString Key = new NSString(nameof(MetroItemViewCell));
        public static readonly UINib Nib = UINib.FromName(Key, NSBundle.MainBundle);

        protected MetroItemViewCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        protected override void Bind()
        {
            base.Bind();

            var bindingSet = this.CreateBindingSet<MetroItemViewCell, MetroItemViewModel>();

            bindingSet.Bind(TitleLabel).For(v => v.Text).To(vm => vm.Text);
            bindingSet.Bind(this).For(v => v.BindTap()).To(vm => vm.GoToShopsCommand);

            bindingSet.Apply();
        }
    }
}

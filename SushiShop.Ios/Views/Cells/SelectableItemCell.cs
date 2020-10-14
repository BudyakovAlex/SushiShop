using BuildApps.Core.Mobile.MvvmCross.UIKit.Views.Cells;
using BuildApps.Core.Mobile.MvvmCross.ViewModels.Simple;
using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Binding;
using System;
using UIKit;

namespace SushiShop.Ios.Views.Cells
{
    public partial class SelectableItemCell : BaseTableViewCell
    {
        public static readonly NSString Key = new NSString("SelectableItemCell");
        public static readonly UINib Nib;

        static SelectableItemCell()
        {
            Nib = UINib.FromName("SelectableItemCell", NSBundle.MainBundle);
        }

        protected SelectableItemCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        protected override void DoBind()
        {
            base.DoBind();

            var bindingSet = this.CreateBindingSet<SelectableItemCell, SelectableItemViewModel>();

            bindingSet.Bind(TitleLabel).For(v => v.Text).To(vm => vm.Text);
            bindingSet.Bind(CheckImageView).For(v => v.BindVisible()).To(vm => vm.IsSelected);

            bindingSet.Apply();
        }
    }
}
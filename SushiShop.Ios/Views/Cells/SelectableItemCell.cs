using System;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Views.Cells;
using BuildApps.Core.Mobile.MvvmCross.ViewModels.Simple;
using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Binding;
using UIKit;

namespace SushiShop.Ios.Views.Cells
{
    public partial class SelectableItemCell : BaseTableViewCell
    {
        public const float Height = 50f;

        public static readonly NSString Key = new NSString(nameof(SelectableItemCell));
        public static readonly UINib Nib = UINib.FromName(Key, NSBundle.MainBundle);

        protected SelectableItemCell(IntPtr handle)
            : base(handle)
        {
        }

        protected override void Bind()
        {
            base.Bind();

            var bindingSet = this.CreateBindingSet<SelectableItemCell, SelectableItemViewModel>();

            bindingSet.Bind(TitleLabel).For(v => v.Text).To(vm => vm.Text);
            bindingSet.Bind(CheckImageView).For(v => v.BindVisible()).To(vm => vm.IsSelected);

            bindingSet.Apply();
        }
    }
}
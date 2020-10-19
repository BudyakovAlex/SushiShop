using System;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Views.Cells;
using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Binding;
using SushiShop.Core.ViewModels.Menu.Items;
using UIKit;

namespace SushiShop.Ios.Views.Cells.Menu
{
    public partial class SimpleMenuActionItemCell : BaseCollectionViewCell
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

        protected override void Stylize()
        {
            base.Stylize();
            ContainerView.Layer.CornerRadius = 6;
        }

        protected override void DoBind()
        {
            base.DoBind();

            var bindingSet = this.CreateBindingSet<SimpleMenuActionItemCell, MenuActionItemViewModel>();

            bindingSet.Bind(NameLabel).For(v => v.Text).To(vm => vm.Title);
            bindingSet.Bind(ContainerView).For(v => v.BindTap()).To(vm => vm.ActionCommand);

            bindingSet.Apply();
        }
    }
}

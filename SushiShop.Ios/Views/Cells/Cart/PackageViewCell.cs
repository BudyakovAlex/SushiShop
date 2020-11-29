using System;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Views.Cells;
using Foundation;
using MvvmCross.Binding.BindingContext;
using SushiShop.Core.ViewModels.Cart.Items;
using UIKit;

namespace SushiShop.Ios.Views.Cells.Cart
{
    public partial class PackageViewCell : BaseTableViewCell
    {
        public static readonly NSString Key = new NSString("PackageViewCell");
        public static readonly UINib Nib;

        static PackageViewCell()
        {
            Nib = UINib.FromName("PackageViewCell", NSBundle.MainBundle);
        }

        protected PackageViewCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        protected override void Bind()
        {
            var bindingSet = this.CreateBindingSet<PackageViewCell, CartPackItemViewModel>();

            bindingSet.Bind(TitleLabel).For(v => v.Text).To(vm => vm.Title);
            bindingSet.Bind(PriceLabel).For(v => v.Text).To(vm => vm.Price);
            bindingSet.Bind(CountStepperView).For(v => v.ViewModel).To(vm => vm.StepperViewModel);

            bindingSet.Apply();
        }
    }
}

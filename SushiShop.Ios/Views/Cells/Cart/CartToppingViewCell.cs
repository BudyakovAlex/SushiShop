using System;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Views.Cells;
using Foundation;
using MvvmCross.Binding.BindingContext;
using SushiShop.Core.ViewModels.Cart.Items;
using UIKit;

namespace SushiShop.Ios.Views.Cells.Cart
{
    public partial class CartToppingViewCell : BaseTableViewCell
    {
        public static readonly NSString Key = new NSString("CartToppingViewCell");
        public static readonly UINib Nib;

        static CartToppingViewCell()
        {
            Nib = UINib.FromName("CartToppingViewCell", NSBundle.MainBundle);
        }

        protected CartToppingViewCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        protected override void Bind()
        {
            var bindignSet = this.CreateBindingSet<CartToppingViewCell, CartToppingItemViewModel>();

            bindignSet.Bind(TitleLabel).For(v => v.Text).To(vm => vm.Title);
            bindignSet.Bind(PriceLabel).For(v => v.Text).To(vm => vm.Price);
            bindignSet.Bind(CountStepperView).For(v => v.ViewModel).To(vm => vm.StepperViewModel);

            bindignSet.Apply();
        }
    }
}

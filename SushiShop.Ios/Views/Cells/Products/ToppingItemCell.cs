using System;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Views.Cells;
using Foundation;
using MvvmCross.Binding.BindingContext;
using SushiShop.Core.ViewModels.Products.Items;
using UIKit;

namespace SushiShop.Ios.Views.Cells.Products
{
    public partial class ToppingItemCell : BaseTableViewCell
    {
        public static readonly NSString Key = new NSString("ToppingItemCell");
        public static readonly UINib Nib;

        static ToppingItemCell()
        {
            Nib = UINib.FromName("ToppingItemCell", NSBundle.MainBundle);
        }

        protected ToppingItemCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void Bind()
        {
            base.Bind();

            var bindingSet = this.CreateBindingSet<ToppingItemCell, ToppingItemViewModel>();

            bindingSet.Bind(TitleLabel).For(v => v.Text).To(vm => vm.Title);
            bindingSet.Bind(PriceLabel).For(v => v.Text).To(vm => vm.Price);
            bindingSet.Bind(StepperView).For(v => v.ViewModel).To(vm => vm.StepperViewModel);

            bindingSet.Apply();
        }
    }
}

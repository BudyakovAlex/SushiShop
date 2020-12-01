using System;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Views.Cells;
using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Binding;
using SushiShop.Core.Converters;
using SushiShop.Core.ViewModels.Cart.Items;
using SushiShop.Ios.Converters;
using UIKit;

namespace SushiShop.Ios.Views.Cells.Cart
{
    public partial class CartProductViewCell : BaseTableViewCell
    {
        public static readonly NSString Key = new NSString("CartProductViewCell");
        public static readonly UINib Nib;

        static CartProductViewCell()
        {
            Nib = UINib.FromName("CartProductViewCell", NSBundle.MainBundle);
        }

        protected CartProductViewCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        protected override void Initialize()
        {
            base.Initialize();

            ProductImage.Layer.CornerRadius = 6f;
            ProductImage.Layer.MasksToBounds = true;

            ToppingsStackView.RegisterView<CartProductToppingViewCell, CartProductToppingItemViewModel>();
        }

        protected override void Bind()
        {
            var bindingSet = this.CreateBindingSet<CartProductViewCell, CartProductItemViewModel>();

            bindingSet.Bind(ProductImage).For(v => v.ImagePath).To(vm => vm.ImageUrl);
            bindingSet.Bind(TitleLabel).For(v => v.Text).To(vm => vm.Title);
            bindingSet.Bind(OldPriceLabel).For(v => v.AttributedText).To(vm => vm.OldPrice)
                .WithConversion<StringToStrikethroughAttributedTextConverter>();
            bindingSet.Bind(PriceLabel).For(v => v.Text).To(vm => vm.Price);
            bindingSet.Bind(WeightLabel).For(v => v.Text).To(vm => vm.Value);
            bindingSet.Bind(CountStepperView).For(v => v.ViewModel).To(vm => vm.StepperViewModel);
            bindingSet.Bind(this).For(v => v.BindTap()).To(vm => vm.ShowDetailsCommand);
            bindingSet.Bind(ToppingsStackView).For(v => v.ItemsSource).To(vm => vm.Toppings);
            bindingSet.Bind(ToppingsStackView).For(v => v.BindVisibility()).To(vm => vm.Toppings.Count)
                .WithConversion<AmountToBoolVisibilityConverter>();

            bindingSet.Apply();
        }
    }
}

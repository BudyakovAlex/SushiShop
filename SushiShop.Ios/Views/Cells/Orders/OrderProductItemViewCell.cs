using System;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Views.Cells;
using Foundation;
using MvvmCross.Binding.BindingContext;
using SushiShop.Core.ViewModels.Orders.Items;
using SushiShop.Ios.Converters;
using UIKit;

namespace SushiShop.Ios.Views.Cells.Orders
{
    public partial class OrderProductItemViewCell : BaseTableViewCell
    {
        public const float Height = 90f;

        public static readonly NSString Key = new NSString(nameof(OrderProductItemViewCell));
        public static readonly UINib Nib = UINib.FromName(Key, NSBundle.MainBundle);

        protected OrderProductItemViewCell(IntPtr handle)
            : base(handle)
        {
        }

        protected override void Bind()
        {
            base.Bind();

            var bindingSet = this.CreateBindingSet<OrderProductItemViewCell, OrderProductItemViewModel>();

            bindingSet.Bind(ProductImageView).For(v => v.ImageUrl).To(vm => vm.ImageUrl);
            bindingSet.Bind(TitleLabel).For(v => v.Text).To(vm => vm.Title);
            bindingSet.Bind(OldPriceLabel).For(v => v.AttributedText).To(vm => vm.OldPrice)
                .WithConversion<StringToStrikethroughAttributedTextConverter>();
            bindingSet.Bind(PriceLabel).For(v => v.Text).To(vm => vm.Title);
            bindingSet.Bind(ValueLabel).For(v => v.Text).To(vm => vm.Value);
            bindingSet.Bind(CountLabel).For(v => v.Text).To(vm => vm.Count);

            bindingSet.Apply();
        }
    }
}

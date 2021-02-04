using System;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Views.Cells;
using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Binding;
using SushiShop.Core.ViewModels.Orders.Items;
using UIKit;

namespace SushiShop.Ios.Views.Cells.Orders
{
    public partial class OrderDeliverySuggestionItemViewCell : BaseTableViewCell
    {
        public static readonly NSString Key = new NSString("OrderDeliverySuggestionItemViewCell");
        public static readonly UINib Nib;

        static OrderDeliverySuggestionItemViewCell()
        {
            Nib = UINib.FromName("OrderDeliverySuggestionItemViewCell", NSBundle.MainBundle);
        }

        protected OrderDeliverySuggestionItemViewCell(IntPtr handle) : base(handle)
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

            var bindingSet = this.CreateBindingSet<OrderDeliverySuggestionItemViewCell, OrderDeliverySuggestionItemViewModel>();

            bindingSet.Bind(this).For(v => v.BindTap()).To(vm => vm.SelectCommand).CommandParameter(DataContext);
            bindingSet.Bind(TitleLabel).For(v => v.Text).To(vm => vm.Address);

            bindingSet.Apply();
        }
    }
}

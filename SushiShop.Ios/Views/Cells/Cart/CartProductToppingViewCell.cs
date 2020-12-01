using System;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Views.Controls;
using Foundation;
using MvvmCross.Binding.BindingContext;
using SushiShop.Core.ViewModels.Cart.Items;
using SushiShop.Ios.Common;
using UIKit;

namespace SushiShop.Ios.Views.Cells.Cart
{
    [Register(nameof(CartProductToppingViewCell))]
    public partial class CartProductToppingViewCell : MvxGenericView<CartProductToppingItemViewModel>
    {
        private UILabel titleLabel;
        private UILabel countLabel;

        protected CartProductToppingViewCell(IntPtr handle) : base(handle)
        {
        }

        protected override void Initialize()
        {
            base.Initialize();

            titleLabel = new UILabel
            {
                TranslatesAutoresizingMaskIntoConstraints = false,
                Font = Font.Create(Core.Data.Enums.FontStyle.Regular, 14),
                Lines = 0
            };

            countLabel = new UILabel
            {
                TranslatesAutoresizingMaskIntoConstraints = false,
                Font = Font.Create(Core.Data.Enums.FontStyle.Regular, 14)
            };

            AddSubviews(
                titleLabel,
                countLabel);

            NSLayoutConstraint.ActivateConstraints(new[]
            {
                titleLabel.TopAnchor.ConstraintEqualTo(TopAnchor),
                titleLabel.BottomAnchor.ConstraintEqualTo(BottomAnchor),
                titleLabel.LeadingAnchor.ConstraintEqualTo(LeadingAnchor),
                titleLabel.TrailingAnchor.ConstraintGreaterThanOrEqualTo(countLabel.TrailingAnchor, 10),
                countLabel.TrailingAnchor.ConstraintEqualTo(TrailingAnchor),
                countLabel.CenterYAnchor.ConstraintEqualTo(CenterYAnchor),
            });
        }

        protected override void Bind()
        {
            var bindingSet = this.CreateBindingSet<CartProductToppingViewCell, CartProductToppingItemViewModel>();

            bindingSet.Bind(titleLabel).For(v => v.Text).To(vm => vm.Title);
            bindingSet.Bind(countLabel).For(v => v.Text).To(vm => vm.Count);

            bindingSet.Apply();
        }
    }
}

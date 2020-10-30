using System;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Views.Cells;
using Foundation;
using MvvmCross.Binding.BindingContext;
using SushiShop.Core.ViewModels.Menu.Items;
using SushiShop.Ios.Common;
using SushiShop.Ios.Common.Styles;
using UIKit;

namespace SushiShop.Ios.Views.Cells.Menu
{
    public partial class ProductItemViewCell : BaseCollectionViewCell
    {
        public static readonly NSString Key = new NSString(nameof(ProductItemViewCell));
        public static readonly UINib Nib = UINib.FromName(Key, NSBundle.MainBundle);

        protected ProductItemViewCell(IntPtr handle)
            : base(handle)
        {
        }

        private string oldPrice;
        public string OldPrice
        {
            get => oldPrice;
            set
            {
                oldPrice = value;
                if (oldPrice is null)
                {
                    OldPriceLabel.Hidden = true;
                }
                else
                {
                    OldPriceLabel.Hidden = false;
                    OldPriceLabel.AttributedText = new NSMutableAttributedString(oldPrice, strikethroughStyle: NSUnderlineStyle.Single);
                }
            }
        }

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();

            if (Layer.ShadowPath?.BoundingBox != Bounds)
            {
                Layer.ShadowPath = UIBezierPath.FromRoundedRect(Bounds, Constants.UI.CornerRadius).CGPath;
            }
        }

        protected override void Initialize()
        {
            base.Initialize();

            this.ApplyShadow();

            ContentView.ClipsToBounds = true;
            ContentView.BackgroundColor = Colors.White;
            ContentView.Layer.CornerRadius = Constants.UI.CornerRadius;
        }

        protected override void Bind()
        {
            base.Bind();

            var bindingSet = this.CreateBindingSet<ProductItemViewCell, ProductItemViewModel>();

            //bindingSet.Bind(this).For(v => v.BindTap()).To(vm => vm.ShowDetailsCommand);
            bindingSet.Bind(this).For(v => v.OldPrice).To(vm => vm.OldPrice);
            bindingSet.Bind(TopImageView).For(v => v.ImagePath).To(vm => vm.ImageUrl);
            bindingSet.Bind(TitleLabel).For(v => v.Text).To(vm => vm.Title);
            bindingSet.Bind(PriceLabel).For(v => v.Text).To(vm => vm.Price);
            bindingSet.Bind(StepperView).For(v => v.ViewModel).To(vm => vm.StepperViewModel);

            bindingSet.Apply();
        }
    }
}

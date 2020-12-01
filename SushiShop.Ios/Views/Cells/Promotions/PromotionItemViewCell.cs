using System;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Views.Cells;
using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Binding;
using SushiShop.Core.ViewModels.Promotions.Items;
using SushiShop.Ios.Common;
using SushiShop.Ios.Common.Styles;
using UIKit;

namespace SushiShop.Ios.Views.Cells.Promotions
{
    public partial class PromotionItemViewCell : BaseCollectionViewCell
    {
        public static readonly NSString Key = new NSString(nameof(PromotionItemViewCell));
        public static readonly UINib Nib = UINib.FromName(Key, NSBundle.MainBundle);

        protected PromotionItemViewCell(IntPtr handle)
            : base(handle)
        {
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

            ImageView.SetPlaceholders();
        }

        protected override void Bind()
        {
            base.Bind();

            var bindingSet = this.CreateBindingSet<PromotionItemViewCell, PromotionItemViewModel>();

            bindingSet.Bind(this).For(v => v.BindTap()).To(vm => vm.ShowDetailsCommand);
            bindingSet.Bind(ImageView).For(v => v.ImageUrl).To(vm => vm.ImageUrl);
            bindingSet.Apply();
        }
    }
}

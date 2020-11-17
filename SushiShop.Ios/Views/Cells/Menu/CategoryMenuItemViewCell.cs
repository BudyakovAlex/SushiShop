using System;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Views.Cells;
using CoreAnimation;
using CoreGraphics;
using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Binding;
using SushiShop.Core.ViewModels.Menu.Items;
using SushiShop.Ios.Common;
using SushiShop.Ios.Common.Styles;
using UIKit;

namespace SushiShop.Ios.Views.Cells.Menu
{
    public partial class CategoryMenuItemViewCell : BaseCollectionViewCell
    {
        public static readonly NSString Key = new NSString(nameof(CategoryMenuItemViewCell));
        public static readonly UINib Nib = UINib.FromName(Key, NSBundle.MainBundle);

        private CAGradientLayer overlayLayer;

        protected CategoryMenuItemViewCell(IntPtr handle)
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

            if (overlayLayer.Frame != Bounds)
            {
                CATransaction.DisableActions = true;
                overlayLayer.Frame = Bounds;
                CATransaction.DisableActions = false;
            }
        }

        protected override void Initialize()
        {
            base.Initialize();

            this.ApplyShadow();

            ContentView.ClipsToBounds = true;
            ContentView.BackgroundColor = Colors.White;
            ContentView.Layer.CornerRadius = Constants.UI.CornerRadius;

            overlayLayer = CreateOverlayLayer();
            ContentView.Layer.InsertSublayerBelow(overlayLayer, Label.Layer);

            ImageView.SetPlaceholders();
        }

        protected override void Bind()
        {
            base.Bind();

            var bindingSet = this.CreateBindingSet<CategoryMenuItemViewCell, CategoryMenuItemViewModel>();

            bindingSet.Bind(this).For(v => v.BindTap()).To(vm => vm.ShowDetailsCommand);
            bindingSet.Bind(ImageView).For(v => v.ImagePath).To(vm => vm.ImageUrl);
            bindingSet.Bind(Label).For(v => v.Text).To(vm => vm.Title);

            bindingSet.Apply();
        }

        private CAGradientLayer CreateOverlayLayer() =>
            new CAGradientLayer
            {
                Colors = new[]
                {
                    Colors.RealBlack.ColorWithAlpha(0f).CGColor,
                    Colors.RealBlack.ColorWithAlpha(0f).CGColor,
                    Colors.RealBlack.ColorWithAlpha(0.31f).CGColor,
                    Colors.RealBlack.ColorWithAlpha(0.56f).CGColor,
                },
                StartPoint = new CGPoint(1f, 0f),
                EndPoint = new CGPoint(1f, 1f)
            };
    }
}

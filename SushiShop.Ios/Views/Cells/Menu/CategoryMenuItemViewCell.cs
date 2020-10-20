using System;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Views.Cells;
using CoreGraphics;
using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Binding;
using SushiShop.Core.ViewModels.Menu.Items;
using SushiShop.Ios.Common;
using UIKit;

namespace SushiShop.Ios.Views.Cells.Menu
{
    public partial class CategoryMenuItemViewCell : BaseCollectionViewCell
    {
        private const float CornerRadius = 6f;

        public static readonly NSString Key = new NSString(nameof(CategoryMenuItemViewCell));
        public static readonly UINib Nib = UINib.FromName(Key, NSBundle.MainBundle);

        protected CategoryMenuItemViewCell(IntPtr handle)
            : base(handle)
        {
        }

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();
            Layer.ShadowPath = UIBezierPath.FromRoundedRect(Bounds, CornerRadius).CGPath;
        }

        protected override void Initialize()
        {
            base.Initialize();

            Layer.MasksToBounds = false;
            Layer.ShouldRasterize = true;
            Layer.RasterizationScale = UIScreen.MainScreen.Scale;
            Layer.ShadowColor = Colors.RealBlack.CGColor;
            Layer.ShadowOffset = new CGSize(0f, 2f);
            Layer.ShadowOpacity = 0.2f;
            Layer.ShadowRadius = 18f;

            ContentView.ClipsToBounds = true;
            ContentView.Layer.CornerRadius = CornerRadius;
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
    }
}

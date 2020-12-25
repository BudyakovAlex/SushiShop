using System;
using System.Drawing;
using System.Linq;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Views.Cells;
using FFImageLoading.Cross;
using FFImageLoading.Transformations;
using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Binding;
using SushiShop.Core.Data.Models.Stickers;
using SushiShop.Core.Extensions;
using SushiShop.Core.ViewModels.Menu.Items;
using SushiShop.Ios.Common;
using SushiShop.Ios.Common.Styles;
using SushiShop.Ios.Converters;
using UIKit;
using Xamarin.Essentials;

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

        private StickerParams[] stickers;
        public StickerParams[] Stickers
        {
            get => stickers;
            set
            {
                stickers = value;
                StickerStackView.ArrangedSubviews.ForEach(view => view.RemoveFromSuperview());

                stickers?
                    .Select(CreateStickerView)
                    .ForEach(StickerStackView.AddArrangedSubview);
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

            TopImageView.SetPlaceholders();
        }

        protected override void Bind()
        {
            base.Bind();

            var bindingSet = this.CreateBindingSet<ProductItemViewCell, ProductItemViewModel>();

            bindingSet.Bind(this).For(v => v.BindTap()).To(vm => vm.ShowDetailsCommand);
            bindingSet.Bind(this).For(v => v.Stickers).To(vm => vm.Stickers);
            bindingSet.Bind(OldPriceLabel).For(v => v.AttributedText).To(vm => vm.OldPrice)
                .WithConversion<StringToStrikethroughAttributedTextConverter>();
            bindingSet.Bind(TopImageView).For(v => v.ImageUrl).To(vm => vm.ImageUrl);
            bindingSet.Bind(TitleLabel).For(v => v.Text).To(vm => vm.Title);
            bindingSet.Bind(PriceLabel).For(v => v.Text).To(vm => vm.Price);
            bindingSet.Bind(StepperView).For(v => v.ViewModel).To(vm => vm.StepperViewModel);

            bindingSet.Apply();
        }

        private StickerView CreateStickerView(StickerParams sticker)
        {
            var stickerView = new StickerView(sticker.ImageUrl, sticker.BackgroundColor);
            stickerView.TranslatesAutoresizingMaskIntoConstraints = false;

            NSLayoutConstraint.ActivateConstraints(new[]
            {
                stickerView.WidthAnchor.ConstraintEqualTo(StickerView.Width),
                stickerView.HeightAnchor.ConstraintEqualTo(StickerView.Height)
            });

            return stickerView;
        }

        private class StickerView : UIView
        {
            public const float Width = 30f;
            public const float Height = 30f;

            private const float ImageWidth = 16f;
            private const float ImageHeight = 16f;

            private MvxCachedImageView imageview;

            public StickerView(string imageUrl, Color backgroundColor)
            {
                BackgroundColor = backgroundColor.ToPlatformColor();
                Layer.CornerRadius = Height / 2f;

                InitializeImageView(imageUrl);
            }

            private void InitializeImageView(string imageUrl)
            {
                imageview = new MvxCachedImageView();
                imageview.TranslatesAutoresizingMaskIntoConstraints = false;
                imageview.TintColor = Colors.White;
                imageview.Transformations.Add(new TintTransformation(255, 255, 255, 255) { EnableSolidColor = true });
                imageview.ContentMode = UIViewContentMode.ScaleAspectFit;
                imageview.ImagePath = imageUrl;

                AddSubview(imageview);

                NSLayoutConstraint.ActivateConstraints(new[]
                {
                    imageview.CenterXAnchor.ConstraintEqualTo(CenterXAnchor),
                    imageview.CenterYAnchor.ConstraintEqualTo(CenterYAnchor),
                    imageview.WidthAnchor.ConstraintEqualTo(ImageWidth),
                    imageview.HeightAnchor.ConstraintEqualTo(ImageHeight)
                });
            }
        }
    }
}

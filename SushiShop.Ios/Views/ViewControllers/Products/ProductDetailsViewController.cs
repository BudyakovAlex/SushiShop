using BuildApps.Core.Mobile.MvvmCross.UIKit.Views.ViewControllers;
using MvvmCross.Platforms.Ios.Binding;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using SushiShop.Core.ViewModels.Products;
using SushiShop.Ios.Common.Styles;
using SushiShop.Ios.Converters;
using UIKit;

namespace SushiShop.Ios.Views.ViewControllers.Products
{
    [MvxChildPresentation(Animated = true)]
    public partial class ProductDetailsViewController : BaseViewController<ProductDetailsViewModel>
    {
        protected override void InitStylesAndContent()
        {
            base.InitStylesAndContent();

            BackButton.SetCornerRadius();

            ProductImageView.ImagePath = "https://img.championat.com/news/big/w/q/pochemu-sushi-vredny-dlja-figury_1590677088981164064.jpg";
            ProductImageView.ContentMode = UIViewContentMode.ScaleAspectFill;

            AddToCartButton.SetGradientBackground().SetCornerRadius(); ;
            ProductSpecificationsView.SetCornerRadius(4);

            InitCollectionView();
        }

        protected override void Bind()
        {
            base.Bind();

            var bindingSet = CreateBindingSet();

            bindingSet.Bind(BackButton).For(v => v.BindTap()).To(vm => vm.CloseCommand);
            bindingSet.Bind(OldPriceLabel).For(v => v.AttributedText).To(vm => vm.OldPrice)
                .WithConversion<StringToStrikethroughAttributedTextConverter>();

            bindingSet.Apply();
        }

        private void InitCollectionView()
        {
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            NavigationController?.SetNavigationBarHidden(true, animated);
        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);

            NavigationController?.SetNavigationBarHidden(false, animated);
        }
    }
}


using BuildApps.Core.Mobile.MvvmCross.UIKit.Views.ViewControllers;
using MvvmCross.Platforms.Ios.Binding;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using SushiShop.Core.ViewModels.Menu.Items;
using SushiShop.Core.ViewModels.Products;
using SushiShop.Ios.Common.Styles;
using SushiShop.Ios.Converters;
using SushiShop.Ios.Delegates;
using SushiShop.Ios.Sources;
using SushiShop.Ios.Views.Cells.Menu;
using UIKit;

namespace SushiShop.Ios.Views.ViewControllers.Products
{
    [MvxChildPresentation(Animated = true)]
    public partial class ProductDetailsViewController : BaseViewController<ProductDetailsViewModel>
    {
        private CollectionViewSource source;

        protected override void InitStylesAndContent()
        {
            base.InitStylesAndContent();

            BackButton.SetCornerRadius();

            ProductImageView.ImagePath = "https://img.championat.com/news/big/w/q/pochemu-sushi-vredny-dlja-figury_1590677088981164064.jpg";
            ProductImageView.ContentMode = UIViewContentMode.ScaleAspectFill;

            AddToCartButton.SetGradientBackground();
            AddToCartButton.SetCornerRadius();
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
            bindingSet.Bind(StepperView).For(v => v.ViewModel).To(vm => vm.StepperViewModel);
            bindingSet.Bind(source).For(v => v.ItemsSource).To(vm => vm.Items);

            bindingSet.Apply();
        }

        private void InitCollectionView()
        {
            source = new CollectionViewSource(BuyAnotherCollectionView)
                .Register<ProductItemViewModel>(ProductItemViewCell.Nib, ProductItemViewCell.Key);

            BuyAnotherCollectionView.Source = source;
            BuyAnotherCollectionView.Delegate = new RelatedProductsCollectionViewDelegateFlow();
            BuyAnotherCollectionView.CollectionViewLayout = new UICollectionViewFlowLayout();
            (BuyAnotherCollectionView.CollectionViewLayout as UICollectionViewFlowLayout).ScrollDirection = UICollectionViewScrollDirection.Horizontal;
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


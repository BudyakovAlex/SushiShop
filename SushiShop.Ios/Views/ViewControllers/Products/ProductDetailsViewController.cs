using BuildApps.Core.Mobile.MvvmCross.UIKit.Views.ViewControllers;
using MvvmCross.Platforms.Ios.Binding;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using SushiShop.Core.ViewModels.Products;
using SushiShop.Ios.Common;
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

            BackButton.SetBackButtonStyle();

            ProductImageView.ImagePath = "https://img.championat.com/news/big/w/q/pochemu-sushi-vredny-dlja-figury_1590677088981164064.jpg";
            ProductImageView.ContentMode = UIViewContentMode.ScaleAspectFill;

            ProductNameLabel.SetBoldStyle("Сливочная креветка", 20);
            ProductDescriptionLabel.SetRegularStyle("Сливочный сыр, снежный краб,икра масаго, гоурец креветка");
            WeightLabel.SetRegularStyle("250г.");
            PriceLabel.SetPriceStyle("237₽");
            OldPriceLabel.SetOldPriceStyle();
            BuyAnotherTitleLabel.SetRegularStyle("С этим товаром покупают".ToUpperInvariant(), 10);
            AddToCartButton.SetGradientStyle("В корзину");

            ProteinsTitleLabel.SetBoldStyle("Белки", 12);
            ProteinsTitleLabel.TextColor = Colors.White;
            ProteinsValueLabel.SetBoldStyle("17", 20);
            ProteinsValueLabel.TextColor = Colors.White;
            FatsTitleLabel.SetBoldStyle("Жиры", 12);
            FatsTitleLabel.TextColor = Colors.White;
            FatsValueLabel.SetBoldStyle("36", 20);
            FatsValueLabel.TextColor = Colors.White;
            CarbohydratesTitleLabel.SetBoldStyle("Углеводы", 12);
            CarbohydratesTitleLabel.TextColor = Colors.White;
            CarbohydratesValueLabel.SetBoldStyle("77", 20);
            CarbohydratesValueLabel.TextColor = Colors.White;
            CaloriesTitleLabel.SetBoldStyle("Ккал", 12);
            CaloriesTitleLabel.TextColor = Colors.White;
            CaloriesValueLabel.SetBoldStyle("185", 20);
            CaloriesValueLabel.TextColor = Colors.White;

            ProductSpecificationsView.BackgroundColor = Colors.TransparentBlack;
            ProductSpecificationsView.Layer.CornerRadius = 4;
            ProductSpecificationsView.Layer.MasksToBounds = true;
            FirstSplitView.BackgroundColor = SecondSplitView.BackgroundColor = ThirdSplitView.BackgroundColor = Colors.TransparentWhite;

            View.BackgroundColor = Colors.Background;

            InitCollectionView();
        }

        protected override void InitNavigationController(UINavigationController navigationController)
        {
            base.InitNavigationController(navigationController);

            navigationController.SetNavigationBarHidden(true, true);
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

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);

            NavigationController?.SetNavigationBarHidden(false, animated);
        }
    }
}


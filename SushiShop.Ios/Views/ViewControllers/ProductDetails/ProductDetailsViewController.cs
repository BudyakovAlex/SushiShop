using BuildApps.Core.Mobile.MvvmCross.UIKit.Views.ViewControllers;
using MvvmCross.Binding.Combiners;
using MvvmCross.Platforms.Ios.Binding;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using SushiShop.Core.ViewModels.Menu.Items;
using SushiShop.Core.ViewModels.ProductDetails;
using SushiShop.Ios.Common.Styles;
using SushiShop.Ios.Converters;
using SushiShop.Ios.Delegates;
using SushiShop.Ios.Sources;
using SushiShop.Ios.Views.Cells.Menu;
using UIKit;

namespace SushiShop.Ios.Views.ViewControllers.ProductDetails
{
    [MvxChildPresentation(Animated = true)]
    public partial class ProductDetailsViewController : BaseViewController<ProductDetailsViewModel>
    {
        private CollectionViewSource source;

        protected override void InitStylesAndContent()
        {
            base.InitStylesAndContent();

            BackButton.SetCornerRadius();

            ProductImageView.SetPlaceholders();

            ProductSpecificationsView.SetCornerRadius(4);

            InitCollectionView();
        }

        protected override void Bind()
        {
            base.Bind();

            var bindingSet = CreateBindingSet();

            bindingSet.Bind(ProductImageView).For(v => v.ImageUrl).To(vm => vm.BackgroungImageUrl);
            bindingSet.Bind(PriceLabel).For(v => v.Text).To(vm => vm.Price);
            bindingSet.Bind(OldPriceLabel).For(v => v.Text).To(vm => vm.OldPrice);
            bindingSet.Bind(ProductNameLabel).For(v => v.Text).To(vm => vm.Title);
            bindingSet.Bind(ProductDescriptionLabel).For(v => v.Text).To(vm => vm.Description);
            bindingSet.Bind(WeightLabel).For(v => v.Text).To(vm => vm.Weight);
            bindingSet.Bind(FatsValueLabel).For(v => v.Text).To(vm => vm.Fats);
            bindingSet.Bind(ProteinsValueLabel).For(v => v.Text).To(vm => vm.Protein);
            bindingSet.Bind(CarbohydratesValueLabel).For(v => v.Text).To(vm => vm.Carbohydrates);
            bindingSet.Bind(CaloriesValueLabel).For(v => v.Text).To(vm => vm.Calories);
            bindingSet.Bind(AddToCartButton).For(v => v.BindTouchUpInside()).To(vm => vm.AddToCartCommand);
            bindingSet.Bind(AddToCartButton).For(v => v.Hidden).To(vm => vm.IsReadOnly);
            bindingSet.Bind(BackButton).For(v => v.BindTouchUpInside()).To(vm => vm.CloseCommand);
            bindingSet.Bind(OldPriceLabel).For(v => v.AttributedText).To(vm => vm.OldPrice)
                .WithConversion<StringToStrikethroughAttributedTextConverter>();
            bindingSet.Bind(StepperView).For(v => v.ViewModel).To(vm => vm.StepperViewModel);
            bindingSet.Bind(StepperView).For(v => v.Hidden).ByCombining(new MvxOrValueCombiner(),
                                                                        vm => vm.IsHiddenStepper,
                                                                        vm => vm.IsReadOnly);
            bindingSet.Bind(source).For(v => v.ItemsSource).To(vm => vm.RelatedItems);
            bindingSet.Bind(LoadingView).For(v => v.BindVisible()).To(vm => vm.IsBusy);

            bindingSet.Apply();
        }

        private void InitCollectionView()
        {
            source = new CollectionViewSource(BuyAnotherCollectionView)
                .Register<ProductItemViewModel>(ProductItemViewCell.Nib, ProductItemViewCell.Key);

            BuyAnotherCollectionView.Source = source;
            BuyAnotherCollectionView.Delegate = new RelatedProductsCollectionViewDelegateFlow();
            BuyAnotherCollectionView.CollectionViewLayout = new UICollectionViewFlowLayout { ScrollDirection = UICollectionViewScrollDirection.Horizontal };
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


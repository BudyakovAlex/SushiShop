using BuildApps.Core.Mobile.MvvmCross.UIKit.Views.ViewControllers;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Binding;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using SushiShop.Core.ViewModels.Promotions;
using SushiShop.Core.ViewModels.Promotions.Items;
using SushiShop.Ios.Common;
using SushiShop.Ios.Delegates;
using SushiShop.Ios.Sources;
using SushiShop.Ios.Views.Cells.Promotions;
using UIKit;

namespace SushiShop.Ios.Views.ViewControllers.Promotions
{
    [MvxTabPresentation(WrapInNavigationController = true)]
    public partial class PromotionsViewController : BaseViewController<PromotionsViewModel>
    {
        private CollectionViewSource source;

        protected override void InitStylesAndContent()
        {
            base.InitStylesAndContent();

            InitializeCollectionView();
        }

        protected override void InitNavigationItem(UINavigationItem navigationItem)
        {
            base.InitNavigationItem(navigationItem);

            var image = UIImage.FromBundle(ImageNames.ImageBackWhite);
            navigationItem.BackBarButtonItem = new UIBarButtonItem(image, UIBarButtonItemStyle.Plain, null);
        }

        protected override void Bind()
        {
            base.Bind();

            var bindingSet = this.CreateBindingSet<PromotionsViewController, PromotionsViewModel>();

            bindingSet.Bind(this).For(v => v.Title).To(vm => vm.Title);
            bindingSet.Bind(source).For(v => v.ItemsSource).To(vm => vm.Items);
            bindingSet.Bind(LoadingView).For(v => v.BindVisible()).To(vm => vm.IsBusy);

            bindingSet.Apply();
        }

        private void InitializeCollectionView()
        {
            source = new CollectionViewSource(CollectionView)
                .Register<PromotionItemViewModel>(PromotionItemViewCell.Nib, PromotionItemViewCell.Key);

            CollectionView.Source = source;
            CollectionView.Delegate = new PromotionsCollectionViewDelegateFlowLayout();
        }
    }
}

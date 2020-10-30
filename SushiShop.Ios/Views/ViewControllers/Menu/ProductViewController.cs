using BuildApps.Core.Mobile.MvvmCross.UIKit.Views.ViewControllers;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Binding;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using SushiShop.Core.ViewModels.Menu;
using SushiShop.Core.ViewModels.Menu.Items;
using SushiShop.Ios.Delegates;
using SushiShop.Ios.Sources;
using SushiShop.Ios.Views.Cells.Menu;

namespace SushiShop.Ios.Views.ViewControllers.Menu
{
    [MvxChildPresentation]
    public partial class ProductViewController : BaseViewController<ProductViewModel>
    {
        private CollectionViewSource source;

        protected override void InitStylesAndContent()
        {
            base.InitStylesAndContent();
            InitializeCollectionView();
        }

        protected override void Bind()
        {
            base.Bind();

            var bindingSet = this.CreateBindingSet<ProductViewController, ProductViewModel>();

            bindingSet.Bind(this).For(v => v.Title).To(vm => vm.Title);
            bindingSet.Bind(source).For(v => v.ItemsSource).To(vm => vm.Items);
            bindingSet.Bind(LoadingView).For(v => v.BindVisible()).To(vm => vm.IsLoading);

            bindingSet.Apply();
        }

        private void InitializeCollectionView()
        {
            source = new CollectionViewSource(CollectionView)
                .Register<ProductItemViewModel>(ProductItemViewCell.Nib, ProductItemViewCell.Key);

            CollectionView.Source = source;
            CollectionView.Delegate = new ProductCollectionViewDelegateFlowLayout();
        }
    }
}

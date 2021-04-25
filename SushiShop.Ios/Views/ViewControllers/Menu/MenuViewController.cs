using BuildApps.Core.Mobile.MvvmCross.UIKit.Views.ViewControllers;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Binding;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using MvvmCross.Platforms.Ios.Views;
using SushiShop.Core.Converters;
using SushiShop.Core.Data.Enums;
using SushiShop.Core.ViewModels.Menu;
using SushiShop.Core.ViewModels.Menu.Items;
using SushiShop.Ios.Common;
using SushiShop.Ios.Delegates;
using SushiShop.Ios.Extensions;
using SushiShop.Ios.Sources;
using SushiShop.Ios.Views.Cells.Menu;
using UIKit;

namespace SushiShop.Ios.Views.ViewControllers.Menu
{
    [MvxTabPresentation(WrapInNavigationController = true)]
    public partial class MenuViewController : BaseViewController<MenuViewModel>
    {
        private UIButton switchPresentationButton;
        private UILabel titleLabel;
        private UIView leftBarButtonItemView;
        private MenuCollectionViewSource collectionViewSource;
        private CollectionViewSource simpleListCollectionViewSource;

        private MvxUIRefreshControl simpleListRefreshControl;
        private MvxUIRefreshControl menuRefreshControl;

        protected override void InitStylesAndContent()
        {
            base.InitStylesAndContent();
            InitializeCollectionView();
            InitializeSimpleListCollectionView();
        }

        protected override void InitNavigationItem(UINavigationItem navigationItem)
        {
            base.InitNavigationItem(navigationItem);

            switchPresentationButton = Components.CreateDefaultBarButton(ImageNames.MenuList, 45, 45);
            switchPresentationButton.ImageEdgeInsets = new UIEdgeInsets(0, 0, 0, -14);
            navigationItem.RightBarButtonItem = new UIBarButtonItem(switchPresentationButton);

            leftBarButtonItemView = CreateLeftBarButtonItem();
            navigationItem.LeftBarButtonItem = new UIBarButtonItem(leftBarButtonItemView);
        }

        protected override void Bind()
        {
            base.Bind();

            var bindingSet = this.CreateBindingSet<MenuViewController, MenuViewModel>();

            bindingSet.Bind(switchPresentationButton).For(v => v.BindImage()).To(vm => vm.IsListMenuPresentation)
                .WithConversion(new BoolToValueConverter<string>(ImageNames.MenuTiles, ImageNames.MenuList));
            bindingSet.Bind(switchPresentationButton).For(v => v.BindTouchUpInside()).To(vm => vm.SwitchPresentationCommand);

            bindingSet.Bind(titleLabel).For(v => v.Text).To(vm => vm.CityName);
            bindingSet.Bind(leftBarButtonItemView).For(v => v.BindTap()).To(vm => vm.SelectCityCommand);
            bindingSet.Bind(CollectionView).For(v => v.BindHidden()).To(vm => vm.IsListMenuPresentation);
            bindingSet.Bind(collectionViewSource).For(v => v.ItemsSource).To(vm => vm.Items);
            bindingSet.Bind(SimpleListCollectionView).For(v => v.BindVisible()).To(vm => vm.IsListMenuPresentation);
            bindingSet.Bind(simpleListCollectionViewSource).For(v => v.ItemsSource).To(vm => vm.SimpleItems);
            bindingSet.Bind(LoadingIndicator).For(v => v.BindVisible()).To(vm => vm.IsBusy);
            bindingSet.Bind(menuRefreshControl).For(v => v.RefreshCommand).To(vm => vm.RefreshDataCommand);
            bindingSet.Bind(simpleListRefreshControl).For(v => v.RefreshCommand).To(vm => vm.RefreshDataCommand);
            bindingSet.Bind(menuRefreshControl).For(v => v.IsRefreshing).To(vm => vm.IsRefreshing);
            bindingSet.Bind(simpleListRefreshControl).For(v => v.IsRefreshing).To(vm => vm.IsRefreshing);

            bindingSet.Apply();
        }

        private UIView CreateLeftBarButtonItem()
        {
            var stackView = new UIStackView()
            {
                Alignment = UIStackViewAlignment.Center,
                Axis = UILayoutConstraintAxis.Horizontal,
                Distribution = UIStackViewDistribution.EqualSpacing,
                Spacing = 10
            };

            titleLabel = new UILabel()
            {
                Font = Font.Create(FontStyle.Medium, 18f),
                TextColor = Colors.FigmaBlack
            };

            stackView.AddArrangedSubview(titleLabel);

            var chevronImage = new UIImageView
            {
                Image = UIImage.FromBundle(ImageNames.ChevronDown)
            };

            stackView.AddArrangedSubview(chevronImage);
            return stackView;
        }

        private void InitializeCollectionView()
        {
            collectionViewSource = new MenuCollectionViewSource(CollectionView);

            CollectionView.Source = collectionViewSource;
            CollectionView.Delegate = new MenuCollectionViewDelegateFlowLayout(collectionViewSource);

            menuRefreshControl = new MvxUIRefreshControl();
            CollectionView.RefreshControl = menuRefreshControl;
        }

        private void InitializeSimpleListCollectionView()
        {
            simpleListCollectionViewSource = new CollectionViewSource(SimpleListCollectionView)
                .Register<MenuActionItemViewModel>(SimpleMenuActionItemCell.Nib, SimpleMenuActionItemCell.Key)
                .Register<CategoryMenuItemViewModel>(SimpleMenuItemCell.Nib, SimpleMenuItemCell.Key)
                .Register<GroupsMenuItemViewModel>(SimpleMenuGroupsCell.Nib, SimpleMenuGroupsCell.Key);

            SimpleListCollectionView.Source = simpleListCollectionViewSource;
            SimpleListCollectionView.Delegate = new SimpleListMenuCollectionDelegateFlowLayout(simpleListCollectionViewSource);

            simpleListRefreshControl = new MvxUIRefreshControl();
            SimpleListCollectionView.RefreshControl = simpleListRefreshControl;
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            NavigationController?.SetNavigationBarHidden(false, animated);
        }
    }
}

using BuildApps.Core.Mobile.MvvmCross.UIKit.Views.ViewControllers;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Binding;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using SushiShop.Core.Converters;
using SushiShop.Core.Data.Enums;
using SushiShop.Core.ViewModels.Menu;
using SushiShop.Ios.Common;
using SushiShop.Ios.Extensions;
using SushiShop.Ios.Sources;
using SushiShop.Ios.Views.Cells.Menu;
using UIKit;

namespace SushiShop.Ios.Views.ViewControllers.Menu
{
    [MvxTabPresentation(WrapInNavigationController = true, TabIconName = ImageNames.MenuTabIcon)]
    public partial class MenuViewController : BaseViewController<MenuViewModel>
    {
        private UIButton switchPresentationButton;
        private UILabel titleLabel;
        private UIView leftBarButtonItemView;
        private SimpleListMenuCollectionViewSource simpleListCollectionViewSource;

        protected override void InitStylesAndContent()
        {
            base.InitStylesAndContent();
            InitializeCollectionView();
            InitializeSimpleListCollectionView();
        }

        protected override void InitNavigationItem(UINavigationItem navigationItem)
        {
            base.InitNavigationItem(navigationItem);
            switchPresentationButton = UIHelper.CreateDefaultBarButton(ImageNames.MenuList, ImageNames.MenuList);
            navigationItem.RightBarButtonItem = new UIBarButtonItem(switchPresentationButton);

            leftBarButtonItemView = CreateLeftBarButtonItem();
            navigationItem.LeftBarButtonItem = new UIBarButtonItem(leftBarButtonItemView);
        }

        protected override void DoBind()
        {
            base.DoBind();

            var bindingSet = this.CreateBindingSet<MenuViewController, MenuViewModel>();

            bindingSet.Bind(switchPresentationButton).For(v => v.BindImage()).To(vm => vm.IsListMenuPresentation)
                .WithConversion(new BoolToValueConverter<string>() { TrueValue = ImageNames.MenuTiles, FalseValue = ImageNames.MenuList });
            bindingSet.Bind(switchPresentationButton).For(v => v.BindTap()).To(vm => vm.SwitchPresentationCommand);

            bindingSet.Bind(titleLabel).For(v => v.Text).To(vm => vm.CityName);
            bindingSet.Bind(leftBarButtonItemView).For(v => v.BindTap()).To(vm => vm.SelectCityCommand);
            bindingSet.Bind(CollectionView).For(v => v.BindHidden()).To(vm => vm.IsListMenuPresentation);
            bindingSet.Bind(SimpleListCollectionView).For(v => v.BindVisible()).To(vm => vm.IsListMenuPresentation);
            bindingSet.Bind(simpleListCollectionViewSource).For(v => v.ItemsSource).To(vm => vm.SimpleItems);

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
                Font = Font.Create(FontStyle.Regular, 18f)
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
            //TODO: implement
        }

        private void InitializeSimpleListCollectionView()
        {
            SimpleListCollectionView.RegisterNibForCell(SimpleMenuItemCell.Nib, SimpleMenuItemCell.Key);
            SimpleListCollectionView.RegisterNibForCell(SimpleMenuGroupsCell.Nib, SimpleMenuGroupsCell.Key);
            SimpleListCollectionView.RegisterNibForCell(SimpleMenuActionItemCell.Nib, SimpleMenuActionItemCell.Key);

            simpleListCollectionViewSource = new SimpleListMenuCollectionViewSource(SimpleListCollectionView);
            SimpleListCollectionView.Source = simpleListCollectionViewSource;
            SimpleListCollectionView.Delegate = new SimpleListMenuCollectionDelegateFlowLayout(simpleListCollectionViewSource);
        }
    }
}

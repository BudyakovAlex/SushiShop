using BuildApps.Core.Mobile.MvvmCross.UIKit.Views.ViewControllers;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Binding;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using SushiShop.Core.Resources;
using SushiShop.Core.ViewModels.Cities;
using SushiShop.Ios.Common;
using SushiShop.Ios.Sources;
using UIKit;

namespace SushiShop.Ios.Views.ViewControllers.Cities
{
    [MvxModalPresentation(Animated = true,
     ModalPresentationStyle = UIModalPresentationStyle.OverFullScreen,
     WrapInNavigationController = true)]
    public partial class SelectCityViewController : BaseViewController<SelectCityViewModel>
    {
        private SelectableTableSource source;
        private UIButton backButton;

        public SelectCityViewController() : base("SelectCityViewController", null)
        {
        }

        protected override void InitStylesAndContent()
        {
            base.InitStylesAndContent();
            InitTableView();
        }

        protected override void DoBind()
        {
            base.DoBind();

            var bindingSet = this.CreateBindingSet<SelectCityViewController, SelectCityViewModel>();

            bindingSet.Bind(SearchBar).For(v => v.Placeholder).To(vm => vm.QueryPlaceholder);
            bindingSet.Bind(SearchBar).For(v => v.Text).To(vm => vm.Query);
            bindingSet.Bind(source).For(v => v.ItemsSource).To(vm => vm.Items);
            bindingSet.Bind(source).For(v => v.SelectionChangedCommand).To(vm => vm.SelectItemCommand);
            bindingSet.Bind(backButton).For(v => v.BindTap()).To(vm => vm.PlatformCloseCommand);

            bindingSet.Apply();
        }

        protected override void InitNavigationItem(UINavigationItem navigationItem)
        {
            base.InitNavigationItem(navigationItem);
            backButton = UIHelper.CreateDefaultBarButton(ImageNames.ImageBack, ImageNames.ImageBack);
            navigationItem.LeftBarButtonItem = new UIBarButtonItem(backButton);
            navigationItem.Title = AppStrings.SelectSity;
        }

        protected override void DoViewWillAppear(bool animated)
        {
            base.DoViewWillAppear(animated);
            if (NavigationController is null)
            {
                return;
            }

            NavigationController.NavigationBar.ShadowImage = new UIImage();
        }

        private void InitTableView()
        {
            source = new SelectableTableSource(SearchResultTableView);
            SearchResultTableView.Source = source;
            SearchResultTableView.EstimatedRowHeight = 50f;
            SearchResultTableView.SeparatorStyle = UITableViewCellSeparatorStyle.None;
            SearchResultTableView.RowHeight = UITableView.AutomaticDimension;
        }
    }
}
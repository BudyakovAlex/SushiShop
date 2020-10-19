using MvvmCross.Platforms.Ios.Presenters.Attributes;
using MvvmCross.Platforms.Ios.Views;
using SushiShop.Core.ViewModels;
using SushiShop.Ios.Common;
using SushiShop.Ios.Common.Styles;
using System.Threading.Tasks;
using UIKit;

namespace SushiShop.Ios.Views.ViewControllers
{
    [MvxRootPresentation(WrapInNavigationController = false)]
    public partial class MainViewController : MvxTabBarViewController<MainViewModel>
    {
        private bool isInitialized;

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            if (!isInitialized)
            {
                isInitialized = true;
                Initialize();
            }
        }

        private void Initialize()
        {
            TabBar.ApplyPrimaryStyle();
            InitializeTabBarTopBorder();

            _ = InitializeTabsAsync();
        }

        private async Task InitializeTabsAsync()
        {
            await ViewModel.LoadTabsCommand.ExecuteAsync();

            var tabNames = ViewModel.TabNames;
            for (var i = 0; i < tabNames.Length; i++)
            {
                var tabBarItem = TabBar.Items[i];
                tabBarItem.Title = tabNames[i];
            }
        }

        private void InitializeTabBarTopBorder()
        {
            var view = new UIView();
            view.TranslatesAutoresizingMaskIntoConstraints = false;
            view.BackgroundColor = Colors.Orange2;

            View.AddSubview(view);

            NSLayoutConstraint.ActivateConstraints(new[]
            {
                view.LeadingAnchor.ConstraintEqualTo(TabBar.LeadingAnchor),
                view.TopAnchor.ConstraintEqualTo(TabBar.TopAnchor),
                view.TrailingAnchor.ConstraintEqualTo(TabBar.TrailingAnchor),
                view.HeightAnchor.ConstraintEqualTo(1f)
            });
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildApps.Core.Mobile.Common.Extensions;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Views.ViewControllers;
using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract;
using CoreAnimation;
using CoreGraphics;
using Foundation;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using MvvmCross.Platforms.Ios.Views;
using MvvmCross.ViewModels;
using SushiShop.Core.Data.Enums;
using SushiShop.Core.Extensions;
using SushiShop.Core.ViewModels;
using SushiShop.Ios.Common;
using UIKit;

namespace SushiShop.Ios.Views.ViewControllers
{
    [MvxRootPresentation(WrapInNavigationController = false)]
    public partial class MainViewController : BaseViewController<MainViewModel>, IMvxTabBarViewController
    {
        private const float TabHeight = 50f;

        private static readonly nfloat TabViewHeight = TabHeight + UIApplication.SharedApplication.KeyWindow.SafeAreaInsets.Bottom;
        private static readonly string[] TabImageNames = new string[]
        {
            ImageNames.MenuTabIcon,
            ImageNames.PromotionsTabIcon,
            ImageNames.CartTabIcon,
            ImageNames.ProfileTabIcon,
            ImageNames.InfoTabIcon
        };

        private readonly List<UIViewController> viewControllers = new List<UIViewController>();

        private UIView childContainer;
        private UIView tabView;
        private TabItemView[] tabs;
        private UIStackView tabStackView;
        private NSLayoutConstraint tabViewHeightConstraint;
        private bool isTabViewHidden;

        private UIViewController SelectedViewController => viewControllers[TabIndex];

        private int tabIndex;
        public int TabIndex
        {
            get => tabIndex;
            set
            {
                if (tabIndex == value)
                {
                    CloseTopViewControllerIfNeeded();
                    return;
                }

                Unselect(tabIndex);
                tabIndex = value;
                Select(tabIndex);
            }
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            View.BackgroundColor = Colors.Clear;

            InitTabView();
            InitializeTabStackView();
            InitializeTabViewTopBorder();
            InitializeChildContainer();

            _ = InitializeTabsAsync();
        }

        protected override void Bind()
        {
            base.Bind();

            var tab = tabs?.ElementAtOrDefault(2);
            if (tab == null)
            {
                return;
            }

            var bindingSet = CreateBindingSet();

            bindingSet.Bind(tab).For(v => v.BadgeCount).To(v => v.CartItemsTotalCount);

            bindingSet.Apply();
        }

        public void ShowTabView()
        {
            if (!isTabViewHidden)
            {
                return;
            }

            isTabViewHidden = false;

            Animate(() =>
            {
                tabViewHeightConstraint.Constant = TabViewHeight;
                UpdateFrameY(tabView, UIScreen.MainScreen.Bounds.Height - TabViewHeight);
                View.LayoutIfNeeded();
            });
        }

        public void HideTabView()
        {
            if (isTabViewHidden)
            {
                return;
            }

            isTabViewHidden = true;

            Animate(() =>
            {
                tabViewHeightConstraint.Constant = 0f;
                UpdateFrameY(tabView, UIScreen.MainScreen.Bounds.Height);
                View.LayoutIfNeeded();
            });
        }

        private async Task InitializeTabsAsync()
        {
            await ViewModel.LoadTabsCommand.ExecuteAsync();

            tabs = ViewModel.TabNames
                .Select((text, index) => new TabItemView(text, TabImageNames[index], () =>
                {
                    TabIndex = index;
                }))
                .Pipe(tabStackView.AddArrangedSubview)
                .ToArray();

            Select(0);

            Bind();
        }

        private void InitTabView()
        {
            tabView = new UIView();
            tabView.TranslatesAutoresizingMaskIntoConstraints = false;
            tabView.BackgroundColor = Colors.White;

            View.AddSubview(tabView);

            NSLayoutConstraint.ActivateConstraints(new[]
            {
                tabView.LeadingAnchor.ConstraintEqualTo(View.SafeAreaLayoutGuide.LeadingAnchor),
                tabView.TrailingAnchor.ConstraintEqualTo(View.SafeAreaLayoutGuide.TrailingAnchor),
                tabView.BottomAnchor.ConstraintEqualTo(View.BottomAnchor),
                tabViewHeightConstraint = tabView.HeightAnchor.ConstraintEqualTo(TabViewHeight)
            });
        }

        private void InitializeTabStackView()
        {
            tabStackView = new UIStackView
            {
                TranslatesAutoresizingMaskIntoConstraints = false,
                Axis = UILayoutConstraintAxis.Horizontal,
                Distribution = UIStackViewDistribution.FillEqually,
                Alignment = UIStackViewAlignment.Fill
            };

            tabView.AddSubview(tabStackView);

            NSLayoutConstraint.ActivateConstraints(new[]
            {
                tabStackView.LeadingAnchor.ConstraintEqualTo(tabView.LeadingAnchor),
                tabStackView.TopAnchor.ConstraintEqualTo(tabView.TopAnchor),
                tabStackView.TrailingAnchor.ConstraintEqualTo(tabView.TrailingAnchor),
                tabStackView.HeightAnchor.ConstraintEqualTo(TabHeight)
            });
        }

        private void InitializeTabViewTopBorder()
        {
            var view = new UIView();
            view.TranslatesAutoresizingMaskIntoConstraints = false;
            view.BackgroundColor = Colors.Orange2;

            tabView.AddSubview(view);

            NSLayoutConstraint.ActivateConstraints(new[]
            {
                view.LeadingAnchor.ConstraintEqualTo(tabView.LeadingAnchor),
                view.TopAnchor.ConstraintEqualTo(tabView.TopAnchor),
                view.TrailingAnchor.ConstraintEqualTo(tabView.TrailingAnchor),
                view.HeightAnchor.ConstraintEqualTo(1f)
            });
        }

        private void InitializeChildContainer()
        {
            childContainer = new UIView();
            childContainer.TranslatesAutoresizingMaskIntoConstraints = false;
            childContainer.BackgroundColor = Colors.FigmaBlack;

            View.AddSubview(childContainer);

            NSLayoutConstraint.ActivateConstraints(new[]
            {
                childContainer.LeadingAnchor.ConstraintEqualTo(View.LeadingAnchor),
                childContainer.TopAnchor.ConstraintEqualTo(View.TopAnchor),
                childContainer.TrailingAnchor.ConstraintEqualTo(View.TrailingAnchor),
                childContainer.BottomAnchor.ConstraintEqualTo(tabStackView.TopAnchor),
            });
        }

        private void Unselect(int index)
        {
            var tab = tabs.ElementAtOrDefault(index);
            if (tab != null)
            {
                tab.IsSelected = false;
            }
        }

        private void Select(int index)
        {
            var tab = tabs.ElementAtOrDefault(index);
            if (tab != null)
            {
                tab.IsSelected = true;
            }

            var viewController = viewControllers.ElementAtOrDefault(index);
            if (viewController != null)
            {
                childContainer.BringSubviewToFront(viewController.View);
            }
        }

        private void Animate(Action animation)
        {
            UIView.AnimateNotify(
                0.5d,
                0d,
                UIViewAnimationOptions.AllowUserInteraction,
                animation,
                null);
        }

        private void UpdateFrameY(UIView view, nfloat y)
        {
            view.Frame = new CGRect(view.Frame.X, y, view.Frame.Width, view.Frame.Height);
        }

        private void CloseTopViewControllerIfNeeded()
        {
            var viewControllers = SelectedViewController.ChildViewControllers;
            if (viewControllers.Length > 1)
            {
                var mvxView = (IMvxIosView) viewControllers.Last();
                var viewModel = (BasePageViewModel) mvxView.ViewModel;
                viewModel.PlatformCloseCommand.Execute(null);
            }
        }

        private void RemoveViewController(UIViewController viewController)
        {
            viewController.View.RemoveFromSuperview();
            viewController.WillMoveToParentViewController(null);
            viewController.DidMoveToParentViewController(null);

            viewControllers.Remove(viewController);
        }

        void IMvxTabBarViewController.ShowTabView(UIViewController viewController, MvxTabPresentationAttribute attribute)
        {
            viewController.View.Frame = childContainer.Bounds;
            childContainer.AddSubview(viewController.View);

            viewController.WillMoveToParentViewController(this);
            viewController.DidMoveToParentViewController(this);

            viewControllers.Add(viewController);
        }

        bool IMvxTabBarViewController.ShowChildView(UIViewController viewController)
        {
            if (SelectedViewController is UINavigationController navigationController)
            {
                navigationController.PushViewController(viewController, animated: true);
                return true;
            }
            else
            {
                return false;
            }
        }

        bool IMvxTabBarViewController.CloseChildViewModel(IMvxViewModel viewModel)
        {
            if (SelectedViewController is UINavigationController navigationController
                && navigationController.ViewControllers != null
                && navigationController.ViewControllers.Any())
            {
                if (navigationController.TopViewController.GetIMvxIosView().ViewModel == viewModel)
                {
                    navigationController.PopViewController(true);
                    return true;
                }

                var controllers = navigationController.ViewControllers.ToList();
                var controllerToClose = controllers.FirstOrDefault(vc => vc.GetIMvxIosView().ViewModel == viewModel);

                if (controllerToClose != null)
                {
                    controllers.Remove(controllerToClose);
                    navigationController.ViewControllers = controllers.ToArray();

                    return true;
                }
            }

            return false;
        }

        bool IMvxTabBarViewController.CloseTabViewModel(IMvxViewModel viewModel)
        {
            if (viewControllers.IsEmpty())
            {
                return false;
            }

            var plainToClose = viewControllers.Where(v => !(v is UINavigationController))
                                              .Select(v => v.GetIMvxIosView())
                                              .FirstOrDefault(mvxView => mvxView.ViewModel == viewModel);
            if (plainToClose != null)
            {
                RemoveViewController((UIViewController) plainToClose);
                return true;
            }

            foreach (var vc in viewControllers.OfType<UINavigationController>())
            {
                var root = vc.ViewControllers.FirstOrDefault();
                if (root != null && root.GetIMvxIosView().ViewModel == viewModel)
                {
                    RemoveViewController(vc);
                    return true;
                }
            }

            return false;
        }

        bool IMvxTabBarViewController.CanShowChildView() =>
            SelectedViewController is UINavigationController;

        private class TabItemView : UIView
        {
            private UILabel label;
            private UIImageView imageView;
            private BadgeView badgeView;

            public TabItemView(string title, string imageName, Action onTap)
            {
                AddGestureRecognizer(new UITapGestureRecognizer(onTap));

                InitializeLabel(title);
                InitializeImageView(imageName);
                InitializeBadgeLabel();

                IsSelected = false;
            }

            private bool isSelected;
            public bool IsSelected
            {
                get => isSelected;
                set
                {
                    isSelected = value;
                    if (isSelected)
                    {
                        label.TextColor = imageView.TintColor = Colors.Orange2;
                    }
                    else
                    {
                        label.TextColor = imageView.TintColor = Colors.FigmaBlack;
                    }
                }
            }

            public long BadgeCount
            {
                get => badgeView.Text;
                set
                {
                    badgeView.Text = value;
                    AnimateBadgeViewVisibility();
                }
            }

            private void InitializeLabel(string text)
            {
                label = new UILabel();
                label.TranslatesAutoresizingMaskIntoConstraints = false;
                label.Font = Font.Create(FontStyle.Medium, 9f);
                label.TextAlignment = UITextAlignment.Center;
                label.Text = text;

                AddSubview(label);

                NSLayoutConstraint.ActivateConstraints(new[]
                {
                    label.LeadingAnchor.ConstraintEqualTo(LeadingAnchor),
                    label.TrailingAnchor.ConstraintEqualTo(TrailingAnchor),
                    label.BottomAnchor.ConstraintEqualTo(BottomAnchor, -3f)
                });
            }

            private void InitializeImageView(string imageName)
            {
                imageView = new UIImageView();
                imageView.TranslatesAutoresizingMaskIntoConstraints = false;
                imageView.Image = UIImage.FromBundle(imageName).ImageWithRenderingMode(UIImageRenderingMode.AlwaysTemplate);

                AddSubview(imageView);

                NSLayoutConstraint.ActivateConstraints(new[]
                {
                    imageView.CenterXAnchor.ConstraintEqualTo(CenterXAnchor),
                    imageView.CenterYAnchor.ConstraintEqualTo(CenterYAnchor, -4f)
                });
            }

            private void InitializeBadgeLabel()
            {
                badgeView = new BadgeView();
                badgeView.TranslatesAutoresizingMaskIntoConstraints = false;

                AddSubview(badgeView);

                AnimateBadgeViewVisibility();

                NSLayoutConstraint.ActivateConstraints(new[]
                {
                    badgeView.CenterXAnchor.ConstraintEqualTo(imageView.RightAnchor),
                    badgeView.CenterYAnchor.ConstraintEqualTo(imageView.TopAnchor)
                });
            }

            private void AnimateBadgeViewVisibility()
            {
                Animate(0.2f, () => badgeView.Alpha = BadgeCount == 0 ? 0f : 1f);
            }

            private class BadgeView : UIView
            {
                private UILabel label;
                private CALayer badgeBackground;

                private long text;
                public long Text
                {
                    get => text;
                    set
                    {
                        text = value;
                        label.Text = value.ToString();
                        label.SizeToFit();
                        var size = Math.Min(Math.Max(label.Frame.Height, label.Frame.Width) + 2, 14);
                        badgeBackground.Frame = new CGRect(-(size - label.Frame.Width) / 2.0f, -(size - label.Frame.Height) / 2.0f, size, size);
                        badgeBackground.CornerRadius = (nfloat)(size / 2.0f);
                    }
                }

                public BadgeView()
                {
                    Initialze();
                }

                public BadgeView(NSCoder coder) : base(coder)
                {
                    Initialze();
                }

                public BadgeView(CGRect frame) : base(frame)
                {
                    Initialze();
                }

                protected BadgeView(NSObjectFlag t) : base(t)
                {
                    Initialze();
                }

                protected internal BadgeView(IntPtr handle) : base(handle)
                {
                }

                public override void AwakeFromNib()
                {
                    base.AwakeFromNib();
                    Initialze();
                }

                private void Initialze()
                {
                    label = new UILabel
                    {
                        TranslatesAutoresizingMaskIntoConstraints = false,
                        Font = Font.Create(FontStyle.Medium, 9f),
                        TextAlignment = UITextAlignment.Center,
                        TextColor = UIColor.White
                    };

                    badgeBackground = new CALayer
                    {
                        BackgroundColor = Colors.Orange2.CGColor
                    };

                    Layer.InsertSublayer(badgeBackground, 0);
                    AddSubview(label);

                    NSLayoutConstraint.ActivateConstraints(new[]
                    {
                        label.CenterXAnchor.ConstraintEqualTo(CenterXAnchor),
                        label.CenterYAnchor.ConstraintEqualTo(CenterYAnchor),
                        label.LeftAnchor.ConstraintEqualTo(LeftAnchor),
                        label.TopAnchor.ConstraintEqualTo(TopAnchor),
                        label.RightAnchor.ConstraintEqualTo(RightAnchor),
                        label.BottomAnchor.ConstraintEqualTo(BottomAnchor),
                    });
                }
            }
        }
    }
}

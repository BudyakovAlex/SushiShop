#nullable enable

using System;
using System.Linq;
using System.Threading.Tasks;
using CoreFoundation;
using CoreGraphics;
using MvvmCross.Platforms.Ios.Views;
using SushiShop.Core.Data.Models.Plugins;
using SushiShop.Core.Extensions;
using SushiShop.Core.Plugins;
using SushiShop.Ios.Extensions;
using SushiShop.Ios.Views.Controls;
using UIKit;

namespace SushiShop.Ios.Plugins
{
    public class Dialog : IDialog
    {
        private const double ToastAnimationDuration = 0.5d;
        private const double ToastDuration = 2d;

        public bool IsToastShown { get; protected set; }

        public Task ShowActionSheetAsync(string? title, string? message, string cancelTitle, params DialogAction[] actions)
        {
            var tcs = new TaskCompletionSource<object?>();

            DispatchQueue.MainQueue.DispatchAsync(() =>
            {
                var alertController = UIAlertController.Create(title, message, UIAlertControllerStyle.ActionSheet);

                actions.Select(CreateDefaultAlertAction).ForEach(alertController.AddAction);
                alertController.AddAction(UIAlertAction.Create(cancelTitle, UIAlertActionStyle.Cancel, _ => tcs.TrySetResult(null)));

                var viewController = UIApplication.SharedApplication.KeyWindow.GetTopViewController();
                viewController.PresentViewController(alertController, true, null);
            });

            return tcs.Task;

            UIAlertAction CreateDefaultAlertAction(DialogAction alertAction)
            {
                return UIAlertAction.Create(alertAction.Title, UIAlertActionStyle.Default, _ =>
                {
                    tcs.TrySetResult(null);
                    alertAction.Command.Execute(null);
                });
            }
        }

        public async Task ShowToastAsync(string message)
        {
            try
            {
                var taskCompletionSource = new TaskCompletionSource<bool>();

                IsToastShown = true;

                var keyWindow = UIApplication.SharedApplication.KeyWindow;
                if (keyWindow == null)
                {
                    IsToastShown = false;
                    return;
                }

                var topViewController = GetTopViewController(keyWindow);
                if (topViewController is null ||
                    topViewController is UIAlertController)
                {
                    IsToastShown = false;
                    return;
                }

                var toast = new ToastView(message, taskCompletionSource)
                {
                    TranslatesAutoresizingMaskIntoConstraints = false
                };

                var superview = topViewController.View!;
                superview.AddSubview(toast);

                NSLayoutConstraint topConstraint;
                NSLayoutConstraint.ActivateConstraints(new[]
                {
                    toast.LeadingAnchor.ConstraintEqualTo(superview.LeadingAnchor),
                    topConstraint = toast.TopAnchor.ConstraintEqualTo(superview.SafeAreaLayoutGuide.TopAnchor),
                    toast.TrailingAnchor.ConstraintEqualTo(superview.TrailingAnchor)
                });

                superview.LayoutIfNeeded();
                topConstraint.Constant = -toast.Bounds.Height;
                superview.LayoutIfNeeded();

                topConstraint.Constant = 0;
                UIView.Transition(
                    toast,
                    0.3d,
                    UIViewAnimationOptions.CurveLinear,
                    () => superview.LayoutIfNeeded(),
                    () => { });

                DispatchQueue.MainQueue.DispatchAfter(
                    new DispatchTime(DispatchTime.Now, TimeSpan.FromSeconds(ToastDuration)),
                    () =>
                    {
                        topConstraint.Constant = -toast.Bounds.Height;
                        UIView.Transition(
                            toast,
                            0.3d,
                            UIViewAnimationOptions.CurveLinear,
                            () => superview.LayoutIfNeeded(),
                            () =>
                            {
                                toast.RemoveFromSuperview();
                                IsToastShown = false;
                                taskCompletionSource?.TrySetResult(true);
                             });
                    });

                await taskCompletionSource.Task;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }

        public static UIViewController? GetTopViewController(UIWindow window)
        {
            var topViewController = window.RootViewController;
            if (topViewController?.PresentedViewController is null &&
                topViewController is MvxTabBarViewController tabBarController)
            {
                topViewController = tabBarController.VisibleUIViewController;
            }
            else
            {
                while (topViewController?.PresentedViewController != null)
                {
                    topViewController = topViewController.PresentedViewController;
                }
            }

            return topViewController is UINavigationController navigationController
                ? navigationController.ViewControllers.LastOrDefault()
                : topViewController;
        }
    }
}

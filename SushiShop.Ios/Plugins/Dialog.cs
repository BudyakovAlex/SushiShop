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

                NSLayoutConstraint.ActivateConstraints(new[]
                {
                    toast.LeadingAnchor.ConstraintEqualTo(superview.LeadingAnchor),
                    toast.TopAnchor.ConstraintEqualTo(superview.SafeAreaLayoutGuide.TopAnchor),
                    toast.TrailingAnchor.ConstraintEqualTo(superview.TrailingAnchor)
                });

                toast.Frame = new CGRect(0f, -toast.Frame.Height, toast.Frame.Width, toast.Frame.Height);
                UIView.Animate(
                    ToastAnimationDuration,
                    () => toast.Frame = new CGRect(0f, 0f, toast.Frame.Width, toast.Frame.Height));

                DispatchQueue.MainQueue.DispatchAfter(
                    new DispatchTime(DispatchTime.Now, TimeSpan.FromSeconds(ToastDuration)),
                    () =>
                    {
                        toast.RemoveFromSuperview();
                        IsToastShown = false;
                        taskCompletionSource?.TrySetResult(true);
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

﻿#nullable enable
using CoreFoundation;
using MvvmCross.Platforms.Ios.Views;
using SushiShop.Core.Data.Enums;
using SushiShop.Core.Data.Models.Plugins;
using SushiShop.Core.Extensions;
using SushiShop.Core.Plugins;
using SushiShop.Ios.Extensions;
using SushiShop.Ios.Views.Controls;
using SushiShop.Ios.Views.ViewControllers.Common.Dialogs;
using System;
using System.Linq;
using System.Threading.Tasks;
using UIKit;
using Xamarin.Essentials;

namespace SushiShop.Ios.Plugins
{
    public class Dialog : IDialog
    {
        private const double ToastDuration = 2d;

        private TaskCompletionSource<bool>? toastDismissTaskCompletionSource;

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

        public Task<DateTime?> ShowDatePickerAsync(
            DateTime initialDate,
            DateTime? minDate,
            DateTime? maxDate,
            DatePickerMode mode = DatePickerMode.Date)
        {
            var keyWindow = UIApplication.SharedApplication.KeyWindow;
            if (keyWindow is null)
            {
                return Task.FromResult<DateTime?>(null);
            }

            var topViewController = GetTopViewController(keyWindow);

            var tcs = new TaskCompletionSource<DateTime?>();
            MainThread.InvokeOnMainThreadAsync(() =>
            {
                var completeAction = new Action<DateTime?>((selectedDate) =>
                {
                    topViewController?.DismissModalViewController(true);
                    tcs.TrySetResult(selectedDate);
                });

                var dialogViewController = new DatePickerViewController(completeAction, initialDate, minDate, maxDate, mode)
                {
                    ModalTransitionStyle = UIModalTransitionStyle.CrossDissolve,
                    ModalPresentationStyle = UIModalPresentationStyle.OverCurrentContext,
                };

                topViewController?.PresentViewController(dialogViewController, true, null);
            });

            return tcs.Task;
        }

        public async Task ShowToastAsync(string message, bool isEndless = false)
        {
            try
            {
                toastDismissTaskCompletionSource = new TaskCompletionSource<bool>();
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

                var toast = new ToastView(message, isEndless, toastDismissTaskCompletionSource)
                {
                    TranslatesAutoresizingMaskIntoConstraints = false
                };

                var superview = topViewController.View!;
                superview.AddSubview(toast);

                var toastTopConstraint = toast.TopAnchor.ConstraintEqualTo(superview.SafeAreaLayoutGuide.TopAnchor);
                NSLayoutConstraint.ActivateConstraints(new[]
                {
                    toast.LeadingAnchor.ConstraintEqualTo(superview.LeadingAnchor),
                    toastTopConstraint,
                    toast.TrailingAnchor.ConstraintEqualTo(superview.TrailingAnchor)
                });

                superview.LayoutIfNeeded();
                toastTopConstraint.Constant = -toast.Bounds.Height;
                superview.LayoutIfNeeded();

                toastTopConstraint.Constant = 0;
                UIView.Transition(
                    toast,
                    0.3d,
                    UIViewAnimationOptions.CurveLinear,
                    () => superview.LayoutIfNeeded(),
                    () => { });

                var dismissTask = isEndless ? toastDismissTaskCompletionSource!.Task : Task.Delay(TimeSpan.FromSeconds(ToastDuration));
                await dismissTask;

                DismissToast(
                    toastTopConstraint,
                    toast,
                    superview);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }

        public void DismissToast()
        {
            toastDismissTaskCompletionSource?.TrySetResult(true);
        }

        private void DismissToast(
            NSLayoutConstraint toastTopConstraint,
            ToastView toast,
            UIView superview)
        {
            DispatchQueue.MainQueue.DispatchAsync(
                () =>
                {
                    toastTopConstraint.Constant = -toast.Bounds.Height;
                    UIView.Transition(
                        toast,
                        0.3d,
                        UIViewAnimationOptions.CurveLinear,
                        () => superview.LayoutIfNeeded(),
                        () =>
                        {
                            toast.RemoveFromSuperview();
                            IsToastShown = false;
                        });
                });
        }

        private static UIViewController? GetTopViewController(UIWindow window)
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

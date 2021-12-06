using Acr.UserDialogs;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Util;
using Android.Views;
using Android.Widget;
using AndroidX.Core.Content.Resources;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Extensions;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Listeners;
using Google.Android.Material.Snackbar;
using SushiShop.Core.Data.Enums;
using SushiShop.Core.Data.Models.Plugins;
using SushiShop.Core.Plugins;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SushiShop.Droid.Plugins
{
    public class Dialog : IDialog
    {
        private readonly IUserDialogs userDialogs;
        private Snackbar lastSnackBar;

        public Dialog(IUserDialogs userDialogs)
        {
            this.userDialogs = userDialogs;
        }

        public void DismissToast() =>
            lastSnackBar?.Dismiss();

        public async Task ShowActionSheetAsync(string title, string message, string cancelTitle, params DialogAction[] actions)
        {
            var param = actions.Select(action => action.Title).ToArray();
            var result = await userDialogs.ActionSheetAsync(title, cancelTitle, null, null, param);
            foreach (var action in actions)
            {
                if (action.Title == result)
                {
                    action.Command?.Execute(null);
                }
            }
        }

        public async Task<DateTime?> ShowDatePickerAsync(DateTime initialDate, DateTime? minDate, DateTime? maxDate, DatePickerMode mode = DatePickerMode.Date)
        {
            if (mode == DatePickerMode.Date)
            {
                var result = await userDialogs.DatePromptAsync(
                    new DatePromptConfig()
                    {
                        SelectedDate = initialDate,
                        MinimumDate = minDate,
                        MaximumDate = maxDate,
                    });

                return result.Value;
            }

            var dateResult = await userDialogs.DatePromptAsync(
                new DatePromptConfig()
                {
                    SelectedDate = initialDate,
                    MinimumDate = minDate,
                    MaximumDate = maxDate,
                });

            if (!dateResult.Ok)
            {
                return null;
            }
            
            var timeResult = await userDialogs.TimePromptAsync(
                new TimePromptConfig()
                {
                    Use24HourClock = true,
                    SelectedTime = initialDate.TimeOfDay,
                });

            if (!timeResult.Ok || timeResult.Value.TotalSeconds + 60.0 < minDate?.TimeOfDay.TotalSeconds)
            {
                return null;
            }

            var selectedDate = dateResult.Value.Date.Add(timeResult.Value);
            return selectedDate;
        }

        public async Task ShowToastAsync(string message, bool isEndless = false)
        {
            var activity = Xamarin.Essentials.Platform.CurrentActivity;
            if (activity?.Window?.DecorView is null)
            {
                return;
            }

            var view = activity.Window.DecorView.FindViewById(Android.Resource.Id.Content);
            if (view is null)
            {
                return;
            }

            var resource = view.Context.Resources;
            var coordinator = view.FindViewById(Resource.Id.coordinator);

            try
            {
                var length = isEndless ? Snackbar.LengthIndefinite : Snackbar.LengthLong;
                lastSnackBar = Snackbar.Make(view, message, length);

                var taskCompletionSource = new TaskCompletionSource<bool>();

                lastSnackBar.AddCallback(new SnackbarCallback(() => taskCompletionSource?.TrySetResult(true)));
                var containerView = lastSnackBar.View as FrameLayout;
                containerView.SetBackgroundColor(new Color(activity.GetColor(Resource.Color.orange)));
                var linearLayout = containerView.GetChildAt(0) as LinearLayout;
                if (linearLayout != null)
                {
                    var textView = linearLayout.GetChildAt(0) as TextView;
                    if (textView != null)
                    {
                        var textViewLayoutParams = textView.LayoutParameters as ViewGroup.MarginLayoutParams;
                        textViewLayoutParams.MarginEnd = (int)activity.DpToPx(66);
                        textView.LayoutParameters = textViewLayoutParams;

                        var typeface = ResourcesCompat.GetFont(containerView.Context, Resource.Font.sf_pro_display_medium);
                        textView.SetTypeface(typeface, TypefaceStyle.Normal);
                        textView.SetTextSize(ComplexUnitType.Dip, 16);
                    }

                    if (!isEndless)
                    {
                        InitializeCloseImageView(activity, containerView);
                    }
                }

                lastSnackBar.Show();

                await taskCompletionSource.Task;
            }
            catch
            {
                return;
            }
        }

        private void InitializeCloseImageView(Context context, FrameLayout containerView)
        {
            var imageView = new ImageView(containerView.Context);
            imageView.SetImageResource(Resource.Drawable.ic_delete_transparent);
            imageView.SetOnClickListener(new ViewOnClickListener(OnDismissImageClickedAsync));
            imageView.SetScaleType(ImageView.ScaleType.Center);

            var imageSize = (int)context.DpToPx(40);
            containerView.AddView(imageView, new FrameLayout.LayoutParams(imageSize, imageSize)
            {
                MarginEnd = (int)context.DpToPx(8),
                Gravity = GravityFlags.End | GravityFlags.CenterVertical
            });
        }

        private Task OnDismissImageClickedAsync(View _)
        {
            DismissToast();
            return Task.CompletedTask;
        }

        private class SnackbarCallback : Snackbar.Callback
        {
            private readonly Action dismissAction;

            public SnackbarCallback(Action dismissAction)
            {
                this.dismissAction = dismissAction;
            }

            public override void OnDismissed(Snackbar transientBottomBar, int @event)
            {
                base.OnDismissed(transientBottomBar, @event);
                dismissAction?.Invoke();
            }
        }
    }
}
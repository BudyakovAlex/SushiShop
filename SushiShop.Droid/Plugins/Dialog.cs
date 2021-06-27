using Acr.UserDialogs;
using Android.App;
using Android.Graphics;
using Android.Widget;
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

            if (!timeResult.Ok)
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
            }
            catch
            {
                return;
            }

            var taskCompletionSource = new TaskCompletionSource<bool>();

            lastSnackBar.AddCallback(new SnackbarCallback(() => taskCompletionSource?.TrySetResult(true)));
            lastSnackBar.View.SetBackgroundColor(new Color(activity.GetColor(Resource.Color.orange)));
            lastSnackBar.Show();

            await taskCompletionSource.Task;
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
using Acr.UserDialogs;
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

        public Dialog(IUserDialogs userDialogs)
        {
            this.userDialogs = userDialogs;
        }

        public void DismissToast()
        {
        }

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
            var result = await userDialogs.DatePromptAsync(
                new DatePromptConfig()
                {
                    SelectedDate = initialDate,
                    MinimumDate = minDate,
                    MaximumDate = maxDate
                });

            return result.Value;
        }

        public Task ShowToastAsync(string message, bool isEndless = false)
        {
            return Task.CompletedTask;
        }
    }
}
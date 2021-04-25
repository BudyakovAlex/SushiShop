using SushiShop.Core.Data.Enums;
using SushiShop.Core.Data.Models.Plugins;
using SushiShop.Core.Plugins;
using System;
using System.Threading.Tasks;

namespace SushiShop.Droid.Plugins
{
    public class Dialog : IDialog
    {
        public void DismissToast()
        {
        }

        public Task ShowActionSheetAsync(string title, string message, string cancelTitle, params DialogAction[] actions)
        {
            return Task.CompletedTask;
        }

        public Task<DateTime?> ShowDatePickerAsync(DateTime initialDate, DateTime? minDate, DateTime? maxDate, DatePickerMode mode = DatePickerMode.Date)
        {
            return Task.FromResult<DateTime?>(null);
        }

        public Task ShowToastAsync(string message, bool isEndless = false)
        {
            return Task.CompletedTask;
        }
    }
}
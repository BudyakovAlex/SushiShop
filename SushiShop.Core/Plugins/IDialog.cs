using System;
using System.Threading.Tasks;
using SushiShop.Core.Data.Enums;
using SushiShop.Core.Data.Models.Plugins;

namespace SushiShop.Core.Plugins
{
    public interface IDialog
    {
        Task ShowActionSheetAsync(string? title, string? message, string cancelTitle, params DialogAction[] actions);

        Task ShowToastAsync(string message, bool isEndless = false);

        void DismissToast();

        Task<DateTime?> ShowDatePickerAsync(
            DateTime initialDate,
            DateTime? minDate,
            DateTime? maxDate,
            DatePickerMode mode = DatePickerMode.Date);
    }
}

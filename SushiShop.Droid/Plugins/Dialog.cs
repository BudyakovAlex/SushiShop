using SushiShop.Core.Data.Enums;
using SushiShop.Core.Data.Models.Plugins;
using SushiShop.Core.Plugins;
using System;
using System.Threading.Tasks;

namespace SushiShop.Droid.Plugins
{
    public class Dialog : IDialog
    {
        public Task ShowActionSheetAsync(string title, string message, string cancelTitle, params DialogAction[] actions)
        {
            throw new NotImplementedException();
        }

        public Task<DateTime?> ShowDatePickerAsync(DateTime initialDate, DateTime? minDate, DateTime? maxDate, DatePickerMode mode = DatePickerMode.Date)
        {
            throw new NotImplementedException();
        }

        public Task ShowToastAsync(string message)
        {
            throw new NotImplementedException();
        }
    }
}

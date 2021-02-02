using SushiShop.Core.Data.Enums;
using UIKit;

namespace SushiShop.Ios.Extensions
{
    public static class DatePickerModeExtensions
    {
        public static UIDatePickerMode ToUIDatePickerMode(this DatePickerMode mode)
        {
            return mode == DatePickerMode.DateAndTime ? UIDatePickerMode.DateAndTime : UIDatePickerMode.Date;
        }
    }
}
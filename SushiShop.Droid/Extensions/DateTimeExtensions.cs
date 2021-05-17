using System;

namespace SushiShop.Droid.Extensions
{
    public static class DateTimeExtensions
    {
        public static long ToDialogPickerDate(this DateTime dateTime)
        {
            return (long)(dateTime.Date - new DateTime(1970, 1, 1)).TotalMilliseconds;
        }
    }
}

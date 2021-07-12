using Android.Content;
using Android.OS;
using Android.Views.InputMethods;

namespace SushiShop.Droid.Extensions
{
    public static class ContextExtensions
    {
        public static void HideKeyboard(this Context context, IBinder windowToken)
        {
            var manager = (InputMethodManager)context.GetSystemService(Context.InputMethodService);
            manager.HideSoftInputFromWindow(windowToken, HideSoftInputFlags.None);
        }
    }
}

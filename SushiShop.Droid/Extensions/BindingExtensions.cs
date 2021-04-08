using Android.Widget;
using SushiShop.Droid.TargetBindings;

namespace SushiShop.Droid.Extensions
{
    public static class BindingExtensions
    {
        public static string BindUrl(this ImageView _) => nameof(ImageViewUrlTargetBinding);
    }
}

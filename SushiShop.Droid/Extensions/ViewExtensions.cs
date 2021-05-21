using Android.Views;
using SushiShop.Droid.Views.OutlineProviders;

namespace SushiShop.Droid.Extensions
{
    public static class ViewExtensions
    {
        public static void SetTopRoundedCorners(this View view, float radius)
        {
            view.OutlineProvider = new TopCornersOutlineProvider(radius);
        }
    }
}

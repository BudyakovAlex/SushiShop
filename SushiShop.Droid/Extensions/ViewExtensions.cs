using Android.Views;
using AndroidX.Core.View;
using SushiShop.Droid.Views.OutlineProviders;

namespace SushiShop.Droid.Extensions
{
    public static class ViewExtensions
    {
        public const int CommonAnimationDuration = 250;

        public static void SetTopRoundedCorners(this View view, float radius)
        {
            view.OutlineProvider = new TopCornersOutlineProvider(radius);
        }

        public static void SlideUpAnimation(this View view, int duration = CommonAnimationDuration)
        {
            var propertyAnimator = ViewCompat.Animate(view);
            propertyAnimator.SetDuration(duration);
            propertyAnimator.TranslationY(0);
            propertyAnimator.Start();
        }

        public static void SlideDownAnimation(this View view, int duration = CommonAnimationDuration)
        {
            var propertyAnimator = ViewCompat.Animate(view);
            propertyAnimator.SetDuration(duration);
            propertyAnimator.TranslationY(view.Height);
            propertyAnimator.Start();
        }
    }
}
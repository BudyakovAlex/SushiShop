using Android.Views;
using AndroidX.Core.View;
using Java.Lang;
using SushiShop.Droid.Views.OutlineProviders;
using System;

namespace SushiShop.Droid.Extensions
{
    public static class ViewExtensions
    {
        public const int CommonAnimationDuration = 250;

        public static void SetTopRoundedCorners(this View view, float radius)
        {
            view.OutlineProvider = new TopCornersOutlineProvider(radius);
        }

        public static void SlideUpAnimation(this View view,
                                            int duration = CommonAnimationDuration,
                                            Action beforeAction = null,
                                            Action completionAction = null)
        {
            var propertyAnimator = ViewCompat.Animate(view);
            propertyAnimator.SetDuration(duration);
            propertyAnimator.WithStartAction(new Runnable(() => beforeAction?.Invoke()));
            propertyAnimator.WithEndAction(new Runnable(() => completionAction?.Invoke()));
            propertyAnimator.TranslationY(0);
            propertyAnimator.Start();
        }

        public static void SlideDownAnimation(this View view,
            int duration = CommonAnimationDuration,
            Action beforeAction = null,
            Action completionAction = null)
        {
            var propertyAnimator = ViewCompat.Animate(view);
            propertyAnimator.SetDuration(duration);
            propertyAnimator.WithStartAction(new Runnable(() => beforeAction?.Invoke()));
            propertyAnimator.WithEndAction(new Runnable(() => completionAction?.Invoke()));
            propertyAnimator.TranslationY(view.Height);
            propertyAnimator.Start();
        }
    }
}
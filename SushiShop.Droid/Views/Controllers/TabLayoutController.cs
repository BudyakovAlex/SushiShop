using System;
using Android.Widget;
using AndroidX.CoordinatorLayout.Widget;
using AndroidX.Core.View;
using AndroidX.ViewPager.Widget;
using SushiShop.Droid.Views.Activities;
using SushiShop.Droid.Views.Listeners;

namespace SushiShop.Droid.Views.Controllers
{
    public class TabLayoutController : ITabLayoutController
    {
        private const int AnimationDuration = 250;

        private ViewPager viewPager;
        private LinearLayout bottomLinearLayout;
        private bool isHidden;

        public TabLayoutController()
        {
        }

        public void Show()
        {
            if (!isHidden || !CheckAreMembersValid())
            {
                return;
            }

            SetPosition(-bottomLinearLayout.Height, true);
        }

        public void Hide()
        {
            if (isHidden || !CheckAreMembersValid())
            {
                return;
            }

            SetPosition(bottomLinearLayout.Height, true);
        }

        public void SetPosition(float margin, bool shouldAnimate = false)
        {
            if (!CheckAreMembersValid())
            {
                return;
            }

            margin = Math.Max(0, Math.Min(bottomLinearLayout.Height, bottomLinearLayout.TranslationY + margin));
            isHidden = margin == bottomLinearLayout.Height;

            ViewCompat.Animate(bottomLinearLayout)
                .SetDuration(shouldAnimate ? AnimationDuration : 0)
                .TranslationY(margin)
                .SetUpdateListener(new ViewPropertyAnimatorUpdateListener(view =>
                {
                    viewPager.LayoutParameters = new CoordinatorLayout.LayoutParams(viewPager.LayoutParameters)
                    {
                        BottomMargin = bottomLinearLayout.Height - (int)bottomLinearLayout.TranslationY
                    };
                }))
                .Start();
        }

        private bool CheckAreMembersValid()
        {
            if (viewPager == null)
            {
                viewPager = MainActivity.Instance.FindViewById<ViewPager>(Resource.Id.main_view_pager);
            }

            if (bottomLinearLayout == null)
            {
                bottomLinearLayout = MainActivity.Instance.FindViewById<LinearLayout>(Resource.Id.bottom_linear_layout);
            }

            return viewPager != null || bottomLinearLayout != null;
        }
    }
}

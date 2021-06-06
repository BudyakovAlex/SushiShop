using System;
using Android.Widget;
using AndroidX.CoordinatorLayout.Widget;
using AndroidX.Core.View;
using AndroidX.ViewPager.Widget;
using SushiShop.Droid.Views.Activities;
using SushiShop.Droid.Views.Listeners;

namespace SushiShop.Droid.Helpers
{
    public class HideTabLayoutHelper
    {
        private const int AnimationDuration = 250;

        private ViewPager viewPager;
        private LinearLayout bottomLinearLayout;
        private bool isHidden;

        private HideTabLayoutHelper()
        {
        }

        private static HideTabLayoutHelper _instance;
        public static HideTabLayoutHelper Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new HideTabLayoutHelper();
                }

                return _instance;
            }
        }

        public void Show()
        {
            if (!isHidden || !CheckAreMembersValid())
            {
                return;
            }

            isHidden = false;
            SetPosition(-bottomLinearLayout.Height, true);
        }

        public void Hide()
        {
            if (isHidden || !CheckAreMembersValid())
            {
                return;
            }

            isHidden = true;
            SetPosition(bottomLinearLayout.Height, true);
        }

        public void SetPosition(float margin, bool shouldAnimate = false)
        {
            if (!CheckAreMembersValid())
            {
                return;
            }

            margin = Math.Max(0, Math.Min(bottomLinearLayout.Height, bottomLinearLayout.TranslationY + margin));
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
            if (!(Xamarin.Essentials.Platform.CurrentActivity is MainActivity mainActivity))
            {
                return false;
            }

            if (viewPager == null)
            {
                viewPager = mainActivity.FindViewById<ViewPager>(Resource.Id.main_view_pager);
            }

            if (bottomLinearLayout == null)
            {
                bottomLinearLayout = mainActivity.FindViewById<LinearLayout>(Resource.Id.bottom_linear_layout);
            }

            return viewPager != null || bottomLinearLayout != null;
        }
    }
}

using System;
using Android.Widget;
using AndroidX.CoordinatorLayout.Widget;
using AndroidX.RecyclerView.Widget;
using SushiShop.Droid.Views.Activities;

namespace SushiShop.Droid.Views.Listeners
{
    public class HideTabLayoutRecyclerViewScrollListener : RecyclerView.OnScrollListener
    {
        private FrameLayout frameLayout;
        private LinearLayout bottomLinearLayout;

        public HideTabLayoutRecyclerViewScrollListener()
        {
        }

        public override void OnScrolled(RecyclerView recyclerView, int dx, int dy)
        {
            base.OnScrolled(recyclerView, dx, dy);

            if (!(recyclerView.Context is MainActivity mainActivity))
            {
                return;
            }

            if (frameLayout == null)
            {
                frameLayout = mainActivity.FindViewById<FrameLayout>(Resource.Id.container_view);
            }

            if (bottomLinearLayout == null)
            {
                bottomLinearLayout = mainActivity.FindViewById<LinearLayout>(Resource.Id.bottom_linear_layout);
            }

            SetPosition(Math.Max(0, Math.Min(bottomLinearLayout.Height, bottomLinearLayout.TranslationY + dy)));
        }

        public void ResetPosition()
        {
            SetPosition(0);
        }

        private void SetPosition(float margin)
        {
            if (bottomLinearLayout == null || frameLayout == null)
            {
                return;
            }

            bottomLinearLayout.TranslationY = margin;
            frameLayout.LayoutParameters = new CoordinatorLayout.LayoutParams(frameLayout.LayoutParameters)
            {
                BottomMargin = bottomLinearLayout.Height - (int)bottomLinearLayout.TranslationY
            };
        }
    }
}

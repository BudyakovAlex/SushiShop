using Android.Graphics;
using Android.Views;
using AndroidX.RecyclerView.Widget;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Extensions;

namespace SushiShop.Droid.Views.Decorators
{
    public class MenuGridItemDecorator : RecyclerView.ItemDecoration
    {
        public MenuGridItemDecorator()
        {
        }

        public override void GetItemOffsets(Rect outRect, View view, RecyclerView parent, RecyclerView.State state)
        {
            base.GetItemOffsets(outRect, view, parent, state);

            var position = parent.GetChildLayoutPosition(view);
            if (position == 0)
            {
                return;
            }

            var margin = (int)parent.Context.DpToPx(5);
            if (position == 1)
            {
                outRect.Left = margin;
                outRect.Right = margin;
                return;
            }

            if (position % 3 == 0)
            {
                outRect.Left = margin;
                return;
            }

            if (position % 3 == 2)
            {
                outRect.Right = margin;
                return;
            }
            else
            {
                outRect.Left = margin;
                outRect.Right = margin;
                return;
            }
        }
    }
}

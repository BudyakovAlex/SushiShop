using System;
using Android.Graphics;
using Android.Views;
using AndroidX.RecyclerView.Widget;

namespace SushiShop.Droid.Views.Decorators
{
    public class SpacesItemDecoration : RecyclerView.ItemDecoration
    {
        private Func<int, Rect, Rect> func;

        public SpacesItemDecoration(Func<int, Rect, Rect> func)
        {
            this.func = func;
        }

        public override void GetItemOffsets(Rect outRect, View view, RecyclerView parent, RecyclerView.State state)
        {
            var position = parent.GetChildLayoutPosition(view);
            outRect = func?.Invoke(position, outRect);
        }
    }
}

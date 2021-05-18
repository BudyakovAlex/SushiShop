using System;
using Android.Text;
using Android.Text.Style;
using Android.Views;

namespace SushiShop.Droid.Views.Spans
{
    public class LinkSpan : ClickableSpan
    {
        private readonly Action action;

        public LinkSpan(Action action)
        {
            this.action = action;
        }

        public override void OnClick(View widget)
        {
            action?.Invoke();
        }

        public override void UpdateDrawState(TextPaint ds)
        {
            base.UpdateDrawState(ds);
            ds.UnderlineText = true;
        }
    }
}

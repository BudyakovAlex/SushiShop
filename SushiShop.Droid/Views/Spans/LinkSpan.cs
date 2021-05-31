using System;
using Android.Text;
using Android.Text.Style;
using Android.Views;

namespace SushiShop.Droid.Views.Spans
{
    public class LinkSpan : ClickableSpan
    {
        private readonly Action<string> action;

        private readonly string text;
        private readonly bool isUnderlineText;

        public LinkSpan(Action<string> action, string text = null, bool isUnderlineText = true)
        {
            this.action = action;
            this.text = text;
            this.isUnderlineText = isUnderlineText;
        }

        public override void OnClick(View widget)
        {
            action?.Invoke(text);
        }

        public override void UpdateDrawState(TextPaint ds)
        {
            base.UpdateDrawState(ds);
            ds.UnderlineText = isUnderlineText;
        }
    }
}

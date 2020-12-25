using System;
using CoreGraphics;
using UIKit;

namespace SushiShop.Ios.Views.Controls.Abstract
{
    public abstract class BaseTextView : UITextView
    {
        protected BaseTextView()
        {
            Initialize();
        }

        protected BaseTextView(CGRect frame)
            : base(frame)
        {
            Initialize();
        }

        protected BaseTextView(CGRect frame, NSTextContainer textContainer)
            : base(frame, textContainer)
        {
            Initialize();
        }

        protected BaseTextView(IntPtr handle)
            : base(handle)
        {
        }

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();
            Initialize();
        }

        protected virtual void Initialize()
        {
        }
    }
}

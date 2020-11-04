using System;
using CoreAnimation;
using CoreGraphics;
using Foundation;
using SushiShop.Ios.Common;
using UIKit;

namespace SushiShop.Ios.Views.Controls
{
    [Register(nameof(PrimaryButton))]
    public class PrimaryButton : UIButton
    {
        private CAGradientLayer gradientLayer;

        public PrimaryButton()
        {
            Initialize();
        }

        public PrimaryButton(UIButtonType type) : base(type)
        {
            Initialize();
        }

        public PrimaryButton(NSCoder coder) : base(coder)
        {
            Initialize();
        }

        public PrimaryButton(CGRect frame) : base(frame)
        {
            Initialize();
        }

        protected PrimaryButton(NSObjectFlag t) : base(t)
        {
            Initialize();
        }

        protected internal PrimaryButton(IntPtr handle) : base(handle)
        {
            Initialize();
        }

        private void Initialize()
        {
            gradientLayer = Components.CreateGradientLayer();
            Layer.AddSublayer(gradientLayer);
        }

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();
            gradientLayer.Frame = Bounds;
        }
    }
}

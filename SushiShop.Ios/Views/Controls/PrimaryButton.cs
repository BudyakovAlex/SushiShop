using System;
using CoreAnimation;
using CoreGraphics;
using Foundation;
using SushiShop.Ios.Common;
using SushiShop.Ios.Common.Styles;
using UIKit;

namespace SushiShop.Ios.Views.Controls
{
    [Register(nameof(PrimaryButton))]
    public class PrimaryButton : UIButton
    {
        private CAGradientLayer gradientLayer;

        private bool withoutRadius;
        public bool WithoutRadius
        {
            get => withoutRadius;
            set
            {
                if (value == withoutRadius)
                {
                    return;
                }

                withoutRadius = value;
                SetCornerRadius();
            }
        }

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
        }

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();
            Initialize();
        }

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();
            gradientLayer.Frame = Bounds;
        }

        private void Initialize()
        {
            gradientLayer = Components.CreateGradientLayer();
            Layer.InsertSublayer(gradientLayer, 0);
            BringSubviewToFront(ImageView);
            SetCornerRadius();
        }

        private void SetCornerRadius()
        {
            if (WithoutRadius)
            {
                this.SetCornerRadius(0);
            }
            else
            {
                this.SetCornerRadius(null);
            }
        }
    }
}

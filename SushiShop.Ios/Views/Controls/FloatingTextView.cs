using System;
using CoreAnimation;
using CoreGraphics;
using Foundation;
using SushiShop.Ios.Common;
using SushiShop.Ios.Extensions;
using SushiShop.Ios.Views.Controls.Abstract;
using UIKit;

namespace SushiShop.Ios.Views.Controls
{
    [Register(nameof(FloatingTextView))]
    public class FloatingTextView : BaseTextView, IUITextViewDelegate
    {
        private const float BottomLineHeight = 1f;
        private const float FontScale = 0.75f;

        private CALayer bottomLineLayer;
        private UILabel placeholderLabel;

        public FloatingTextView()
        {
        }

        protected FloatingTextView(IntPtr handle)
            : base(handle)
        {
        }

        public string Placeholder
        {
            get => placeholderLabel.Text;
            set
            {
                placeholderLabel.Text = value;
                UpdatePlaceholderLabelFrame();
            }
        }

        private bool isEmpty = true;
        private bool IsEmpty
        {
            get => isEmpty;
            set
            {
                if (isEmpty == value)
                {
                    return;
                }

                isEmpty = value;
                UpdatePlaceholderLabelFrame();
            }
        }

        [Export("textViewDidChange:")]
        public void DidChange(UITextView _)
        {
            IsEmpty = string.IsNullOrEmpty(Text);
        }

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();
            bottomLineLayer.Frame = new CGRect(0f, TextInputView.Frame.Bottom - BottomLineHeight, Bounds.Width, BottomLineHeight);
            if (ContentOffset.Y < TextInputView.Frame.Height - Frame.Height)
            {
                this.SetContentOffset(new CGPoint(0, TextInputView.Frame.Height - Frame.Height), false);
            }
        }

        protected override void Initialize()
        {
            base.Initialize();

            Delegate = this;

            var sideInset = -TextContainer.LineFragmentPadding;
            TextContainerInset = new UIEdgeInsets(16f, sideInset, 10f, sideInset);

            CreateBottomLineLayer();
            CreatePlaceholderLabel();
        }

        private void CreateBottomLineLayer()
        {
            bottomLineLayer = new CALayer();
            bottomLineLayer.BackgroundColor = Colors.Gray2.CGColor;

            Layer.AddSublayer(bottomLineLayer);
        }

        private void CreatePlaceholderLabel()
        {
            placeholderLabel = new UILabel
            {
                Font = Font,
                TextColor = Colors.Gray4
            };

            AddSubview(placeholderLabel);
        }

        private void UpdatePlaceholderLabelFrame()
        {
            if (IsEmpty)
            {
                Animate(() =>
                {
                    placeholderLabel.Font = Font;

                    var size = placeholderLabel.SizeThatFits(Bounds.Size);
                    placeholderLabel.UpdateFrame(y: TextContainerInset.Top, width: size.Width, height: size.Height);
                });
            }
            else
            {
                Animate(() =>
                {
                    placeholderLabel.Font = Font.WithSize(Font.PointSize * FontScale);

                    var size = placeholderLabel.SizeThatFits(Bounds.Size);
                    placeholderLabel.UpdateFrame(y: 0f, width: size.Width, height: size.Height);
                });
            }
        }

        private void Animate(Action action)
        {
            AnimateNotify(
                0.2d,
                0d,
                UIViewAnimationOptions.BeginFromCurrentState | UIViewAnimationOptions.CurveLinear,
                action,
                null);
        }
    }
}

using System;
using CoreAnimation;
using CoreGraphics;
using Foundation;
using SushiShop.Ios.Common;
using UIKit;

namespace SushiShop.Ios.Views.Controls
{
    [Register(nameof(FloatingTextField))]
    public class FloatingTextField : UITextField
    {
        private const float ScaleFloatingLabel = 0.8f;

        private CALayer bottomLine;
        private UILabel floatingLabel;
        private nfloat maxLineHeight;

        public override string Placeholder
        {
            get => floatingLabel.Text;
            set => SetFloatingLabelText(value);
        }

        public FloatingTextField()
        {
            Initialize();
        }

        public FloatingTextField(NSCoder coder) : base(coder)
        {
            Initialize();
        }

        public FloatingTextField(CGRect frame) : base(frame)
        {
            Initialize();
        }

        protected FloatingTextField(NSObjectFlag t) : base(t)
        {
            Initialize();
        }

        protected internal FloatingTextField(IntPtr handle) : base(handle)
        {
        }

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();
            Initialize();
        }

        public override CGRect TextRect(CGRect forBounds)
        {
            if (floatingLabel == null)
            {
                return base.TextRect(forBounds);
            }

            UpdateMaxLineHeight();
            return InsetRect(base.TextRect(forBounds), new UIEdgeInsets(maxLineHeight, 0, 0, 0));
        }

        public override CGRect EditingRect(CGRect forBounds)
        {
            if (floatingLabel == null)
            {
                return base.EditingRect(forBounds);
            }

            UpdateMaxLineHeight();
            return InsetRect(base.EditingRect(forBounds), new UIEdgeInsets(maxLineHeight, 0, 0, 0));
        }

        public override CGRect ClearButtonRect(CGRect forBounds)
        {
            var rect = base.ClearButtonRect(forBounds);

            if (floatingLabel == null)
            {
                return rect;
            }

            return new CGRect(
                rect.X,
                rect.Y + floatingLabel.Font.LineHeight / 2.0f,
                rect.Size.Width,
                rect.Size.Height);
        }

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();

            InitializeBottomLine();
            UpdatePlaceholer();
        }

        private void Initialize()
        {
            BorderStyle = UITextBorderStyle.None;
            bottomLine = new CALayer()
            {
                BackgroundColor = Colors.Gray2.CGColor
            };

            floatingLabel = new UILabel()
            {
                Font = Font,
                TextColor = Colors.Gray4
            };

            AddSubview(floatingLabel);
        }

        private void InitializeBottomLine()
        {
            bottomLine.RemoveFromSuperLayer();
            bottomLine.Frame = new CGRect(0, Frame.Height + 10, Frame.Width, 1);
            Layer.AddSublayer(bottomLine);
        }

        private void UpdatePlaceholer()
        {
            Animate(
                0.3f, 0.0f,
                UIViewAnimationOptions.BeginFromCurrentState | UIViewAnimationOptions.CurveEaseOut,
                () =>
                {
                    var translateX = 0f;
                    var translateY = 0f;
                    var scaleX = 1.0f;
                    var scaleY = 1.0f;
                    if (!string.IsNullOrEmpty(Text))
                    {
                        translateX = (float)-(floatingLabel.Frame.Width * (Text.Length == 1 ? ScaleFloatingLabel : 1f)) * 0.2f;
                        translateY = (float)-maxLineHeight;
                        scaleX = ScaleFloatingLabel;
                        scaleY = ScaleFloatingLabel;
                    }

                    floatingLabel.Transform = CGAffineTransform.Scale(CGAffineTransform.MakeTranslation(translateX, translateY), scaleX, scaleY);
                },
                () => { });
        }

        private CGRect InsetRect(CGRect rect, UIEdgeInsets insets) =>
            new CGRect(
                rect.X + insets.Left,
                rect.Y + insets.Top,
                rect.Width - insets.Left - insets.Right,
                rect.Height - insets.Top - insets.Bottom);

        private void UpdateMaxLineHeight()
        {
            maxLineHeight = (nfloat)Math.Max(maxLineHeight, floatingLabel.Font.LineHeight);
        }

        private void SetFloatingLabelText(string value)
        {
            floatingLabel.Text = value;
            floatingLabel.SizeToFit();
            floatingLabel.Frame = new CGRect(0, floatingLabel.Font.LineHeight, floatingLabel.Frame.Size.Width, floatingLabel.Frame.Size.Height);
        }
    }
}

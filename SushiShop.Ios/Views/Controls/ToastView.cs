using CoreFoundation;
using SushiShop.Core.Data.Enums;
using SushiShop.Ios.Common;
using System.Threading.Tasks;
using UIKit;

namespace SushiShop.Ios.Views.Controls
{
    public class ToastView : UIView
    {
        private readonly UILabel messageLabel;
        private readonly TaskCompletionSource<bool> taskCompletionSource;

        private UIImageView closeImageView;

        public ToastView(
            string message,
            bool isEndless,
            TaskCompletionSource<bool> taskCompletionSource)
        {
            this.taskCompletionSource = taskCompletionSource;

            BackgroundColor = Colors.Orange;

            messageLabel = new UILabel
            {
                TranslatesAutoresizingMaskIntoConstraints = false,
                Lines = 5,
                TextColor = UIColor.White,
                Font = Font.Create(FontStyle.Regular, 16),
                Text = message
            };

            if (!isEndless)
            {
                AddGestureRecognizer(new UISwipeGestureRecognizer(OnSwiped) { Direction = UISwipeGestureRecognizerDirection.Up });
                InitializeCloseImageView();
            }

            AddSubview(messageLabel);

            NSLayoutConstraint.ActivateConstraints(new[]
            {
                messageLabel.LeadingAnchor.ConstraintEqualTo(LeadingAnchor, 19f),
                messageLabel.TopAnchor.ConstraintEqualTo(TopAnchor, 21f),
                messageLabel.BottomAnchor.ConstraintEqualTo(BottomAnchor, -21f),
                messageLabel.TrailingAnchor.ConstraintEqualTo(TrailingAnchor, -66f),
            });
        }

        private void InitializeCloseImageView()
        {
            closeImageView = new UIImageView
            {
                TranslatesAutoresizingMaskIntoConstraints = false,
                Image = UIImage.FromBundle(ImageNames.CloseWhite),
                ContentMode = UIViewContentMode.Center,
                UserInteractionEnabled = true
            };

            closeImageView.AddGestureRecognizer(new UITapGestureRecognizer(CloseToast));
            AddSubview(closeImageView);

            NSLayoutConstraint.ActivateConstraints(new[]
            {
                closeImageView.TrailingAnchor.ConstraintEqualTo(TrailingAnchor, -8f),
                closeImageView.CenterYAnchor.ConstraintEqualTo(CenterYAnchor),
                closeImageView.HeightAnchor.ConstraintEqualTo(54f),
                closeImageView.WidthAnchor.ConstraintEqualTo(54f),
            });
        }

        private void OnSwiped()
        {
            CloseToast();
        }

        public void CloseToast()
        {
            DispatchQueue.MainQueue.DispatchAsync(RemoveFromSuperview);
            taskCompletionSource?.TrySetResult(false);
        }
    }
}

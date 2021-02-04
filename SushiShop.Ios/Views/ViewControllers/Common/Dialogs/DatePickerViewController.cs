using Acr.UserDialogs;
using SushiShop.Core.Data.Enums;
using SushiShop.Core.Resources;
using SushiShop.Ios.Common;
using SushiShop.Ios.Extensions;
using System;
using UIKit;

namespace SushiShop.Ios.Views.ViewControllers.Common.Dialogs
{
    public class DatePickerViewController : UIViewController
    {
        public DatePickerViewController(
            Action<DateTime?> completeAction,
            DateTime initialDate,
            DateTime? minDate,
            DateTime? maxDate,
            DatePickerMode mode)
        {
            var picker = ProduceDatePicker(initialDate, minDate, maxDate, mode);
            var button = ProduceDoneButton();
            button.AddGestureRecognizer(new UITapGestureRecognizer(() => completeAction?.Invoke(picker.Date.ToDateTime())));

            var pickerContainer = new UIView
            {
                TranslatesAutoresizingMaskIntoConstraints = false,
                BackgroundColor = Colors.White
            };

            pickerContainer.Layer.CornerRadius = 15;

            pickerContainer.AddSubview(picker);
            View.AddSubviews(button, pickerContainer);
            View.BackgroundColor = Colors.RealBlack.ColorWithAlpha(0.3f);

            NSLayoutConstraint.ActivateConstraints(new[]
            {
                button.BottomAnchor.ConstraintEqualTo(View.SafeAreaLayoutGuide.BottomAnchor, -8),
                button.LeadingAnchor.ConstraintEqualTo(View.LeadingAnchor, 10),
                button.TrailingAnchor.ConstraintEqualTo(View.TrailingAnchor, -10),
                button.HeightAnchor.ConstraintEqualTo(56),

                pickerContainer.BottomAnchor.ConstraintEqualTo(button.TopAnchor, -8),
                pickerContainer.LeadingAnchor.ConstraintEqualTo(View.LeadingAnchor, 10),
                pickerContainer.TrailingAnchor.ConstraintEqualTo(View.TrailingAnchor, -10),
                pickerContainer.HeightAnchor.ConstraintEqualTo(300),

                picker.BottomAnchor.ConstraintEqualTo(pickerContainer.BottomAnchor),
                picker.LeadingAnchor.ConstraintEqualTo(pickerContainer.LeadingAnchor),
                picker.TrailingAnchor.ConstraintEqualTo(pickerContainer.TrailingAnchor),
                picker.TopAnchor.ConstraintEqualTo(pickerContainer.TopAnchor),
            });
        }

        private UIDatePicker ProduceDatePicker(
            DateTime initialDate,
            DateTime? minDate,
            DateTime? maxDate,
            DatePickerMode mode)
        {
            var datePicker = new UIDatePicker
            {
                Date = initialDate.Date.ToNSDate(),
                Mode = mode.ToUIDatePickerMode(),
                MaximumDate = maxDate?.ToNSDate(),
                MinimumDate = minDate?.ToNSDate(),
                BackgroundColor = Colors.White,
                UserInteractionEnabled = true,
                PreferredDatePickerStyle = UIDatePickerStyle.Wheels,
                TranslatesAutoresizingMaskIntoConstraints = false
            };

            return datePicker;
        }

        private UIButton ProduceDoneButton()
        {
            var button = new UIButton
            {
                TranslatesAutoresizingMaskIntoConstraints = false,
                BackgroundColor = Colors.White,
                Font = Font.Create(FontStyle.Bold, 16),
            };

            button.SetTitle(AppStrings.Done, UIControlState.Normal);
            button.SetTitleColor(Colors.Blue, UIControlState.Normal);
            button.Layer.CornerRadius = 15;
            return button;
        }
    }
}
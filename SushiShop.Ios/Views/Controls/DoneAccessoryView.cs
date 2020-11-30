using System;
using SushiShop.Core.Resources;
using SushiShop.Ios.Common;
using UIKit;

namespace SushiShop.Ios.Views.Controls
{
    public class DoneAccessoryView : UIToolbar
    {
        private readonly UIView parentView;
        private readonly Action tapAction;

        private UIButton doneButton;

        public DoneAccessoryView(UIView parentView, Action tapAction)
        {
            InitView();
            this.parentView = parentView;
            this.tapAction = tapAction;
        }

        public bool CanCancelEditing { get; set; } = true;

        private void InitView()
        {
            BarTintColor = Colors.Orange2;

            TranslatesAutoresizingMaskIntoConstraints = false;
            doneButton = new UIButton { TranslatesAutoresizingMaskIntoConstraints = false };
            doneButton.Font = Font.Create(Core.Data.Enums.FontStyle.Regular, 17);
            doneButton.SetTitle(AppStrings.Done, UIControlState.Normal);
            doneButton.SetTitleColor(Colors.White, UIControlState.Normal);

            var flexibleSpace = new UIBarButtonItem(UIBarButtonSystemItem.FlexibleSpace);
            SetItems(new[] { flexibleSpace, new UIBarButtonItem(doneButton) }, true);

            doneButton.AddGestureRecognizer(new UITapGestureRecognizer(() =>
            {
                tapAction?.Invoke();
                parentView.EndEditing(true);
            }));
        }
    }
}

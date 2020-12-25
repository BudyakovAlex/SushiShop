// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace SushiShop.Ios.Views.ViewControllers.Promotions
{
	[Register ("PromotionDetailsViewController")]
	partial class PromotionDetailsViewController
	{
		[Outlet]
		UIKit.UIButton BackButton { get; set; }

		[Outlet]
		UIKit.UITextView ContentTextView { get; set; }

		[Outlet]
		UIKit.NSLayoutConstraint ContentTextViewBottomConstraint { get; set; }

		[Outlet]
		UIKit.NSLayoutConstraint ContentTextViewHeightConstraint { get; set; }

		[Outlet]
		UIKit.UILabel DateLabel { get; set; }

		[Outlet]
		UIKit.NSLayoutConstraint DateLabelBottomConstraint { get; set; }

		[Outlet]
		FFImageLoading.Cross.MvxCachedImageView ImageView { get; set; }

		[Outlet]
		UIKit.NSLayoutConstraint ImageViewAspectRationConstraint { get; set; }

		[Outlet]
		UIKit.UIView LoadingView { get; set; }

		[Outlet]
		UIKit.UILabel PageTitleLabel { get; set; }

		[Outlet]
		SushiShop.Ios.Views.Controls.Stepper.BigStepperView StepperView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (BackButton != null) {
				BackButton.Dispose ();
				BackButton = null;
			}

			if (ContentTextView != null) {
				ContentTextView.Dispose ();
				ContentTextView = null;
			}

			if (ContentTextViewBottomConstraint != null) {
				ContentTextViewBottomConstraint.Dispose ();
				ContentTextViewBottomConstraint = null;
			}

			if (ContentTextViewHeightConstraint != null) {
				ContentTextViewHeightConstraint.Dispose ();
				ContentTextViewHeightConstraint = null;
			}

			if (DateLabel != null) {
				DateLabel.Dispose ();
				DateLabel = null;
			}

			if (DateLabelBottomConstraint != null) {
				DateLabelBottomConstraint.Dispose ();
				DateLabelBottomConstraint = null;
			}

			if (ImageView != null) {
				ImageView.Dispose ();
				ImageView = null;
			}

			if (ImageViewAspectRationConstraint != null) {
				ImageViewAspectRationConstraint.Dispose ();
				ImageViewAspectRationConstraint = null;
			}

			if (LoadingView != null) {
				LoadingView.Dispose ();
				LoadingView = null;
			}

			if (PageTitleLabel != null) {
				PageTitleLabel.Dispose ();
				PageTitleLabel = null;
			}

			if (StepperView != null) {
				StepperView.Dispose ();
				StepperView = null;
			}
		}
	}
}

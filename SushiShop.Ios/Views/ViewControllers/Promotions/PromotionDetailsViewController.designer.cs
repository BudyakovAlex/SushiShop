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
		SushiShop.Ios.Views.Controls.ResizableWebView ContentWebView { get; set; }

		[Outlet]
		UIKit.UILabel DateLabel { get; set; }

		[Outlet]
		SushiShop.Ios.Views.Controls.ScalableImageView ImageView { get; set; }

		[Outlet]
		UIKit.UILabel IntroLabel { get; set; }

		[Outlet]
		UIKit.UIView LoadingView { get; set; }

		[Outlet]
		SushiShop.Ios.Views.Controls.Stepper.BigStepperView StepperView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (ContentWebView != null) {
				ContentWebView.Dispose ();
				ContentWebView = null;
			}

			if (BackButton != null) {
				BackButton.Dispose ();
				BackButton = null;
			}

			if (DateLabel != null) {
				DateLabel.Dispose ();
				DateLabel = null;
			}

			if (ImageView != null) {
				ImageView.Dispose ();
				ImageView = null;
			}

			if (IntroLabel != null) {
				IntroLabel.Dispose ();
				IntroLabel = null;
			}

			if (LoadingView != null) {
				LoadingView.Dispose ();
				LoadingView = null;
			}

			if (StepperView != null) {
				StepperView.Dispose ();
				StepperView = null;
			}
		}
	}
}

// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace SushiShop.Ios.Views.ViewControllers.Profile
{
	[Register ("ProfileViewController")]
	partial class ProfileViewController
	{
		[Outlet]
		SushiShop.Ios.Views.Controls.FloatingTextField EmailLoginTextField { get; set; }

		[Outlet]
		UIKit.UIView FeedbackView { get; set; }

		[Outlet]
		UIKit.UIActivityIndicatorView LoadingActivityIndicator { get; set; }

		[Outlet]
		SushiShop.Ios.Views.Controls.PrimaryButton LoginButton { get; set; }

		[Outlet]
		UIKit.UIView LoginView { get; set; }

		[Outlet]
		UIKit.UIView MyOrdersView { get; set; }

		[Outlet]
		UIKit.UIView PersonalDataView { get; set; }

		[Outlet]
		UIKit.UIView ProfileView { get; set; }

		[Outlet]
		UIKit.UIButton RegisterButton { get; set; }

		[Outlet]
		SushiShop.Ios.Views.Controls.PrimaryButton ScoreButton { get; set; }

		[Outlet]
		FFImageLoading.Cross.MvxCachedImageView UserImage { get; set; }

		[Outlet]
		UIKit.UILabel UserNameLabel { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (EmailLoginTextField != null) {
				EmailLoginTextField.Dispose ();
				EmailLoginTextField = null;
			}

			if (FeedbackView != null) {
				FeedbackView.Dispose ();
				FeedbackView = null;
			}

			if (LoginButton != null) {
				LoginButton.Dispose ();
				LoginButton = null;
			}

			if (LoginView != null) {
				LoginView.Dispose ();
				LoginView = null;
			}

			if (MyOrdersView != null) {
				MyOrdersView.Dispose ();
				MyOrdersView = null;
			}

			if (PersonalDataView != null) {
				PersonalDataView.Dispose ();
				PersonalDataView = null;
			}

			if (ProfileView != null) {
				ProfileView.Dispose ();
				ProfileView = null;
			}

			if (RegisterButton != null) {
				RegisterButton.Dispose ();
				RegisterButton = null;
			}

			if (ScoreButton != null) {
				ScoreButton.Dispose ();
				ScoreButton = null;
			}

			if (UserImage != null) {
				UserImage.Dispose ();
				UserImage = null;
			}

			if (UserNameLabel != null) {
				UserNameLabel.Dispose ();
				UserNameLabel = null;
			}

			if (LoadingActivityIndicator != null) {
				LoadingActivityIndicator.Dispose ();
				LoadingActivityIndicator = null;
			}
		}
	}
}

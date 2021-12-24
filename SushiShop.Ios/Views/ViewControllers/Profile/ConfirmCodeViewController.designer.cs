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
	[Register ("ConfirmCodeViewController")]
	partial class ConfirmCodeViewController
	{
		[Outlet]
		SushiShop.Ios.Views.Controls.FloatingTextField CodeTextField { get; set; }

		[Outlet]
		UIKit.UILabel ConfirmationMessageLabel { get; set; }

		[Outlet]
		UIKit.UIActivityIndicatorView LoadingActivityIndicator { get; set; }

		[Outlet]
		UIKit.UILabel MessageToReceiveNewCodeLabel { get; set; }

		[Outlet]
		SushiShop.Ios.Views.Controls.PrimaryButton SendNewCodeButton { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (CodeTextField != null) {
				CodeTextField.Dispose ();
				CodeTextField = null;
			}

			if (ConfirmationMessageLabel != null) {
				ConfirmationMessageLabel.Dispose ();
				ConfirmationMessageLabel = null;
			}

			if (LoadingActivityIndicator != null) {
				LoadingActivityIndicator.Dispose ();
				LoadingActivityIndicator = null;
			}

			if (MessageToReceiveNewCodeLabel != null) {
				MessageToReceiveNewCodeLabel.Dispose ();
				MessageToReceiveNewCodeLabel = null;
			}

			if (SendNewCodeButton != null) {
				SendNewCodeButton.Dispose ();
				SendNewCodeButton = null;
			}
		}
	}
}

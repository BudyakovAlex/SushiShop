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
		SushiShop.Ios.Views.Controls.PrimaryButton ContinueButton { get; set; }

		[Outlet]
		UIKit.UIActivityIndicatorView LoadingActivityIndicator { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (CodeTextField != null) {
				CodeTextField.Dispose ();
				CodeTextField = null;
			}

			if (ContinueButton != null) {
				ContinueButton.Dispose ();
				ContinueButton = null;
			}

			if (LoadingActivityIndicator != null) {
				LoadingActivityIndicator.Dispose ();
				LoadingActivityIndicator = null;
			}
		}
	}
}

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
	[Register ("EditProfileViewController")]
	partial class EditProfileViewController
	{
		[Outlet]
		SushiShop.Ios.Views.Controls.FloatingTextField DateOfBirdthtextField { get; set; }

		[Outlet]
		SushiShop.Ios.Views.Controls.FloatingTextField EmailTextField { get; set; }

		[Outlet]
		SushiShop.Ios.Views.Controls.FloatingTextField GenderTextField { get; set; }

		[Outlet]
		SushiShop.Ios.Views.Controls.FloatingTextField NametextField { get; set; }

		[Outlet]
		UIKit.UISwitch IsAllowPushSwitch { get; set; }

		[Outlet]
		UIKit.UISwitch IsAllowNotificationsSwitch { get; set; }

		[Outlet]
		SushiShop.Ios.Views.Controls.FloatingTextField PhoneTextField { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (NametextField != null) {
				NametextField.Dispose ();
				NametextField = null;
			}

			if (GenderTextField != null) {
				GenderTextField.Dispose ();
				GenderTextField = null;
			}

			if (DateOfBirdthtextField != null) {
				DateOfBirdthtextField.Dispose ();
				DateOfBirdthtextField = null;
			}

			if (PhoneTextField != null) {
				PhoneTextField.Dispose ();
				PhoneTextField = null;
			}

			if (EmailTextField != null) {
				EmailTextField.Dispose ();
				EmailTextField = null;
			}

			if (IsAllowNotificationsSwitch != null) {
				IsAllowNotificationsSwitch.Dispose ();
				IsAllowNotificationsSwitch = null;
			}

			if (IsAllowPushSwitch != null) {
				IsAllowPushSwitch.Dispose ();
				IsAllowPushSwitch = null;
			}
		}
	}
}
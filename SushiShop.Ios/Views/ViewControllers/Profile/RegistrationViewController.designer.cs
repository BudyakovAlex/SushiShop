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
	[Register ("RegistrationViewController")]
	partial class RegistrationViewController
	{
		[Outlet]
		UIKit.UILabel AcceptDescriptionLabel { get; set; }

		[Outlet]
		SushiShop.Ios.Views.Controls.FloatingTextField DateOfBirthTextField { get; set; }

		[Outlet]
		UIKit.UISwitch EmailNotificationsSwitch { get; set; }

		[Outlet]
		SushiShop.Ios.Views.Controls.FloatingTextField EmailTextField { get; set; }

		[Outlet]
		SushiShop.Ios.Views.Controls.FloatingTextField NameTextField { get; set; }

		[Outlet]
		SushiShop.Ios.Views.Controls.FloatingTextField PhoneTextField { get; set; }

		[Outlet]
		UIKit.UISwitch PushNotificationsSwitch { get; set; }

		[Outlet]
		SushiShop.Ios.Views.Controls.PrimaryButton RegisterButton { get; set; }

		[Outlet]
		UIKit.UISwitch SmsNotificationsSwitch { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (NameTextField != null) {
				NameTextField.Dispose ();
				NameTextField = null;
			}

			if (DateOfBirthTextField != null) {
				DateOfBirthTextField.Dispose ();
				DateOfBirthTextField = null;
			}

			if (PhoneTextField != null) {
				PhoneTextField.Dispose ();
				PhoneTextField = null;
			}

			if (EmailTextField != null) {
				EmailTextField.Dispose ();
				EmailTextField = null;
			}

			if (PushNotificationsSwitch != null) {
				PushNotificationsSwitch.Dispose ();
				PushNotificationsSwitch = null;
			}

			if (EmailNotificationsSwitch != null) {
				EmailNotificationsSwitch.Dispose ();
				EmailNotificationsSwitch = null;
			}

			if (SmsNotificationsSwitch != null) {
				SmsNotificationsSwitch.Dispose ();
				SmsNotificationsSwitch = null;
			}

			if (AcceptDescriptionLabel != null) {
				AcceptDescriptionLabel.Dispose ();
				AcceptDescriptionLabel = null;
			}

			if (RegisterButton != null) {
				RegisterButton.Dispose ();
				RegisterButton = null;
			}
		}
	}
}

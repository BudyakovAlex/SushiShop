// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace SushiShop.Ios.Views.ViewControllers.Orders
{
	[Register ("OrderRegistrationViewController")]
	partial class OrderRegistrationViewController
	{
		[Outlet]
		UIKit.UILabel ChoiceAddressLabel { get; set; }

		[Outlet]
		UIKit.UIView ChoiceAddressView { get; set; }

		[Outlet]
		UIKit.UILabel DeliveryAddressLabel { get; set; }

		[Outlet]
		SushiShop.Ios.Views.Controls.FloatingTextField DeliveryAppartmentTextField { get; set; }

		[Outlet]
		SushiShop.Ios.Views.Controls.FloatingTextField DeliveryEntranceTextField { get; set; }

		[Outlet]
		SushiShop.Ios.Views.Controls.FloatingTextField DeliveryFloorTextField { get; set; }

		[Outlet]
		SushiShop.Ios.Views.Controls.FloatingTextField DeliveryIntercomTextField { get; set; }

		[Outlet]
		UIKit.UILabel DeliveryPriceLabel { get; set; }

		[Outlet]
		UIKit.UIView DeliveryView { get; set; }

		[Outlet]
		UIKit.UILabel PickUpAddressLabel { get; set; }

		[Outlet]
		UIKit.UILabel PickUpPhoneLabel { get; set; }

		[Outlet]
		UIKit.UILabel PickUpTimeWorkingLabel { get; set; }

		[Outlet]
		SushiShop.Ios.Views.Controls.ScrollableTabView scrollableTabsView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (ChoiceAddressLabel != null) {
				ChoiceAddressLabel.Dispose ();
				ChoiceAddressLabel = null;
			}

			if (ChoiceAddressView != null) {
				ChoiceAddressView.Dispose ();
				ChoiceAddressView = null;
			}

			if (DeliveryView != null) {
				DeliveryView.Dispose ();
				DeliveryView = null;
			}

			if (scrollableTabsView != null) {
				scrollableTabsView.Dispose ();
				scrollableTabsView = null;
			}

			if (PickUpAddressLabel != null) {
				PickUpAddressLabel.Dispose ();
				PickUpAddressLabel = null;
			}

			if (PickUpPhoneLabel != null) {
				PickUpPhoneLabel.Dispose ();
				PickUpPhoneLabel = null;
			}

			if (PickUpTimeWorkingLabel != null) {
				PickUpTimeWorkingLabel.Dispose ();
				PickUpTimeWorkingLabel = null;
			}

			if (DeliveryAddressLabel != null) {
				DeliveryAddressLabel.Dispose ();
				DeliveryAddressLabel = null;
			}

			if (DeliveryPriceLabel != null) {
				DeliveryPriceLabel.Dispose ();
				DeliveryPriceLabel = null;
			}

			if (DeliveryAppartmentTextField != null) {
				DeliveryAppartmentTextField.Dispose ();
				DeliveryAppartmentTextField = null;
			}

			if (DeliveryEntranceTextField != null) {
				DeliveryEntranceTextField.Dispose ();
				DeliveryEntranceTextField = null;
			}

			if (DeliveryIntercomTextField != null) {
				DeliveryIntercomTextField.Dispose ();
				DeliveryIntercomTextField = null;
			}

			if (DeliveryFloorTextField != null) {
				DeliveryFloorTextField.Dispose ();
				DeliveryFloorTextField = null;
			}
		}
	}
}

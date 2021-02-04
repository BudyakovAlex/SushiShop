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
	[Register ("SelectOrderDeliveryAddressViewController")]
	partial class SelectOrderDeliveryAddressViewController
	{
		[Outlet]
		UIKit.UIView AboutPointView { get; set; }

		[Outlet]
		UIKit.UILabel AddressSelectedPointLabel { get; set; }

		[Outlet]
		UIKit.UILabel BadLocationLabel { get; set; }

		[Outlet]
		UIKit.UILabel DeliveryPriceLabel { get; set; }

		[Outlet]
		SushiShop.Ios.Views.Controls.PrimaryButton DeliveryThereButton { get; set; }

		[Outlet]
		UIKit.UIView MapViewContainer { get; set; }

		[Outlet]
		SushiShop.Ios.Views.Controls.SelfSizeTableView ResultsTableView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (AboutPointView != null) {
				AboutPointView.Dispose ();
				AboutPointView = null;
			}

			if (AddressSelectedPointLabel != null) {
				AddressSelectedPointLabel.Dispose ();
				AddressSelectedPointLabel = null;
			}

			if (BadLocationLabel != null) {
				BadLocationLabel.Dispose ();
				BadLocationLabel = null;
			}

			if (DeliveryPriceLabel != null) {
				DeliveryPriceLabel.Dispose ();
				DeliveryPriceLabel = null;
			}

			if (DeliveryThereButton != null) {
				DeliveryThereButton.Dispose ();
				DeliveryThereButton = null;
			}

			if (MapViewContainer != null) {
				MapViewContainer.Dispose ();
				MapViewContainer = null;
			}
		}
	}
}

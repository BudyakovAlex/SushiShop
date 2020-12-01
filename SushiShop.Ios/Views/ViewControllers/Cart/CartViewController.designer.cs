// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace SushiShop.Ios.Views.ViewControllers.Cart
{
	[Register ("CartViewController")]
	partial class CartViewController
	{
		[Outlet]
		UIKit.UIView AddSauseView { get; set; }

		[Outlet]
		UIKit.UIView BottomCheckoutView { get; set; }

		[Outlet]
		SushiShop.Ios.Views.Controls.PrimaryButton CheckoutButton { get; set; }

		[Outlet]
		UIKit.UIScrollView ContentScrollView { get; set; }

		[Outlet]
		SushiShop.Ios.Views.Controls.PrimaryButton GoToMenuButton { get; set; }

		[Outlet]
		SushiShop.Ios.Views.Controls.SelfSizeTableView PackagesTableView { get; set; }

		[Outlet]
		UIKit.UILabel PriceLabel { get; set; }

		[Outlet]
		SushiShop.Ios.Views.Controls.SelfSizeTableView ProductsTableView { get; set; }

		[Outlet]
		SushiShop.Ios.Views.Controls.FloatingTextField PromocodeTextField { get; set; }

		[Outlet]
		SushiShop.Ios.Views.Controls.SelfSizeTableView ToppingsTableView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (AddSauseView != null) {
				AddSauseView.Dispose ();
				AddSauseView = null;
			}

			if (CheckoutButton != null) {
				CheckoutButton.Dispose ();
				CheckoutButton = null;
			}

			if (ContentScrollView != null) {
				ContentScrollView.Dispose ();
				ContentScrollView = null;
			}

			if (PackagesTableView != null) {
				PackagesTableView.Dispose ();
				PackagesTableView = null;
			}

			if (PriceLabel != null) {
				PriceLabel.Dispose ();
				PriceLabel = null;
			}

			if (ProductsTableView != null) {
				ProductsTableView.Dispose ();
				ProductsTableView = null;
			}

			if (PromocodeTextField != null) {
				PromocodeTextField.Dispose ();
				PromocodeTextField = null;
			}

			if (ToppingsTableView != null) {
				ToppingsTableView.Dispose ();
				ToppingsTableView = null;
			}

			if (BottomCheckoutView != null) {
				BottomCheckoutView.Dispose ();
				BottomCheckoutView = null;
			}

			if (GoToMenuButton != null) {
				GoToMenuButton.Dispose ();
				GoToMenuButton = null;
			}
		}
	}
}

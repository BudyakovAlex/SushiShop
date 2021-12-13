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
		UIKit.UILabel AddPackagesTitleLabel { get; set; }

		[Outlet]
		UIKit.UIView AddSauseContainerView { get; set; }

		[Outlet]
		UIKit.UIView AddSauseView { get; set; }

		[Outlet]
		UIKit.UILabel ApplyedPromocodePriceLabel { get; set; }

		[Outlet]
		UIKit.UILabel ApplyedPromocodeTitle { get; set; }

		[Outlet]
		UIKit.UIView ApplyedPromocodeView { get; set; }

		[Outlet]
		SushiShop.Ios.Views.Controls.PrimaryButton ApplyPromocodeButton { get; set; }

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
		UIKit.UIView SelectPackageTitleContainerView { get; set; }

		[Outlet]
		SushiShop.Ios.Views.Controls.SelfSizeTableView ToppingsTableView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (ApplyPromocodeButton != null) {
				ApplyPromocodeButton.Dispose ();
				ApplyPromocodeButton = null;
			}

			if (AddPackagesTitleLabel != null) {
				AddPackagesTitleLabel.Dispose ();
				AddPackagesTitleLabel = null;
			}

			if (AddSauseContainerView != null) {
				AddSauseContainerView.Dispose ();
				AddSauseContainerView = null;
			}

			if (AddSauseView != null) {
				AddSauseView.Dispose ();
				AddSauseView = null;
			}

			if (ApplyedPromocodePriceLabel != null) {
				ApplyedPromocodePriceLabel.Dispose ();
				ApplyedPromocodePriceLabel = null;
			}

			if (ApplyedPromocodeTitle != null) {
				ApplyedPromocodeTitle.Dispose ();
				ApplyedPromocodeTitle = null;
			}

			if (ApplyedPromocodeView != null) {
				ApplyedPromocodeView.Dispose ();
				ApplyedPromocodeView = null;
			}

			if (BottomCheckoutView != null) {
				BottomCheckoutView.Dispose ();
				BottomCheckoutView = null;
			}

			if (CheckoutButton != null) {
				CheckoutButton.Dispose ();
				CheckoutButton = null;
			}

			if (ContentScrollView != null) {
				ContentScrollView.Dispose ();
				ContentScrollView = null;
			}

			if (GoToMenuButton != null) {
				GoToMenuButton.Dispose ();
				GoToMenuButton = null;
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

			if (SelectPackageTitleContainerView != null) {
				SelectPackageTitleContainerView.Dispose ();
				SelectPackageTitleContainerView = null;
			}

			if (ToppingsTableView != null) {
				ToppingsTableView.Dispose ();
				ToppingsTableView = null;
			}
		}
	}
}

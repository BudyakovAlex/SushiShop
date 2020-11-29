// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace SushiShop.Ios.Views.Cells.Cart
{
	[Register ("CartProductViewCell")]
	partial class CartProductViewCell
	{
		[Outlet]
		SushiShop.Ios.Views.Controls.Stepper.SmallStepperView CountStepperView { get; set; }

		[Outlet]
		UIKit.UILabel OldPriceLabel { get; set; }

		[Outlet]
		UIKit.UILabel PriceLabel { get; set; }

		[Outlet]
		FFImageLoading.Cross.MvxCachedImageView ProductImage { get; set; }

		[Outlet]
		UIKit.UILabel TitleLabel { get; set; }

		[Outlet]
		BuildApps.Core.Mobile.MvvmCross.UIKit.Views.Controls.MvxStackView ToppingsStackView { get; set; }

		[Outlet]
		UIKit.UILabel WeightLabel { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (CountStepperView != null) {
				CountStepperView.Dispose ();
				CountStepperView = null;
			}

			if (OldPriceLabel != null) {
				OldPriceLabel.Dispose ();
				OldPriceLabel = null;
			}

			if (PriceLabel != null) {
				PriceLabel.Dispose ();
				PriceLabel = null;
			}

			if (ProductImage != null) {
				ProductImage.Dispose ();
				ProductImage = null;
			}

			if (TitleLabel != null) {
				TitleLabel.Dispose ();
				TitleLabel = null;
			}

			if (WeightLabel != null) {
				WeightLabel.Dispose ();
				WeightLabel = null;
			}

			if (ToppingsStackView != null) {
				ToppingsStackView.Dispose ();
				ToppingsStackView = null;
			}
		}
	}
}

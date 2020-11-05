// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace SushiShop.Ios.Views.ViewControllers.ProductDetails
{
	[Register ("ProductDetailsViewController")]
	partial class ProductDetailsViewController
	{
		[Outlet]
		UIKit.UIActivityIndicatorView ActivityIndicator { get; set; }

		[Outlet]
		UIKit.UIButton AddToCartButton { get; set; }

		[Outlet]
		UIKit.UIButton BackButton { get; set; }

		[Outlet]
		UIKit.UICollectionView BuyAnotherCollectionView { get; set; }

		[Outlet]
		UIKit.UILabel CaloriesValueLabel { get; set; }

		[Outlet]
		UIKit.UILabel CarbohydratesValueLabel { get; set; }

		[Outlet]
		UIKit.UILabel FatsValueLabel { get; set; }

		[Outlet]
		UIKit.UILabel OldPriceLabel { get; set; }

		[Outlet]
		UIKit.UILabel PriceLabel { get; set; }

		[Outlet]
		UIKit.UILabel ProductDescriptionLabel { get; set; }

		[Outlet]
		FFImageLoading.Cross.MvxCachedImageView ProductImageView { get; set; }

		[Outlet]
		UIKit.UILabel ProductNameLabel { get; set; }

		[Outlet]
		UIKit.UIView ProductSpecificationsView { get; set; }

		[Outlet]
		UIKit.UILabel ProteinsValueLabel { get; set; }

		[Outlet]
		SushiShop.Ios.Views.Controls.Stepper.BigStepperView StepperView { get; set; }

		[Outlet]
		UIKit.UILabel WeightLabel { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (AddToCartButton != null) {
				AddToCartButton.Dispose ();
				AddToCartButton = null;
			}

			if (BackButton != null) {
				BackButton.Dispose ();
				BackButton = null;
			}

			if (BuyAnotherCollectionView != null) {
				BuyAnotherCollectionView.Dispose ();
				BuyAnotherCollectionView = null;
			}

			if (CaloriesValueLabel != null) {
				CaloriesValueLabel.Dispose ();
				CaloriesValueLabel = null;
			}

			if (CarbohydratesValueLabel != null) {
				CarbohydratesValueLabel.Dispose ();
				CarbohydratesValueLabel = null;
			}

			if (FatsValueLabel != null) {
				FatsValueLabel.Dispose ();
				FatsValueLabel = null;
			}

			if (OldPriceLabel != null) {
				OldPriceLabel.Dispose ();
				OldPriceLabel = null;
			}

			if (PriceLabel != null) {
				PriceLabel.Dispose ();
				PriceLabel = null;
			}

			if (ProductDescriptionLabel != null) {
				ProductDescriptionLabel.Dispose ();
				ProductDescriptionLabel = null;
			}

			if (ProductImageView != null) {
				ProductImageView.Dispose ();
				ProductImageView = null;
			}

			if (ProductNameLabel != null) {
				ProductNameLabel.Dispose ();
				ProductNameLabel = null;
			}

			if (ProductSpecificationsView != null) {
				ProductSpecificationsView.Dispose ();
				ProductSpecificationsView = null;
			}

			if (ProteinsValueLabel != null) {
				ProteinsValueLabel.Dispose ();
				ProteinsValueLabel = null;
			}

			if (StepperView != null) {
				StepperView.Dispose ();
				StepperView = null;
			}

			if (WeightLabel != null) {
				WeightLabel.Dispose ();
				WeightLabel = null;
			}

			if (ActivityIndicator != null) {
				ActivityIndicator.Dispose ();
				ActivityIndicator = null;
			}
		}
	}
}

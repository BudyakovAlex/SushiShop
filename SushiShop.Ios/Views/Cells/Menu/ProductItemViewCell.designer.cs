// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace SushiShop.Ios.Views.Cells.Menu
{
	[Register ("ProductItemViewCell")]
	partial class ProductItemViewCell
	{
		[Outlet]
		UIKit.UILabel OldPriceLabel { get; set; }

		[Outlet]
		UIKit.UILabel PriceLabel { get; set; }

		[Outlet]
		SushiShop.Ios.Views.Controls.Stepper.SmallStepperView StepperView { get; set; }

		[Outlet]
		UIKit.UIStackView StickerStackView { get; set; }

		[Outlet]
		UIKit.UILabel TitleLabel { get; set; }

		[Outlet]
		SushiShop.Ios.Views.Controls.ScalableImageView TopImageView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (OldPriceLabel != null) {
				OldPriceLabel.Dispose ();
				OldPriceLabel = null;
			}

			if (PriceLabel != null) {
				PriceLabel.Dispose ();
				PriceLabel = null;
			}

			if (StepperView != null) {
				StepperView.Dispose ();
				StepperView = null;
			}

			if (StickerStackView != null) {
				StickerStackView.Dispose ();
				StickerStackView = null;
			}

			if (TitleLabel != null) {
				TitleLabel.Dispose ();
				TitleLabel = null;
			}

			if (TopImageView != null) {
				TopImageView.Dispose ();
				TopImageView = null;
			}
		}
	}
}

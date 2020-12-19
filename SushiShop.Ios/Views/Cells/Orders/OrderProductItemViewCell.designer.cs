// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace SushiShop.Ios.Views.Cells.Orders
{
	[Register ("OrderProductItemViewCell")]
	partial class OrderProductItemViewCell
	{
		[Outlet]
		UIKit.UILabel CountLabel { get; set; }

		[Outlet]
		UIKit.UILabel OldPriceLabel { get; set; }

		[Outlet]
		UIKit.UILabel PriceLabel { get; set; }

		[Outlet]
		SushiShop.Ios.Views.Controls.ScalableImageView ProductImageView { get; set; }

		[Outlet]
		UIKit.UILabel TitleLabel { get; set; }

		[Outlet]
		UIKit.UILabel ValueLabel { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (ProductImageView != null) {
				ProductImageView.Dispose ();
				ProductImageView = null;
			}

			if (TitleLabel != null) {
				TitleLabel.Dispose ();
				TitleLabel = null;
			}

			if (OldPriceLabel != null) {
				OldPriceLabel.Dispose ();
				OldPriceLabel = null;
			}

			if (PriceLabel != null) {
				PriceLabel.Dispose ();
				PriceLabel = null;
			}

			if (ValueLabel != null) {
				ValueLabel.Dispose ();
				ValueLabel = null;
			}

			if (CountLabel != null) {
				CountLabel.Dispose ();
				CountLabel = null;
			}
		}
	}
}

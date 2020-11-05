// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace SushiShop.Ios.Views.Cells.Products
{
	[Register ("ToppingItemCell")]
	partial class ToppingItemCell
	{
		[Outlet]
		UIKit.UILabel PriceLabel { get; set; }

		[Outlet]
		SushiShop.Ios.Views.Controls.Stepper.SmallStepperView StepperView { get; set; }

		[Outlet]
		UIKit.UILabel TitleLabel { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (TitleLabel != null) {
				TitleLabel.Dispose ();
				TitleLabel = null;
			}

			if (PriceLabel != null) {
				PriceLabel.Dispose ();
				PriceLabel = null;
			}

			if (StepperView != null) {
				StepperView.Dispose ();
				StepperView = null;
			}
		}
	}
}

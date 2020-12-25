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
	[Register ("OrderItemViewCell")]
	partial class OrderItemViewCell
	{
		[Outlet]
		UIKit.UILabel DateLabel { get; set; }

		[Outlet]
		UIKit.UILabel NumberLabel { get; set; }

		[Outlet]
		UIKit.UILabel ReceiveMethodLabel { get; set; }

		[Outlet]
		SushiShop.Ios.Views.Controls.PrimaryButton RepeatButton { get; set; }

		[Outlet]
		UIKit.UILabel StatusLabel { get; set; }

		[Outlet]
		UIKit.UILabel TotalPriceLabel { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (DateLabel != null) {
				DateLabel.Dispose ();
				DateLabel = null;
			}

			if (NumberLabel != null) {
				NumberLabel.Dispose ();
				NumberLabel = null;
			}

			if (StatusLabel != null) {
				StatusLabel.Dispose ();
				StatusLabel = null;
			}

			if (ReceiveMethodLabel != null) {
				ReceiveMethodLabel.Dispose ();
				ReceiveMethodLabel = null;
			}

			if (RepeatButton != null) {
				RepeatButton.Dispose ();
				RepeatButton = null;
			}

			if (TotalPriceLabel != null) {
				TotalPriceLabel.Dispose ();
				TotalPriceLabel = null;
			}
		}
	}
}

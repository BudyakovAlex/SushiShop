// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace SushiShop.Ios.Views.Cells.Shops
{
	[Register ("ShopItemViewCell")]
	partial class ShopItemViewCell
	{
		[Outlet]
		UIKit.UILabel AddressLabel { get; set; }

		[Outlet]
		SushiShop.Ios.Views.Controls.PrimaryButton MetroButton { get; set; }

		[Outlet]
		SushiShop.Ios.Views.Controls.PrimaryButton NearestMetroButton { get; set; }

		[Outlet]
		UIKit.UILabel PhoneLabel { get; set; }

		[Outlet]
		UIKit.UILabel WorkingTimeLabel { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (AddressLabel != null) {
				AddressLabel.Dispose ();
				AddressLabel = null;
			}

			if (PhoneLabel != null) {
				PhoneLabel.Dispose ();
				PhoneLabel = null;
			}

			if (WorkingTimeLabel != null) {
				WorkingTimeLabel.Dispose ();
				WorkingTimeLabel = null;
			}

			if (MetroButton != null) {
				MetroButton.Dispose ();
				MetroButton = null;
			}

			if (NearestMetroButton != null) {
				NearestMetroButton.Dispose ();
				NearestMetroButton = null;
			}
		}
	}
}

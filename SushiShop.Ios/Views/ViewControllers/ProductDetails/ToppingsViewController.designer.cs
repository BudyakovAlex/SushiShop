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
	[Register ("ToppingsViewController")]
	partial class ToppingsViewController
	{
		[Outlet]
		UIKit.UIButton AddButton { get; set; }

		[Outlet]
		UIKit.UITableView DataTableView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (DataTableView != null) {
				DataTableView.Dispose ();
				DataTableView = null;
			}

			if (AddButton != null) {
				AddButton.Dispose ();
				AddButton = null;
			}
		}
	}
}

// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace SushiShop.Ios.Views.ViewControllers.Orders
{
	[Register ("MyOrdersViewController")]
	partial class MyOrdersViewController
	{
		[Outlet]
		UIKit.UIActivityIndicatorView LoadingIndicator { get; set; }

		[Outlet]
		UIKit.UITableView TableView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (TableView != null) {
				TableView.Dispose ();
				TableView = null;
			}

			if (LoadingIndicator != null) {
				LoadingIndicator.Dispose ();
				LoadingIndicator = null;
			}
		}
	}
}

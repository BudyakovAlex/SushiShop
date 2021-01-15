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
	[Register ("OrderRegistrationViewController")]
	partial class OrderRegistrationViewController
	{
		[Outlet]
		SushiShop.Ios.Views.Controls.ScrollableTabView scrollableTabsView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (scrollableTabsView != null) {
				scrollableTabsView.Dispose ();
				scrollableTabsView = null;
			}
		}
	}
}

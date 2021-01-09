// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace SushiShop.Ios.Views.ViewControllers.Shops
{
	[Register ("ShopsViewController")]
	partial class ShopsViewController
	{
		[Outlet]
		UIKit.UICollectionView contentCollectionView { get; set; }

		[Outlet]
		SushiShop.Ios.Views.Controls.ScrollableTabView tabsScrollableTabView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (tabsScrollableTabView != null) {
				tabsScrollableTabView.Dispose ();
				tabsScrollableTabView = null;
			}

			if (contentCollectionView != null) {
				contentCollectionView.Dispose ();
				contentCollectionView = null;
			}
		}
	}
}

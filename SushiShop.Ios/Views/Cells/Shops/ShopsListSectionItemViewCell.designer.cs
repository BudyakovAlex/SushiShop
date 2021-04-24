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
	[Register ("ShopsListSectionItemViewCell")]
	partial class ShopsListSectionItemViewCell
	{
		[Outlet]
		UIKit.UITableView ContentTableView { get; set; }

		[Outlet]
		UIKit.UISearchBar SearchBar { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (ContentTableView != null) {
				ContentTableView.Dispose ();
				ContentTableView = null;
			}

			if (SearchBar != null) {
				SearchBar.Dispose ();
				SearchBar = null;
			}
		}
	}
}

// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace SushiShop.Ios.Views.Controls
{
	partial class NearestMetroView
	{
		[Outlet]
		SushiShop.Ios.Views.Controls.SelfSizeTableView MetroTableView { get; set; }

		[Outlet]
		UIKit.UIView RootContentView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (RootContentView != null) {
				RootContentView.Dispose ();
				RootContentView = null;
			}

			if (MetroTableView != null) {
				MetroTableView.Dispose ();
				MetroTableView = null;
			}
		}
	}
}

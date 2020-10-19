// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace SushiShop.Ios.Views.Cells.Menu
{
	[Register ("SimpleMenuGroupsCell")]
	partial class SimpleMenuGroupsCell
	{
		[Outlet]
		BuildApps.Core.Mobile.MvvmCross.UIKit.Views.Controls.MvxStackView GroupStackView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (GroupStackView != null) {
				GroupStackView.Dispose ();
				GroupStackView = null;
			}
		}
	}
}

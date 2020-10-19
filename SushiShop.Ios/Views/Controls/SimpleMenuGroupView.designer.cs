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
	partial class SimpleMenuGroupView
	{
		[Outlet]
		UIKit.UIView ContainerView { get; set; }

		[Outlet]
		UIKit.UIImageView GroupImageView { get; set; }

		[Outlet]
		UIKit.UILabel GroupNameLabel { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (ContainerView != null) {
				ContainerView.Dispose ();
				ContainerView = null;
			}

			if (GroupImageView != null) {
				GroupImageView.Dispose ();
				GroupImageView = null;
			}

			if (GroupNameLabel != null) {
				GroupNameLabel.Dispose ();
				GroupNameLabel = null;
			}
		}
	}
}

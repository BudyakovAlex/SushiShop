// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace SushiShop.Ios.Views.ViewControllers.Profile
{
	[Register ("BonusProgramViewController")]
	partial class BonusProgramViewController
	{
		[Outlet]
		UIKit.UILabel ContentLabel { get; set; }

		[Outlet]
		UIKit.UIScrollView ParentScrollView { get; set; }

		[Outlet]
		UIKit.UIView RoundedContentView { get; set; }

		[Outlet]
		UIKit.UIView ScrollViewContentView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (ContentLabel != null) {
				ContentLabel.Dispose ();
				ContentLabel = null;
			}

			if (RoundedContentView != null) {
				RoundedContentView.Dispose ();
				RoundedContentView = null;
			}

			if (ScrollViewContentView != null) {
				ScrollViewContentView.Dispose ();
				ScrollViewContentView = null;
			}

			if (ParentScrollView != null) {
				ParentScrollView.Dispose ();
				ParentScrollView = null;
			}
		}
	}
}

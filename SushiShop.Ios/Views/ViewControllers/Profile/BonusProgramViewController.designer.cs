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
		BuildApps.Core.Mobile.MvvmCross.UIKit.Views.Controls.MvxStackView BonusesImagesStackView { get; set; }

		[Outlet]
		UIKit.UILabel ContentLabel { get; set; }

		[Outlet]
		UIKit.UIScrollView ContentScrollView { get; set; }

		[Outlet]
		UIKit.UIActivityIndicatorView LoadingActivityIndicator { get; set; }

		[Outlet]
		UIKit.UIView RoundedContentView { get; set; }

		[Outlet]
		UIKit.UIView ScrollViewContentView { get; set; }

		[Outlet]
		UIKit.UILabel TitleLabel { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (ContentLabel != null) {
				ContentLabel.Dispose ();
				ContentLabel = null;
			}

			if (ContentScrollView != null) {
				ContentScrollView.Dispose ();
				ContentScrollView = null;
			}

			if (LoadingActivityIndicator != null) {
				LoadingActivityIndicator.Dispose ();
				LoadingActivityIndicator = null;
			}

			if (RoundedContentView != null) {
				RoundedContentView.Dispose ();
				RoundedContentView = null;
			}

			if (ScrollViewContentView != null) {
				ScrollViewContentView.Dispose ();
				ScrollViewContentView = null;
			}

			if (BonusesImagesStackView != null) {
				BonusesImagesStackView.Dispose ();
				BonusesImagesStackView = null;
			}

			if (TitleLabel != null) {
				TitleLabel.Dispose ();
				TitleLabel = null;
			}
		}
	}
}

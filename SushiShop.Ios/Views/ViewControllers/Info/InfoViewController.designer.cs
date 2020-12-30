// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace SushiShop.Ios.Views.ViewControllers.Info
{
	[Register ("InfoViewController")]
	partial class InfoViewController
	{
		[Outlet]
		BuildApps.Core.Mobile.MvvmCross.UIKit.Views.Controls.MvxStackView MenuStackView { get; set; }

		[Outlet]
		UIKit.UILabel PhoneLabel { get; set; }

		[Outlet]
		UIKit.UILabel PhoneTitleLabel { get; set; }

		[Outlet]
		UIKit.UIView ShopsContainerView { get; set; }

		[Outlet]
		UIKit.UILabel ShopsTitleLabel { get; set; }

		[Outlet]
		BuildApps.Core.Mobile.MvvmCross.UIKit.Views.Controls.MvxStackView SocialNetworksStackView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (PhoneTitleLabel != null) {
				PhoneTitleLabel.Dispose ();
				PhoneTitleLabel = null;
			}

			if (PhoneLabel != null) {
				PhoneLabel.Dispose ();
				PhoneLabel = null;
			}

			if (ShopsTitleLabel != null) {
				ShopsTitleLabel.Dispose ();
				ShopsTitleLabel = null;
			}

			if (ShopsContainerView != null) {
				ShopsContainerView.Dispose ();
				ShopsContainerView = null;
			}

			if (MenuStackView != null) {
				MenuStackView.Dispose ();
				MenuStackView = null;
			}

			if (SocialNetworksStackView != null) {
				SocialNetworksStackView.Dispose ();
				SocialNetworksStackView = null;
			}
		}
	}
}

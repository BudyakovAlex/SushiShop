// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace SushiShop.Ios.Views.ViewControllers.Promotions
{
	[Register ("PromotionsViewController")]
	partial class PromotionsViewController
	{
		[Outlet]
		UIKit.UICollectionView CollectionView { get; set; }

		[Outlet]
		UIKit.UIView LoadingView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (CollectionView != null) {
				CollectionView.Dispose ();
				CollectionView = null;
			}

			if (LoadingView != null) {
				LoadingView.Dispose ();
				LoadingView = null;
			}
		}
	}
}

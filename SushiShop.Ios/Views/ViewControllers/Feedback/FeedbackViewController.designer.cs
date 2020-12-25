// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace SushiShop.Ios.Views.ViewControllers.Feedback
{
	[Register ("FeedbackViewController")]
	partial class FeedbackViewController
	{
		[Outlet]
		UIKit.UIView LoadingView { get; set; }

		[Outlet]
		SushiShop.Ios.Views.Controls.FloatingTextView OrderNumberTextView { get; set; }

		[Outlet]
		UIKit.UICollectionView PhotosCollectionView { get; set; }

		[Outlet]
		UIKit.NSLayoutConstraint PhotosCollectionViewHeightConstraint { get; set; }

		[Outlet]
		SushiShop.Ios.Views.Controls.FloatingTextView QuestionTextView { get; set; }

		[Outlet]
		UIKit.UIScrollView ScrollView { get; set; }

		[Outlet]
		UIKit.NSLayoutConstraint ScrollViewBottomConstraint { get; set; }

		[Outlet]
		SushiShop.Ios.Views.Controls.PrimaryButton SendButton { get; set; }

		[Outlet]
		UIKit.UILabel UploadPhotosLabel { get; set; }

		[Outlet]
		UIKit.UIView UploadPhotosView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (LoadingView != null) {
				LoadingView.Dispose ();
				LoadingView = null;
			}

			if (PhotosCollectionView != null) {
				PhotosCollectionView.Dispose ();
				PhotosCollectionView = null;
			}

			if (PhotosCollectionViewHeightConstraint != null) {
				PhotosCollectionViewHeightConstraint.Dispose ();
				PhotosCollectionViewHeightConstraint = null;
			}

			if (QuestionTextView != null) {
				QuestionTextView.Dispose ();
				QuestionTextView = null;
			}

			if (ScrollView != null) {
				ScrollView.Dispose ();
				ScrollView = null;
			}

			if (ScrollViewBottomConstraint != null) {
				ScrollViewBottomConstraint.Dispose ();
				ScrollViewBottomConstraint = null;
			}

			if (SendButton != null) {
				SendButton.Dispose ();
				SendButton = null;
			}

			if (UploadPhotosLabel != null) {
				UploadPhotosLabel.Dispose ();
				UploadPhotosLabel = null;
			}

			if (UploadPhotosView != null) {
				UploadPhotosView.Dispose ();
				UploadPhotosView = null;
			}

			if (OrderNumberTextView != null) {
				OrderNumberTextView.Dispose ();
				OrderNumberTextView = null;
			}
		}
	}
}

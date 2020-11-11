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
	[Register ("FilteredProductsItemViewCell")]
	partial class FilteredProductsItemViewCell
	{
		[Outlet]
		UIKit.UICollectionView ProductsCollectionView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (ProductsCollectionView != null) {
				ProductsCollectionView.Dispose ();
				ProductsCollectionView = null;
			}
		}
	}
}

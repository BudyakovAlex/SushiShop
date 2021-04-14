using Android.Views;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Extensions;
using MvvmCross.Platforms.Android.Binding.BindingContext;

namespace SushiShop.Droid.Views.ViewHolders.Menu.Products
{
    public class RelatedProductItemViewHolder : MenuProductItemViewHolder
    {
        public RelatedProductItemViewHolder(View view, IMvxAndroidBindingContext context) : base(view, context)
        {
            view.LayoutParameters.Width = (int)view.Context.DpToPx(168);
        }
    }
}

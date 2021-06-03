using Android.Views;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using SushiShop.Core.ViewModels.Shops.Sections;
using SushiShop.Droid.Views.ViewHolders.Abstract;

namespace SushiShop.Droid.Views.ViewHolders.Shops.Sections
{
    public class ShopsOnMapSectionViewHolder : CardViewHolder<ShopsOnMapSectionViewModel>
    {
        public ShopsOnMapSectionViewHolder(View view, IMvxAndroidBindingContext context) : base(view, context)
        {
        }

        protected override void DoInit(View view)
        {
            base.DoInit(view);
        }

        public override void BindData()
        {
            base.BindData();

            using var bindingSet = CreateBindingSet();
        }
    }
}

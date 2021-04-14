using Android.Views;
using Android.Widget;
using MvvmCross.Platforms.Android.Binding;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using SushiShop.Core.ViewModels.Menu.Items;
using SushiShop.Droid.Extensions;
using SushiShop.Droid.Views.ViewHolders.Abstract;

namespace SushiShop.Droid.Views.ViewHolders.Menu.Grid
{
    public class MenuPromotionItemViewHolder : CardViewHolder<MenuPromotionItemViewModel>
    {
        private ImageView imageView;

        public MenuPromotionItemViewHolder(View view, IMvxAndroidBindingContext context) : base(view, context)
        {
        }

        protected override void DoInit(View view)
        {
            base.DoInit(view);

            imageView = view as ImageView;
        }

        public override void BindData()
        {
            base.BindData();

            using var bindingSet = CreateBindingSet();

            bindingSet.Bind(imageView).For(v => v.BindAdaptedUrl()).To(vm => vm.ImageUrl);
            bindingSet.Bind(imageView).For(v => v.BindClick()).To(vm => vm.ShowDetailsCommand);
        }
    }
}
using Android.Graphics;
using Android.Views;
using Android.Widget;
using AndroidX.Core.Content.Resources;
using MvvmCross.Platforms.Android.Binding;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using SushiShop.Core.ViewModels.Menu.Items;
using SushiShop.Droid.Extensions;
using SushiShop.Droid.Views.ViewHolders.Abstract;

namespace SushiShop.Droid.Views.ViewHolders.Menu.Grid
{
    public class CategoryMenuItemViewHolder : CardViewHolder<CategoryMenuItemViewModel>
    {
        private ImageView imageView;
        private TextView titleTextView;

        public CategoryMenuItemViewHolder(View view, IMvxAndroidBindingContext context) : base(view, context)
        {
        }

        protected override void DoInit(View view)
        {
            base.DoInit(view);

            imageView = view.FindViewById<ImageView>(Resource.Id.image_view);

            titleTextView = view.FindViewById<TextView>(Resource.Id.title_text_view);
            //var themeColorResource = Xamarin.Essentials.AppInfo.RequestedTheme == Xamarin.Essentials.AppTheme.Light
            //    ? Resource.Color.black
            //    : Resource.Color.black;

            //var argbColor = ResourcesCompat.GetColor(view.Context.Resources, themeColorResource, null);
            //var titleTextColor = new Color(argbColor);
            //titleTextView.SetTextColor(titleTextColor);
        }

        public override void BindData()
        {
            base.BindData();

            using var bindingSet = CreateBindingSet();

            bindingSet.Bind(imageView).For(v => v.BindAdaptedUrl()).To(vm => vm.ImageUrl);
            bindingSet.Bind(titleTextView).For(v => v.Text).To(vm => vm.Title);
            bindingSet.Bind(imageView).For(v => v.BindClick()).To(vm => vm.ShowDetailsCommand);
        }
    }
}
using Android.Views;
using Android.Widget;
using MvvmCross.Platforms.Android.Binding;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using SushiShop.Core.ViewModels.Info.Items;
using SushiShop.Droid.Views.ViewHolders.Abstract;

namespace SushiShop.Droid.Views.ViewHolders.Info
{
    public class InfoMenuItemViewHolder : CardViewHolder<InfoMenuItemViewModel>
    {
        private View view;
        private TextView titleTextView;

        public InfoMenuItemViewHolder(View view, IMvxAndroidBindingContext context) : base(view, context)
        {
        }

        protected override void DoInit(View view)
        {
            base.DoInit(view);

            this.view = view;
            titleTextView = view.FindViewById<TextView>(Resource.Id.title_text_view);
        }

        public override void BindData()
        {
            base.BindData();

            using var bindingSet = CreateBindingSet();

            bindingSet.Bind(view).For(v => v.BindClick()).To(vm => vm.ExecuteMenuActionCommand);
            bindingSet.Bind(titleTextView).For(v => v.Text).To(vm => vm.Title);
        }
    }
}

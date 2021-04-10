using Android.App;
using Android.Widget;
using AndroidX.AppCompat.Widget;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Adapter.TemplateSelectors;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Adapters;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Extensions;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Views.Activities;
using MvvmCross.DroidX.RecyclerView;
using MvvmCross.Platforms.Android.Binding;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using SushiShop.Core.Resources;
using SushiShop.Core.ViewModels.CardProduct.Items;
using SushiShop.Core.ViewModels.ProductDetails;
using SushiShop.Droid.Views.ViewHolders.Toppings;

namespace SushiShop.Droid.Views.Activities.Toppings
{
    [Activity]
    public class ToppingsActivty : BaseActivity<ToppingsViewModel>
    {
        private TextView resetTextView;
        private TextView toolbarTitleTextView;
        private AppCompatButton applyButton;
        private MvxRecyclerView recyclerView;

        public ToppingsActivty() : base(Resource.Layout.activity_toppings_selection)
        {
        }

        protected override void InitializeViewPoroperties()
        {
            base.InitializeViewPoroperties();

            InitializeRecyclerView();

            resetTextView = FindViewById<TextView>(Resource.Id.reset_text_view);
            resetTextView.Text = AppStrings.Discard;

            toolbarTitleTextView = FindViewById<TextView>(Resource.Id.toolbar_title_text_view);

            applyButton = FindViewById<AppCompatButton>(Resource.Id.apply_button);
            applyButton.Text = AppStrings.AddToCart;
            applyButton.SetRoundedCorners(this.DpToPx(25));

            var toolbar = FindViewById<AndroidX.AppCompat.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
        }

        protected override void Bind()
        {
            base.Bind();

            using var bindingSet = CreateBindingSet();

            bindingSet.Bind(resetTextView).For(v => v.BindClick()).To(v => v.ResetCommand);
            bindingSet.Bind(applyButton).For(v => v.BindClick()).To(v => v.AddToCartCommand);
            bindingSet.Bind(recyclerView).For(v => v.ItemsSource).To(vm => vm.Items);
            bindingSet.Bind(toolbarTitleTextView).For(v => v.Text).To(vm => vm.PageTitle);
        }

        private void InitializeRecyclerView()
        {
            recyclerView = FindViewById<MvxRecyclerView>(Resource.Id.recycler_view);
            recyclerView.Adapter = new RecycleViewBindableAdapter((IMvxAndroidBindingContext)BindingContext);
            recyclerView.ItemTemplateSelector = new TemplateSelector()
                .AddElement<ToppingItemViewModel, ToppingItemViewHolder>(Resource.Layout.item_select_topping);
        }
    }
}

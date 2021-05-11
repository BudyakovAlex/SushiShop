using Android.OS;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.Widget;
using AndroidX.ConstraintLayout.Widget;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Adapter.TemplateSelectors;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Adapters;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Extensions;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Views.Fragments;
using Google.Android.Material.TextField;
using MvvmCross.DroidX;
using MvvmCross.DroidX.RecyclerView;
using MvvmCross.Platforms.Android.Binding;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using SushiShop.Core.Resources;
using SushiShop.Core.ViewModels;
using SushiShop.Core.ViewModels.Cart;
using SushiShop.Core.ViewModels.Cart.Items;
using SushiShop.Droid.Views.Fragments.Abstract;
using SushiShop.Droid.Views.ViewHolders.Cart;
using Toolbar = AndroidX.AppCompat.Widget.Toolbar;

namespace SushiShop.Droid.Views.Fragments.Cart
{
    [MvxTabLayoutPresentation(
        TabLayoutResourceId = Resource.Id.main_tab_layout,
        ViewPagerResourceId = Resource.Id.main_view_pager,
        ActivityHostViewModelType = typeof(MainViewModel))]
    public class CartFragment : BaseFragment<CartViewModel>, ITabFragment
    {
        private Toolbar toolbar;
        private MvxRecyclerView productsRecyclerView;
        private MvxRecyclerView toppingsRecyclerView;
        private LinearLayout addSaucesLinearLayout;
        private TextView choosePackageTextView;
        private MvxRecyclerView packagesRecyclerView;
        private TextInputLayout promocodeInputLayout;
        private TextInputEditText promocodeEditText;
        private AppCompatButton checkoutButton;
        private TextView totalPriceTextView;
        private ConstraintLayout emptyCartConstraintLayout;
        private MvxSwipeRefreshLayout swipeRefreshLayout;

        public CartFragment()
            : base(Resource.Layout.fragment_cart)
        {
        }

        public bool IsActivated { get; set; }

        protected override void InitializeViewPoroperties(View view, Bundle savedInstanceState)
        {
            base.InitializeViewPoroperties(view, savedInstanceState);

            toolbar = view.FindViewById<Toolbar>(Resource.Id.toolbar);
            addSaucesLinearLayout = view.FindViewById<LinearLayout>(Resource.Id.add_sauce_linear_layout);
            choosePackageTextView = view.FindViewById<TextView>(Resource.Id.choose_package_text_view);
            promocodeInputLayout = view.FindViewById<TextInputLayout>(Resource.Id.promocode_input_layout);
            promocodeEditText = view.FindViewById<TextInputEditText>(Resource.Id.promocode_edit_text);
            checkoutButton = view.FindViewById<AppCompatButton>(Resource.Id.checkout_button);
            totalPriceTextView = view.FindViewById<TextView>(Resource.Id.total_price_text_view);
            emptyCartConstraintLayout = view.FindViewById<ConstraintLayout>(Resource.Id.empty_basket_constraint_layout);
            swipeRefreshLayout = view.FindViewById<MvxSwipeRefreshLayout>(Resource.Id.swipe_refresh_layout);

            choosePackageTextView.Text = AppStrings.ChoosePackage;
            promocodeInputLayout.Hint = AppStrings.Promocode;
            view.FindViewById<TextView>(Resource.Id.add_sauce_text_view).Text = AppStrings.AddSauce;
            checkoutButton.Text = AppStrings.CheckoutOrder;
            view.FindViewById<TextView>(Resource.Id.total_price_title_text_view).Text = $"{AppStrings.Total}:";
            view.FindViewById<TextView>(Resource.Id.empty_cart_text_view).Text = AppStrings.EmptyCart;

            checkoutButton.SetRoundedCorners(Context.DpToPx(25));

            InitializeProductsRecyclerView();
            InitializeSaucesRecyclerView();
            InitializePackagesRecyclerView();
        }

        protected override void Bind()
        {
            base.Bind();

            using var bindingSet = CreateBindingSet();

            bindingSet.Bind(toolbar).For(v => v.Title).To(vm => vm.Title);
            bindingSet.Bind(addSaucesLinearLayout).For(v => v.BindClick()).To(vm => vm.AddSaucesCommand);
            bindingSet.Bind(addSaucesLinearLayout).For(v => v.BindVisible()).To(vm => vm.CanAddSauses);
            bindingSet.Bind(productsRecyclerView).For(v => v.ItemsSource).To(vm => vm.Products);
            bindingSet.Bind(toppingsRecyclerView).For(v => v.ItemsSource).To(vm => vm.Sauces);
            bindingSet.Bind(packagesRecyclerView).For(v => v.ItemsSource).To(vm => vm.Packages);
            bindingSet.Bind(packagesRecyclerView).For(v => v.BindVisible()).To(vm => vm.CanAddPackages);
            bindingSet.Bind(choosePackageTextView).For(v => v.BindVisible()).To(vm => vm.CanAddPackages);
            bindingSet.Bind(promocodeEditText).For(v => v.Text).To(vm => vm.Promocode);
            bindingSet.Bind(checkoutButton).For(v => v.BindClick()).To(vm => vm.CheckoutCommand);
            bindingSet.Bind(totalPriceTextView).For(v => v.Text).To(vm => vm.TotalPrice);
            bindingSet.Bind(emptyCartConstraintLayout).For(v => v.BindVisible()).To(vm => vm.IsEmptyBasket);
            bindingSet.Bind(swipeRefreshLayout).For(v => v.Refreshing).To(vm => vm.IsRefreshing);
            bindingSet.Bind(swipeRefreshLayout).For(v => v.RefreshCommand).To(vm => vm.RefreshDataCommand);
        }

        private void InitializeProductsRecyclerView()
        {
            productsRecyclerView = View.FindViewById<MvxRecyclerView>(Resource.Id.products_recycler_view);
            productsRecyclerView.Adapter = new RecycleViewBindableAdapter((IMvxAndroidBindingContext)BindingContext);
            productsRecyclerView.ItemTemplateSelector = new TemplateSelector()
                .AddElement<CartProductItemViewModel, CartProductItemViewHolder>(Resource.Layout.item_cart_product);
        }

        private void InitializeSaucesRecyclerView()
        {
            toppingsRecyclerView = View.FindViewById<MvxRecyclerView>(Resource.Id.toppings_recycler_view);
            toppingsRecyclerView.Adapter = new RecycleViewBindableAdapter((IMvxAndroidBindingContext)BindingContext);
            toppingsRecyclerView.ItemTemplateSelector = new TemplateSelector()
                .AddElement<CartToppingItemViewModel, CartToppingItemViewHolder>(Resource.Layout.item_cart_topping);
        }

        private void InitializePackagesRecyclerView()
        {
            packagesRecyclerView = View.FindViewById<MvxRecyclerView>(Resource.Id.packages_recycler_view);
            packagesRecyclerView.Adapter = new RecycleViewBindableAdapter((IMvxAndroidBindingContext)BindingContext);
            packagesRecyclerView.ItemTemplateSelector = new TemplateSelector()
                .AddElement<CartPackItemViewModel, CartPackItemViewHolder>(Resource.Layout.item_cart_pack);
        }
    }
}
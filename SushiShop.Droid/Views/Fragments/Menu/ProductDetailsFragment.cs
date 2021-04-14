using Android.Graphics;
using Android.OS;
using Android.Views;
using Android.Widget;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Adapter.TemplateSelectors;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Adapters;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Extensions;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Views.Fragments;
using MvvmCross.Binding.Combiners;
using MvvmCross.DroidX.RecyclerView;
using MvvmCross.Platforms.Android.Binding;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using SushiShop.Core.Resources;
using SushiShop.Core.ViewModels;
using SushiShop.Core.ViewModels.Menu.Items;
using SushiShop.Core.ViewModels.ProductDetails;
using SushiShop.Droid.Extensions;
using SushiShop.Droid.Views.Controls;
using SushiShop.Droid.Views.Decorators;
using SushiShop.Droid.Views.ViewHolders.Menu.Products;

namespace SushiShop.Droid.Views.Fragments.Menu
{
    [MvxFragmentPresentation(
        AddToBackStack = true,
        ActivityHostViewModelType = typeof(MainViewModel))]
    public class ProductDetailsFragment : BaseFragment<ProductDetailsViewModel>
    {
        private ImageView backImageView;
        private ImageView productImageView;
        private TextView productNameTextView;
        private TextView productWeightTextView;
        private TextView productDescriptionTextView;
        private TextView productPriceTextView;
        private TextView productOldPriceTextView;
        private TextView proteintTextView;
        private TextView fatsTextView;
        private TextView carbohydratesTextVoew;
        private TextView caloriesTextView;
        private MvxRecyclerView relatedProductsRecyclerView;
        private BigStepperView stepperView;
        private Button addToBasketButton;

        private int itemSpace;

        public ProductDetailsFragment()
            : base(Resource.Layout.fragment_product_details)
        {
        }

        protected override void InitializeViewPoroperties(View view, Bundle savedInstanceState)
        {
            base.InitializeViewPoroperties(view, savedInstanceState);

            backImageView = view.FindViewById<ImageView>(Resource.Id.back_image_view);
            productImageView = view.FindViewById<ImageView>(Resource.Id.product_image_view);
            productNameTextView = view.FindViewById<TextView>(Resource.Id.product_name_text_view);
            productWeightTextView = view.FindViewById<TextView>(Resource.Id.product_weight_text_view);
            productDescriptionTextView = view.FindViewById<TextView>(Resource.Id.product_description_text_view);
            productPriceTextView = view.FindViewById<TextView>(Resource.Id.price_text_view);
            productOldPriceTextView = view.FindViewById<TextView>(Resource.Id.old_price_text_view);
            proteintTextView = view.FindViewById<TextView>(Resource.Id.protein_text_view);
            fatsTextView = view.FindViewById<TextView>(Resource.Id.fats_text_view);
            carbohydratesTextVoew = view.FindViewById<TextView>(Resource.Id.carbohydrates_text_view);
            caloriesTextView = view.FindViewById<TextView>(Resource.Id.calories_text_view);
            stepperView = view.FindViewById<BigStepperView>(Resource.Id.stepper_view);
            addToBasketButton = view.FindViewById<Button>(Resource.Id.add_to_basket_button);
            addToBasketButton.SetRoundedCorners(Context.DpToPx(25));
            addToBasketButton.Text = AppStrings.AddToCart;

            productOldPriceTextView.PaintFlags |= PaintFlags.StrikeThruText;
            itemSpace = (int)view.Context.Resources.GetDimension(Resource.Dimension.product_item_margin);
            InitializeRelatedProductsRecyclerView();
        }

        protected override void Bind()
        {
            base.Bind();

            using var bindingSet = CreateBindingSet();

            bindingSet.Bind(backImageView).For(v => v.BindClick()).To(vm => vm.CloseCommand);
            bindingSet.Bind(productImageView).For(v => v.BindAdaptedUrl()).To(vm => vm.BackgroungImageUrl);
            bindingSet.Bind(productNameTextView).For(v => v.Text).To(vm => vm.Title);
            bindingSet.Bind(productWeightTextView).For(v => v.Text).To(vm => vm.Weight);
            bindingSet.Bind(productDescriptionTextView).For(v => v.Text).To(vm => vm.Description);
            bindingSet.Bind(productPriceTextView).For(v => v.Text).To(vm => vm.Price);
            bindingSet.Bind(productOldPriceTextView).For(v => v.Text).To(vm => vm.OldPrice);
            bindingSet.Bind(proteintTextView).For(v => v.Text).To(vm => vm.Protein);
            bindingSet.Bind(fatsTextView).For(v => v.Text).To(vm => vm.Fats);
            bindingSet.Bind(carbohydratesTextVoew).For(v => v.Text).To(vm => vm.Carbohydrates);
            bindingSet.Bind(caloriesTextView).For(v => v.Text).To(vm => vm.Calories);
            bindingSet.Bind(relatedProductsRecyclerView).For(v => v.ItemsSource).To(vm => vm.RelatedItems);

            bindingSet.Bind(addToBasketButton).For(v => v.BindClick()).To(vm => vm.AddToCartCommand);
            bindingSet.Bind(addToBasketButton).For(v => v.BindHidden()).ByCombining(new MvxOrValueCombiner(),
                                                                                    vm => vm.IsStepperVisible,
                                                                                    vm => vm.IsReadOnly);

            bindingSet.Bind(stepperView).For(v => v.DataContext).To(vm => vm.StepperViewModel);
            bindingSet.Bind(stepperView).For(v => v.BindHidden()).ByCombining(new MvxOrValueCombiner(),
                                                                              vm => vm.IsHiddenStepper,
                                                                              vm => vm.IsReadOnly);
        }

        private void InitializeRelatedProductsRecyclerView()
        {
            relatedProductsRecyclerView = View.FindViewById<MvxRecyclerView>(Resource.Id.related_products_recycler_view);

            relatedProductsRecyclerView.Adapter = new RecycleViewBindableAdapter((IMvxAndroidBindingContext)BindingContext);
            relatedProductsRecyclerView.ItemTemplateSelector = new TemplateSelector()
                .AddElement<ProductItemViewModel, RelatedProductItemViewHolder>(Resource.Layout.item_products_product);
            var layoutManager = new MvxGuardedLinearLayoutManager(Context) { Orientation = MvxGuardedLinearLayoutManager.Horizontal };
            relatedProductsRecyclerView.SetLayoutManager(layoutManager);
            relatedProductsRecyclerView.AddItemDecoration(new SpacesItemDecoration(CalculateItemSpace));
        }

        private Rect CalculateItemSpace(int position, Rect rect)
        {
            rect.Top = 20;
            rect.Bottom = itemSpace;
            rect.Left = itemSpace;
            rect.Right = ViewModel.RelatedItems.Count - 1 == position ? itemSpace : 0;
            return rect;
        }
    }
}
